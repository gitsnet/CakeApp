using CakeApp.API.Resolver;
using Core.Data;
using Data;
using Microsoft.Practices.Unity;
using Service.Category;
using Service.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CakeApp.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            var container = new UnityContainer();
            container.RegisterType(typeof(IRepository<>), typeof(EfRepository<>));
            container.RegisterType<IDbContext, ObjectContext>(new HierarchicalLifetimeManager());
            container.RegisterType<ICategoriesService, CategoriesService>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductsService, ProductsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductPriceService, ProductPriceService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

          

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.EnableCors();
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.DependencyResolver = new UnityResolver(container);
            config.MapHttpAttributeRoutes();
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
