using Lis.Domain;
using Lis.EntityFramework.Implements;
using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Lis.EntityFramework.Interfaces
{
    public interface IDbContext
    {
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : Entity;
    }
}
