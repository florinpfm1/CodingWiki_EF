using CodingWiki_DataAccess.FluentConfig;
using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        //for model classes that use Data Attributes
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        //Db Set for intermediary mapping table - is not really needed, only if we want to retrieve some data from it
        public DbSet<BookAuthorMap> BookAuthorMaps { get; set; }



        //for model classes having Data Attributes replaced by Fluent API config in OnModelCreating method
        public DbSet<Fluent_BookDetail> BookDetails_fluent { get; set; }
        public DbSet<Fluent_Book> Fluent_Books { get; set; }
        public DbSet<Fluent_Author> Fluent_Authors { get; set; }
        public DbSet<Fluent_Publisher> Fluent_Publishers { get; set; }
        //Db Set for intermediary mapping table - is not really needed, only if we want to retrieve some data from it
        public DbSet<Fluent_BookAuthorMap> Fluent_BookAuthorMaps { get; set; }


        public DbSet<MainBookDetails> MainBookDetails { get; set; }




        //CTOR - the DbContext builder options with the connection string will be passed from the Startup.cs file through CTOR here
        //options we need to pass to base Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }




        //here we override the DbContext config and we impose out custom config from where to take the connection string for db
        //is not needed since we registered DbContext in Program.cs file
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=ACERFLORIN\\SQL2022SIIT;Database=CodingWikiData;TrustServerCertificate=True;Trusted_Connection=True")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information); //this will log to Console all queries/commands that EF Core is doing towards SQL Server db
        }
        */

        //
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FluentAuthorConfig());
            modelBuilder.ApplyConfiguration(new FluentBookAuthorMapConfig());
            modelBuilder.ApplyConfiguration(new FluentBookConfig());
            modelBuilder.ApplyConfiguration(new FluentBookDetailConfig());
            modelBuilder.ApplyConfiguration(new FluentPublisherConfig());



            // In manual created intermediary table BookAuthorMap for Many-To-Many
            // Set the PK as composite key from the 2 columns FK's existing in this table
            modelBuilder.Entity<BookAuthorMap>()
                .HasKey(u => new { u.Author_Id, u.Book_Id });
            

            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasPrecision(10, 5); // Set the precision and scale for the Price column



            //---1    seeding table Books:---
            modelBuilder.Entity<Book>()
                .HasData(
                    new Book { BookId = 1, Title = "Spider without duty", ISBN = "123B12", Price = 10.99m, Publisher_Id=1 },
                    new Book { BookId = 2, Title = "Fortune of time", ISBN = "12123B12", Price = 11.99m, Publisher_Id = 1 }
                );
            //---1    seeding table Books:---

            //---2    other way to seed in table Books:---
            var bookList = new Book[]
            {
                new Book { BookId = 3, Title = "Fake Sunday", ISBN = "773B12", Price = 20.99m, Publisher_Id=2 },
                new Book { BookId = 4, Title = "Cookie Jar", ISBN = "CC3B12", Price = 25.99m, Publisher_Id=3 },
                new Book { BookId = 5, Title = "Cloudy forest", ISBN = "XV4B12", Price = 40.99m, Publisher_Id=3 }
            };

            modelBuilder.Entity<Book>().HasData(bookList);
            //---2    other way to seed in table Books:---

            //---seeding table Publisher
            modelBuilder.Entity<Publisher>()
                .HasData(
                    new Publisher { Publisher_Id = 1, Name = "Pub 1 Jimmy", Location = "Chicago" },
                    new Publisher { Publisher_Id = 2, Name = "Pub 2 John", Location = "New York" },
                    new Publisher { Publisher_Id = 3, Name = "Pub 3 Ben", Location = "Hawaii" }
                );
            //---seeding table Publisher


            modelBuilder.Entity<MainBookDetails>()
                .HasNoKey()  //this tells EF that it will have no Primary Key
                             //also this makes that this entity will not be tracked by EF Core, all we retrieve from this View will be read-only
                .ToView("GetMainBookDetails"); //this tells EF that we don't want to create a table in db for this entity, but only to have a view
        }
    }
}
