using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ACERFLORIN\\SQL2022SIIT;Database=CodingWikiData;TrustServerCertificate=True;Trusted_Connection=True");
        }

        //
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasPrecision(10,5); // Set the precision and scale for the Price column

            //---1    seeding table Books:---
            modelBuilder.Entity<Book>()
                .HasData(
                    new Book { BookId = 1, Title = "Spider without duty", ISBN = "123B12", Price = 10.99m },
                    new Book { BookId = 2, Title = "Fortune of time", ISBN = "12123B12", Price = 11.99m }
                );

            //---2    other way to seed in table Books:---
            var bookList = new Book[]
            {
                new Book { BookId = 3, Title = "Fake Sunday", ISBN = "773B12", Price = 20.99m },
                new Book { BookId = 4, Title = "Cookie Jar", ISBN = "CC3B12", Price = 25.99m },
                new Book { BookId = 5, Title = "Cloudy forest", ISBN = "XV4B12", Price = 40.99m }
            };

            modelBuilder.Entity<Book>().HasData(bookList);
        }
    }
}
