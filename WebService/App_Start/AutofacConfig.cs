using Autofac;
using Autofac.Configuration;
using Autofac.Integration.WebApi;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace WebService.App_Start
{
    /// <summary>
    /// Represent Autofac configuration.
    /// </summary>
    public class AutofacConfig
    {
        /// <summary>
        /// Configured instance of <see cref="IContainer"/>
        /// <remarks><see cref="AutofacConfig.Configure"/> must be called before trying to get Container instance.</remarks>
        /// </summary>
        protected internal static IContainer Container;

        /// <summary>
        /// Initializes and configures instance of <see cref="IContainer"/>.
        /// </summary>
        /// <param name="config"></param>
        public static void Configure(HttpConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            var builder = new ContainerBuilder();

            // Register your Web API controllers.
            Assembly[] controllersAssemblies = GetAssemblies(true);
            builder.RegisterApiControllers(controllersAssemblies).PropertiesAutowired();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();

            Assembly[] otherAssemblies = GetAssemblies(false);
            foreach (var assembly in otherAssemblies)
            {
                Assembly.Load(assembly.FullName);
            }

            RegisterServices(builder);

            Container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }

#pragma warning disable RECS0154 // Parameter is never used
        private static void RegisterServices(ContainerBuilder builder)
#pragma warning restore RECS0154 // Parameter is never used
        {
            // TODO: Register additional services for injection
            // For more information see https://github.com/drwatson1/AspNet-WebApi/wiki#dependency-injection
            var config = new ConfigurationBuilder();
            config.AddJsonFile("autofac.json");
            var module = new ConfigurationModule(config.Build());

            builder.RegisterModule(module);
        }

        private static Assembly[] GetAssemblies(bool isController)
        {
            var path = HttpContext.Current.Server.MapPath("~/bin");

            return isController
                ? Directory.GetFiles(path, "*.dll").Where(x => x.Contains("Controllers")).Select(Assembly.LoadFrom).ToArray()
                : Directory.GetFiles(path, "*.dll").Where(x => !x.Contains("Controllers")).Select(Assembly.LoadFrom).ToArray();
        }
    }
}