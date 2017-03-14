write-host "before -flush"

$res = & .\HeddokoService.exe "-flush"

Write-Host "$res"

write-host "flush done"