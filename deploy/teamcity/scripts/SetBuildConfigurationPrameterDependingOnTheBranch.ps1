$branch = "%teamcity.build.branch%"

if ($Branch -eq "%ReleaseBranchName%") {
   Write-Host "##teamcity[setParameter name='configuration' value='prod']"
}
else {
   Write-Host "##teamcity[setParameter name='configuration' value='dev']"
}