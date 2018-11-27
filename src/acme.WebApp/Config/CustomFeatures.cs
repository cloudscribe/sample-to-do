using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CustomFeatures
    {
        public static IServiceCollection AddCustomFeatures(
            this IServiceCollection services,
            IConfiguration config
            )
        {
            var connectionString = config.GetConnectionString("EntityFrameworkConnection");

            services.AddToDoEFStorageMSSQL(connectionString);
            services.AddToDoServices();


            return services;
        }
    }
}
