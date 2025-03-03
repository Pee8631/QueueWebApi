using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using QueueWebApi.Models;

namespace QueueWebApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Queue> Queue { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Queue>(entity =>
            {
                entity.HasData(new Queue { id = 1, QueueNumber = "00", createdAt = DateTime.Now, updatedAt = DateTime.Now });
            });
        }
    }
}
