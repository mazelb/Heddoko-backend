[int]$timeoutSec = $null
if(-not [int]::TryParse($Timeout, [ref]$timeoutSec)) { $timeoutSec = 60 }
$json = ConvertTo-Json -Compress -InputObject @{"title" = $Title; "text" = $Body; }
Invoke-WebRequest -Method Post -Uri $Url -Body $json -ContentType "application/json; charset=utf-8" -UseBasicParsing -TimeoutSec $Timeout 