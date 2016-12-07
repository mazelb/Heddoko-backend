    Write-Host "Getting prev successful migration to rollback "
    
    $connectionString = $OctopusParameters["Heddoko.ConnectionString"]
    $sqlCommand = $OctopusParameters["Heddoko.GetLastMigrationQuery"]
    
    $connection = new-object system.data.SqlClient.SQLConnection($connectionString)
    $command = new-object system.data.sqlclient.sqlcommand($sqlCommand,$connection)
    $connection.Open()

    $res = $command.executescalar()
    
    Set-OctopusVariable -name "MigrationToRollback" -value $res
    
    Write-Host "Set MigrationToRollback variable:  '$res'"

    $connection.Close();
    
    Write-Host "Updating DB"
    .\migrate.exe DAL.dll /startupConfigurationFile="DAL.dll.config" /verbose