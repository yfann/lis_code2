using Lis.Domain.Entities;
using Lis.EntityFramework.Implements;
using Lis.WebApi.Filters;
using Lis.WebApi.Utils;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Z.EntityFramework.Plus;
using AutoMapper;
using Lis.Service;
using System.Web.Http.Dispatcher;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using CommonServiceLocator;
using Unity.ServiceLocation;

namespace Lis.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                   name: "API Default",
                   routeTemplate: "api/{controller}/{action}/{id}",
                   defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                }
            );
            config.EnableSystemDiagnosticsTracing();

            // conert result to camlCase
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // all the datetime seserialization should be local time, here we did not concern utc time for now
            // if we do not add this handling, the datetime will be serialized as utc time, but in everywhere we use them as local time
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;

            AuditManager.DefaultConfiguration.IgnoreRelationshipAdded = true;
            AuditManager.DefaultConfiguration.IgnoreRelationshipDeleted = true;
            AuditManager.DefaultConfiguration.IgnorePropertyUnchanged = true;
            AuditManager.DefaultConfiguration.IgnoreEntitySoftAdded = true;
            AuditManager.DefaultConfiguration.IgnoreEntitySoftDeleted = true;

            // IoC configuration
            var container = UnityConfig.Container;
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
            config.DependencyResolver = new UnityResolver(container);



        }
    }
}
