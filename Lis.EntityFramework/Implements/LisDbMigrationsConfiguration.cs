using System;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;

namespace Lis.EntityFramework.Implements
{
    public class LisDbMigrationsConfiguration : DbMigrationsConfiguration<LisContext>
    {
        public LisDbMigrationsConfiguration()
        {
            base.AutomaticMigrationsEnabled = true;
            base.AutomaticMigrationDataLossAllowed = true;
            base.ContextKey = "Lis.EntityFramework.LisDbMigrationsConfiguration";
        }

        /// <summary>
        /// Add initialize data
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(LisContext context)
        {
            context.InitialData();
            base.Seed(context);
        }
    }
}
