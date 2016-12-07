function OctopusDeploymentSuccessful ($taskApiPath, $octopus_server, $octopus_apikey) {
    $octopus_url = "$($octopus_server)$taskApiPath"
    $result = Invoke-RestMethod -Uri $octopus_url -ContentType application/json -Headers @{"X-Octopus-ApiKey"="$octopus_apikey"} -Method Get
    $result.State -eq "Success"
}

function OctopusPreviousSuccessfulReleaseId ($octopus_server, $octopus_apikey, $project, $environment) {
    $octopus_url = "$($octopus_server)/api/deployments?projects=$project&environments=$environment"
    $result = Invoke-RestMethod -Uri $octopus_url -ContentType application/json -Headers @{"X-Octopus-ApiKey"="$octopus_apikey"} -Method Get
    $item = $result.Items |
        where { OctopusDeploymentSuccessful -taskApiPath $_.Links.Task -octopus_server $octopus_server -octopus_apikey $octopus_apikey} |
        Select-Object -first 1
    $item.ReleaseId
}

Write-Host "**********"
Write-Host "Chained Deployment of '$Chain_ProjectName' last successful release to '$Chain_DeployTo'."

Write-Host "**********"

$baseUri = $OctopusParameters['Octopus.Web.BaseUrl']
$reqheaders = @{"X-Octopus-ApiKey" = $Chain_ApiKey }

# Find Environment
$environments = Invoke-WebRequest "$baseUri/api/environments/all" -Headers $reqheaders -UseBasicParsing | ConvertFrom-Json
$environment = $environments | Where-Object {$_.Name -eq $Chain_DeployTo }
if ($environment -eq $null) {
    throw "Environment $Chain_DeployTo not found."
}

# Find Project
$projects = Invoke-WebRequest "$baseUri/api/projects/all" -Headers $reqheaders -UseBasicParsing | ConvertFrom-Json
$project = $projects | Where-Object {$_.Name -eq $Chain_ProjectName }
if ($project -eq $null) {
    throw "Project $Chain_ProjectName not found."
}

Write-Host "Get release of the last successful deploy"
# Get release Id of the last successful deploy
try
{
    $releaseId = OctopusPreviousSuccessfulReleaseId -octopus_server $baseUri -octopus_apikey $Chain_ApiKey -project $project.Id -environment $environment.Id
}
catch
{
    $result = $_.Exception.Response.GetResponseStream()
    $reader = New-Object System.Io.StreamReader($result);
    $responseBody = $reader.ReadToEnd();
    throw "Unable to find information of last successful release: $responseBody"
}

Write-Host "Deploying release Id '$releaseId'"

# Check for existing release with given release id
$releaseUri = "$baseUri/api/releases/$releaseId"
try {
$release = Invoke-WebRequest $releaseUri -Headers $reqheaders -UseBasicParsing | ConvertFrom-Json
} catch {
    if ($_.Exception.Response.StatusCode.Value__ -ne 404) {
        $result = $_.Exception.Response.GetResponseStream()
        $reader = New-Object System.Io.StreamReader($result);
        $responseBody = $reader.ReadToEnd();
        throw "Error occurred retrieving a Release: $responseBody"
    }
}

# We should have a release at this point
if ($release -eq $null) {
    throw "Release '$releaseId' of '$Chain_ProjectName' not found. Could not create deployment."
}

# Get roollback step name
$stepId = $OctopusParameters["Octopus.Step.Id"]
Write-Host "Step Id to skip in next deploy '$stepId'"

try {
$projectId = $project.Id
$deploymentProcessUri = "$baseUri/api/deploymentprocesses/deploymentprocess-$projectId"
$deploymentProcess = Invoke-RestMethod -Uri $deploymentProcessUri -ContentType application/json -Headers $reqheaders -Method Get

$step = $deploymentProcess.Steps | Where-Object {$_.Id -eq $stepId }

$actions = $step.Actions | Select-Object -ExpandProperty Id
} catch {
    $result = $_.Exception.Response.GetResponseStream()
    $reader = New-Object System.Io.StreamReader($result);
    $responseBody = $reader.ReadToEnd();
    throw "Error occurred getting actions to skip: $responseBody"
}

Write-Host "Child actions to skip in next deploy $actions"

# Create deployment
$deployment = @{
    ReleaseId = $release.Id
    EnvironmentId = $environment.Id
    SkipActions = $actions
} | ConvertTo-Json
try {
    $version = $release.Version
    Write-Host "Deploying '$Chain_ProjectName' version '$version' to '$Chain_DeployTo'"
    $result = Invoke-WebRequest "$baseUri/api/deployments" -Method Post -Headers $reqHeaders -Body $deployment -UseBasicParsing | ConvertFrom-Json
} catch {
    $result = $_.Exception.Response.GetResponseStream()
    $reader = New-Object System.Io.StreamReader($result);
    $responseBody = $reader.ReadToEnd();
    throw "Error occurred creating a Deployment: $responseBody"
}

if([bool]::Parse($Chain_WaitForDeployment)) {
    Write-Host "Waiting for deployment to complete before continuing..."
    $taskId = $result.TaskId
    $task = Invoke-WebRequest "$baseUri/api/tasks/$taskId" -Headers $reqHeaders -UseBasicParsing | ConvertFrom-Json
    while($task.State -eq "Queued" -or $task.State -eq "Executing") {
        Start-Sleep -Seconds 2
        $task = Invoke-WebRequest "$baseUri/api/tasks/$taskId" -Headers $reqHeaders -UseBasicParsing | ConvertFrom-Json
    }
}