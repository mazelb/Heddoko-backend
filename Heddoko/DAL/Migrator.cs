using DAL.Migrations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Migrator
    {
        //public static void RunMigrations(string version = null)
        //{
        //    Configuration migrator = new Configuration();
        //    DbMigrator dbMigrator = new System.Data.Entity.Migrations.DbMigrator(migrator);
        //    dbMigrator.Update(version);
        //}

        //public static void InitMigration()
        //{
        //    RunMigrations("0");
        //}

        //public static IEnumerable<string> GetPending()
        //{
        //    Configuration migrator = new Configuration();
        //    DbMigrator dbMigrator = new System.Data.Entity.Migrations.DbMigrator(migrator);
        //    return dbMigrator.GetPendingMigrations();
        //}
    }
}
