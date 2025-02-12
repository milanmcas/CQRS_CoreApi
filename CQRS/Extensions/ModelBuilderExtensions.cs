using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City
                {
                    Id = 1,
                    Name = "William"                    
                }
            );
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, CourseName = "Hamlet" },
                new Book { Id = 2,  CourseName = "King Lear" },
                new Book { Id = 3,  CourseName = "Othello" }
            );
        }
    }
}
