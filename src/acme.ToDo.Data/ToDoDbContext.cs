using acme.ToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace acme.ToDo.Data
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
        {

        }

        public DbSet<ToDoItem> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ToDoItem>(entity =>
            {
                entity.ToTable("acme_ToDoItems");
                entity.HasKey(p => p.Id);
                
                entity.HasIndex(x => x.SiteId);
                entity.HasIndex(x => x.UserId);

            });

        }
    }
}
