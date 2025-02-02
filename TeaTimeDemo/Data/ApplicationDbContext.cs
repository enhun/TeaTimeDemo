using Microsoft.EntityFrameworkCore;
using TeaTimeDemo.Models;

namespace TeaTimeDemo.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Black Tea", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Green Tea", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Herbal Tea", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Oolong Tea", DisplayOrder = 4 },
                new Category { Id = 5, Name = "White Tea", DisplayOrder = 5 },
                new Category { Id = 6, Name = "Yellow Tea", DisplayOrder = 6 }
            );
        }
    }


}
