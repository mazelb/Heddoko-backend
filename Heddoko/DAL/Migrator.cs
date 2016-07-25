using System.Collections.Generic;
using System.Data.Entity.Migrations;
using DAL.Migrations;

namespace DAL
{
    public static class Migrator
    {
        public static void RunMigrations(string version = null)
        {
            Configuration migrator = new Configuration();
            DbMigrator dbMigrator = new DbMigrator(migrator);
            dbMigrator.Update(version);
        }

        public static void InitMigration()
        {
            RunMigrations("0");
        }

        public static IEnumerable<string> GetPending()
        {
            Configuration migrator = new Configuration();
            DbMigrator dbMigrator = new DbMigrator(migrator);
            return dbMigrator.GetPendingMigrations();
        }
    }
}