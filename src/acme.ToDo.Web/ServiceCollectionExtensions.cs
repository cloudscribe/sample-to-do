using acme.ToDo.Web.Services;
using cloudscribe.Core.Models.EventHandlers;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddToDoServices(
            this IServiceCollection services)
        {
            services.AddScoped<ToDoService>();
            services.AddScoped<IHandleSitePreDelete, SitePreDeleteHandler>();
            services.AddScoped<IHandleUserPreDelete, UserPreDeleteHandler>();

            return services;
        }

    }
}
