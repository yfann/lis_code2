using Lis.Common.Logger;
using System.Configuration;
using System.Data.Entity.Infrastructure;

namespace Lis.EntityFramework.Implements
{
    public class MigrationsContextFactory : IDbContextFactory<LisContext>
    {
        public LisContext Create()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["LisContext"].ConnectionString;
            return new LisContext(connectionString, new SystemLogger(), null);
        }
    }
}
