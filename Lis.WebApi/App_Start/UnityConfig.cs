using Lis.Common.Logger;
using Lis.EntityFramework.Implements;
using Lis.EntityFramework.Interfaces;
using Lis.Service;
using Lis.Service.ServiceImpl;
using System;
using System.Configuration;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Lis.WebApi
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container
        {
            get
            {
                return container.Value;
            }
        }
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            container.RegisterType<ISystemLogger, SystemLogger>(new ContainerControlledLifetimeManager());
            container.RegisterType<IInitialDataService, InitialDataService>(new ContainerControlledLifetimeManager());

            var connectionString = ConfigurationManager.ConnectionStrings["LisContext"].ConnectionString;
            container.RegisterType<ILisContext, LisContext>(new InjectionConstructor(connectionString, container.Resolve<ISystemLogger>(), container.Resolve<IInitialDataService>()));

            container.RegisterType<ISystemService, SystemService>();
            container.RegisterType<ILisService, LisService>();
            container.RegisterType<IUserService, UserService>();
        }
    }
}