using AutoMapper;
using Lis.Common.Extensions;
using System;
using System.Linq;
using System.Reflection;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Lis.Service.Startup), "AutoMapperStart")]
namespace Lis.Service
{
     public class Startup
    {
        public static void AutoMapperStart()
        {
            var profiles = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(Profile).IsAssignableFrom(t) && t.GetConstructor(Type.EmptyTypes) != null)
                .Select(Activator.CreateInstance).Cast<Profile>().ToList();
            foreach (var profile in profiles)
            {
                Mapper.AddProfile(profile);
            }
            AttributeMap();
        }

        public static void AttributeMap()
        {
            var autoMapType = typeof(AutoMapAttribute);
            var types = typeof(Startup).Assembly.GetTypes().Where(t => t.IsDefined(autoMapType, false));
            foreach (var t in types)
            {
                Mapper.CreateProfile(t.Name.TrimEnd("Dto") + "Mapper", expression =>
                {
                    var attr = (AutoMapAttribute[])t.GetCustomAttributes(autoMapType, false);
                    if (attr.Length > 0)
                    {
                        expression.CreateMap(t, attr[0].TargetType);
                        expression.CreateMap(attr[0].TargetType, t);
                    }
                });
            };
        }
    }
}
