using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace acme.ToDo.Data
{
    public class DesignTimeFactory : IDesignTimeDbContextFactory<ToDoDbContext>
    {
        public ToDoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ToDoDbContext>();
            builder.UseSqlServer("Server=(local);Database=DATABASENAME;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new ToDoDbContext(builder.Options);
        }

    }
}
