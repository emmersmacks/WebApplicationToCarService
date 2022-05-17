using Microsoft.EntityFrameworkCore;
using WebApplicationToCarService.Structures;

namespace WebApplicationToCarService
{
    public class DataBaseApplication : DbContext
    {
        public DbSet<Auto> autoList { get; set; } = null!;

        public DataBaseApplication(DbContextOptions<DataBaseApplication> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auto>().HasData(
                new Auto { Id = 1, AutoNumber = "A123PR", Brand = "Volvo", Color = "Red", ReleaseDate = DateTime.UtcNow },
                new Auto { Id = 2, AutoNumber = "A233PR", Brand = "Sena", Color = "blue", ReleaseDate = DateTime.UtcNow },
                new Auto { Id = 3, AutoNumber = "A111PR", Brand = "Renoult", Color = "black", ReleaseDate = DateTime.UtcNow },
                new Auto { Id = 4, AutoNumber = "A143PR", Brand = "Volvo", Color = "white", ReleaseDate = DateTime.UtcNow }
            );
        }
    }
}
