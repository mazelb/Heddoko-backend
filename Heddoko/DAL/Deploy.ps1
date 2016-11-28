$connectionString = $OctopusParameters["SQL.Heddoko.DeployConnectionString"]

.\migrate.exe DAL.dll /connectionString="$($connectionString)" /connectionProviderName="System.Data.SqlClient"
