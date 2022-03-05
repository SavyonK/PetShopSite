using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace PetShop.Data
{
    public class PetContext : DbContext
    {
        public PetContext(DbContextOptions<PetContext> options) : base(options)
        { }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comments> CommentsDbSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Animal>().Property(prop => prop.AnimalID).UseIdentityColumn(7, 1);
            modelBuilder.Entity<Comments>().Property(prop => prop.CommentsID).UseIdentityColumn(8, 1);
          

            modelBuilder.Entity<Category>().HasData(
                new { CategoryID = 1, Name = "Mammal" },
                new { CategoryID = 2, Name = "Reptile" },
                new { CategoryID = 3, Name = "Aquatic" }               
                ) ;

            modelBuilder.Entity<Animal>().HasData(
                new { AnimalID = 1, Name = "Cat", Age = 2, ProfileImage = "GlassOfMilk.jpeg", Description = "The liquid type", CategoryID = 1 },
                new { AnimalID = 2, Name = "People After Quarantine", Age = 1, ProfileImage = "PeopleAfterQuarantine.png", Description = "Diet starts tomorrow",  CategoryID = 1 },
                new { AnimalID = 3, Name = "Goat", Age = 3, ProfileImage = "GulabiGoat.jpeg", Description = "Such stylish ears", CategoryID = 1 },
                new { AnimalID = 4, Name = "Axlotol", Age = 3, ProfileImage = "Axolotl.jpeg", Description = "your new best friend", CategoryID = 3 },
                new { AnimalID = 5, Name = "Snake", Age = 3, ProfileImage = "Snake.jpeg", Description = "Likes hats", CategoryID = 2 },
                new { AnimalID = 6, Name = "Farret", Age = 5, ProfileImage = "Farret.jpeg", Description = "Likes to swim", CategoryID = 1 }
                ) ;
           

            modelBuilder.Entity<Comments>().HasData(
                new { CommentsID=1,Comment="This cat is milk",AnimalID=1},
                new { CommentsID=2,Comment="No soup for you!",AnimalID=2},
                new { CommentsID=3,Comment="3rd comment",AnimalID=2},
                new { CommentsID=4,Comment="4th comment",AnimalID=2},
                new { CommentsID = 5, Comment = "If dumbo was a goat", AnimalID = 3 },
                new { CommentsID = 6, Comment = "Parachute ears", AnimalID = 3 },
                new { CommentsID = 7, Comment = "Look at this distinguished gentelman", AnimalID = 5 }

                               
                );



        }

    }

}
