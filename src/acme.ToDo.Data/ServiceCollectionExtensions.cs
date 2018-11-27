using acme.ToDo.Data;
using acme.ToDo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddToDoEFStorageMSSQL(
            this IServiceCollection services,
            string connectionString,
            int maxConnectionRetryCount = 0,
            int maxConnectionRetryDelaySeconds = 30,
            ICollection<int> transientSqlErrorNumbersToAdd = null,
            bool useSql2008Compatibility = false
            )
        {  
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<ToDoDbContext>(options =>
                    options.UseSqlServer(connectionString,
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            if (maxConnectionRetryCount > 0)
                            {
                                //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                sqlOptions.EnableRetryOnFailure(
                                    maxRetryCount: maxConnectionRetryCount,
                                    maxRetryDelay: TimeSpan.FromSeconds(maxConnectionRetryDelaySeconds),
                                    errorNumbersToAdd: transientSqlErrorNumbersToAdd);
                            }

                            if (useSql2008Compatibility)
                            {
                                sqlOptions.UseRowNumberForPaging();
                            }

                        }),
                        optionsLifetime: ServiceLifetime.Singleton
                        );

            services.AddScoped<ToDoDbContext>();
            services.AddSingleton<ToDoDbContextFactory>();
            services.AddScoped<IToDoQueries, ToDoQueries>();
            services.AddScoped<IToDoCommands, ToDoCommands>();

            return services;
        }

    }
}
