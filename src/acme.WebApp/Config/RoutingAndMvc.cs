using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    public static class RoutingAndMvc
    {
        public static IRouteBuilder UseCustomRoutes(this IRouteBuilder routes, bool useFolders)
        {
            routes.AddCloudscribeFileManagerRoutes();
            if (useFolders)
            {
                routes.MapRoute(
                    name: "foldererrorhandler",
                    template: "{sitefolder}/oops/error/{statusCode?}",
                    defaults: new { controller = "Oops", action = "Error" },
                    constraints: new { name = new cloudscribe.Core.Web.Components.SiteFolderRouteConstraint() }
                );

                routes.MapRoute(
                       name: "apifoldersitemap",
                       template: "{sitefolder}/api/sitemap"
                       , defaults: new { controller = "FolderSiteMap", action = "Index" }
                       , constraints: new { name = new cloudscribe.Core.Web.Components.SiteFolderRouteConstraint() }
                       );

                routes.MapRoute(
                    name: "folderdefault",
                    template: "{sitefolder}/{controller}/{action}/{id?}",
                    defaults: new { action = "Index" },
                    constraints: new { name = new cloudscribe.Core.Web.Components.SiteFolderRouteConstraint() }
                    );
            }
            routes.MapRoute(
                name: "errorhandler",
                template: "oops/error/{statusCode?}",
                defaults: new { controller = "Oops", action = "Error" }
                );



            routes.MapRoute(
                name: "def",
                template: "{controller}/{action}"
                , defaults: new { controller = "Home", action = "Index" }
                );

            return routes;
        }

        public static IServiceCollection SetupMvc(
            this IServiceCollection services,
            bool sslIsAvailable
            )
        {
            services.Configure<MvcOptions>(options =>
            {
                if (sslIsAvailable)
                {
                    options.Filters.Add(new RequireHttpsAttribute());
                }

                options.CacheProfiles.Add("SiteMapCacheProfile",
                     new CacheProfile
                     {
                         Duration = 30
                     });

            });

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddMvc()
                .AddRazorOptions(options =>
                {
                    options.ViewLocationExpanders.Add(new cloudscribe.Core.Web.Components.SiteViewLocationExpander());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return services;
        }

    }
}
