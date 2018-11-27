using Microsoft.EntityFrameworkCore;

namespace acme.ToDo.Data
{
    public class ToDoDbContextFactory
    {
        public ToDoDbContextFactory(DbContextOptions<ToDoDbContext> options)
        {
            _options = options;
        }

        private readonly DbContextOptions<ToDoDbContext> _options;

        public ToDoDbContext CreateContext()
        {
            return new ToDoDbContext(_options);
        }

    }
}
