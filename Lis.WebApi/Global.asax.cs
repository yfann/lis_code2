using Lis.Common.Logger;
using Lis.EntityFramework.Implements;
using Lis.EntityFramework.Interfaces;
using Lis.Service.ServiceImpl;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;

namespace Lis.WebApi
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMigrationDbToLatestVersion();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Re‌ferenceLoopHandling = ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
        }

        private void AutoMigrationDbToLatestVersion()
        {
            var container = UnityConfig.Container;
            var logger = new SystemLogger();
            logger.Log(LogLevel.Info, "Executing data migration...");

            try
            {
                var context = (LisContext)container.Resolve<ILisContext>();
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<LisContext, LisDbMigrationsConfiguration>(true));
                context.Database.Initialize(true);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, string.Format("MigrateDatabaseToLatestVersion  Failed message: {0}, StackTrace：{1}", ex.Message, ex.StackTrace));
            }

            logger.Log(LogLevel.Info, "Data migration executed.");
        }
    }
}
