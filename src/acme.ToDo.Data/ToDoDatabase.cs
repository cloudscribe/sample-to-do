using acme.ToDo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Hosting //this namespace is used so it will show up in Program.cs without a using 
{
    public static class ToDoDatabase
    {
        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<ToDoDbContext>();
            await db.Database.MigrateAsync();

            // You could also seed initial data here
        }

    }
}
