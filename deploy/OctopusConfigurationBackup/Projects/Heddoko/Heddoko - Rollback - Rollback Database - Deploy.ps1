$targetMigration = $OctopusParameters["Octopus.Action[Migrate Database].Output.MigrationToRollback"]

Write-Host "Rollback to migration $targetMigration"

.\migrate.exe  DAL.dll /startupConfigurationFile="DAL.dll.config" /targetMigration="$($targetMigration)" /verbose