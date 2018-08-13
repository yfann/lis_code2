using AutoMapper;
using Lis.Common;
using Lis.Domain;
using Lis.Domain.Entities;
using Lis.EntityFramework.Interfaces;
using Lis.Common.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Lis.EntityFramework.Implements
{    
    public class LisContext : DbContext, ILisContext
    {
        private readonly ISystemLogger _logger;
        private readonly IInitialDataService _InitialDataService;

        public LisContext(string connectionString, ISystemLogger logger, IInitialDataService initialDataService)
            : base(connectionString)
        {
            _logger = logger;
            _InitialDataService = initialDataService;
            Database.Log = log => Debug.WriteLine(log);
            this.Database.CommandTimeout = 0;

            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }


        public void InitialData()
        {
            if (_InitialDataService != null)
            {
                _InitialDataService.InitialData(this);
            }
        }

        public new DbSet<T> Set<T>() where T : Entity
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

        public new int SaveChanges()
        {
            try
            {
                ChangeTracker.DetectChanges();

                var nowTime = DateTime.Now;
                string userId = Consts.SuperUserId;
                if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
                {
                    var identities = Thread.CurrentPrincipal.Identity.Name.Split(new[] { '@' });
                    if (identities.Length == 3)
                    {
                        userId = identities[2];
                    }
                }

                var objectContext = ((IObjectContextAdapter)this).ObjectContext;
                var entries = objectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified);
                foreach (ObjectStateEntry entry in entries)
                {
                    if (entry.Entity != null)
                    {
                        var mutableObj = entry.Entity as IMutable;
                        if (mutableObj != null)
                        {
                            if (entry.State == EntityState.Added)
                            {
                                mutableObj.CreateUserId = userId;
                                mutableObj.CreateTime = nowTime;
                            }
                            mutableObj.LastUpdateUserId = userId;
                            mutableObj.LastUpdateTime = nowTime;
                        }
                    }
                }

                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    var errorMessage = String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    Debug.WriteLine(errorMessage);
                    _logger.Log(LogLevel.Error, errorMessage);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        var propertyMessage = String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        _logger.Log(LogLevel.Error, propertyMessage);
                        Debug.WriteLine(propertyMessage);
                    }
                }
                throw;
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.Log(LogLevel.Error, e.Message, e);
                throw new Exception("该记录已被其他用户修改或删除，请刷新页面后重试。");
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message, e);
                if (e.InnerException != null)
                {
                    _logger.Log(LogLevel.Error, e.InnerException.Message, e.InnerException);
                }
                throw;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
