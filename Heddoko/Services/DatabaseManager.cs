/**
 * @file DatabaseManager.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Services
{
    public static class DatabaseManager
    {
        public static void Migrate()
        {
            Migrator.RunMigrations();
        }

        public static IEnumerable<string> Pending()
        {
            return Migrator.GetPending();
        }

        public static void Rollback(string migration)
        {
            Migrator.RunMigrations(migration);
        }
    }
}
