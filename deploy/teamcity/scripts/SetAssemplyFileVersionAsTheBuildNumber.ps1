function TruncateString([string] $s, [int] $maxLength)
{
    return $s.substring(0, [System.Math]::Min($maxLength, $s.Length))
}

# Fetch AssemblyFileVersion from AssemblyInfo.cs for use as the base for the build number. Example of what
# we can expect: "1.1.82.88"
# We need to filter out some invalid characters and possibly truncate the result and then we're good to go. 
$info = (Get-Content %MainAssemblyInfoFilePath%)

Write-Host $info

$matches = ([regex]'AssemblyFileVersion\(\"([^\"]+)\"\)').Matches($info)
$baseNumber = $matches[0].Groups[1].Value
$branch = "%teamcity.build.branch%"

# Remove "parent" folders from branch name.
# Example "1.0.119-bug/XENA-5834" =&amp;amp;amp;gt; "1.0.119-XENA-5834"
$branch = ($branch -replace '^([^/]+/)*(.+)$','$2' )

# Filter out illegal characters, replace with '-'
$branch = ($branch -replace '[/\\ _\.]','-')

$releaseBranch = "%ReleaseBranchName%"

Write-Host "relese branch: '$releaseBranch'"

if ($Branch -eq $releaseBranch) {
    $newBuildNumber = "$baseNumber"
}
else{
    $newBuildNumber = "$baseNumber-$branch"
}

# Limit build number to 20 characters to make it work with Octopack
$newBuildNumber = (TruncateString $newBuildNumber 20)

Write-Host "##teamcity[buildNumber '$newBuildNumber']"