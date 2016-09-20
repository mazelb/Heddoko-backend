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
