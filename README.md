# heddoko-backend

0. clear redis
redis-cli KEYS "dev:*"

redis-cli KEYS "dev:*" | xargs redis-cli DEL

1. Revert to first Migration
Update-Database –TargetMigration: $InitialDatabase

2. Analytic Dashboard are separated from asp.net mvc
2.1 all angular project place in Heddoko\Heddoko\DashboardUI
2.2 in Heddoko\Heddoko run "grunt" to push changes to asp.net mvc assets


3. Upload sowftware via Powershel script

upload-desktop-app.ps1 -version 1.1 -file c:\users\file.exe