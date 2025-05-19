using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace CodingWiki_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //--- Helper methods for working with EF Core ---
            /*
            using(ApplicationDbContext context = new())
            {
                //this will check if the database exists in the server host and if not it will create it
                context.Database.EnsureCreated();
                //check if we have pending migrations that were not yet pushed to db
                if(context.Database.GetPendingMigrations().Count() > 0)
                {
                    Console.WriteLine("Pending migrations exist");
                    foreach (var item in context.Database.GetPendingMigrations())
                    {
                        Console.WriteLine(item);
                    }

                    //this will apply the pending migrations to the database
                    context.Database.Migrate();
                }
                else
                {
                    Console.WriteLine("No pending migrations");
                }
            }
            */

            //--- Insert a new Book into Books table using EF Core ---
            //AddBook();

            //--- Retrieve data from Books table using EF Core ---
            //GetAllBooks();

            //--- Retrieve a single Book from Books table using EF Core ---
            //GetBook();

            //--- Updates a single Book from Books table using EF Core ---
            //UpdateBook();

            //--- Deletes a single Book from Books table using EF Core ---
            //DeleteBook();

        }

        
        private static void DeleteBook()
        {
            //we create the DbContext used to retrieve the data from db
            //using var context = new ApplicationDbContext();

            //to delete any record first we need to retrieve it from db using EF Core
            //once a record is retrieved it will be tracked by EF Core
            /*
            try
            {
                var book = context.Books.Find(8); //here we retrieve the record we want to update
                context.Books.Remove(book); //this will mark the record for deletion
                context.SaveChanges(); //this will delete the record in db - will delete all records being tracked for deletion
            }
            catch (Exception e)
            {
                throw;
            }
            */
        }
        
        
        private static async void DeleteBookAsync()
        {
            //we create the DbContext used to retrieve the data from db
            //using var context = new ApplicationDbContext();

            //to delete any record first we need to retrieve it from db using EF Core
            //once a record is retrieved it will be tracked by EF Core
            /*
            try
            {
                var book = await context.Books.FindAsync(8); //here we retrieve the record we want to update
                context.Books.Remove(book); //this will mark the record for deletion
                await context.SaveChangesAsync(); //this will delete the record in db - will delete all records being tracked for deletion
            }
            catch (Exception e)
            {
                throw;
            }
            */
        }

        private static void UpdateBook()
        {
            //we create the DbContext used to retrieve the data from db
            //using var context = new ApplicationDbContext();

            //to update any record first we need to retrieve it from db using EF Core
            //once a record is retrieved it will be tracked by EF Core
            /*
            try
            {
                var book = context.Books.Find(7); //here we retrieve the record we want to update
                book.ISBN = "777000"; //this will mark the record for updating
                context.SaveChanges(); //this will update the record in db - will update all records being tracked and which have been modified
            }
            catch (Exception e)
            {
                throw;
            }
            */
        }

        private static async void UpdateBooksAsync()
        {
            //we create the DbContext used to retrieve the data from db
            //using var context = new ApplicationDbContext();

            //to update any record first we need to retrieve it from db using EF Core
            //once a record is retrieved it will be tracked by EF Core
            /*
            try
            {
                var books = await context.Books.Where(u=>u.Publisher_Id==1).ToListAsync(); //here we retrieve the record we want to update
                //this will mark the record for updating
                foreach (var book in books)
                {
                    book.Price = 55.88m;
                }
                await context.SaveChangesAsync(); //this will update the record in db - will update all records being tracked and which have been modified
            }
            catch (Exception e)
            {
                throw;
            }
            */
        }

        private static void GetBook()
        {
            //we create the DbContext used to retrieve the data from db
            //using var context = new ApplicationDbContext();

            //--- .First() ---
            //this will retrieve the first book from the Books table
            //var book = context.Books.First();
            //Console.WriteLine(book.Title + " - " + book.ISBN);


            //this will retrieve the first book from the Books table - when table is empty will throw an exception which we can catch with try-catch
            /*
            try
            {
                var book = context.Fluent_Books.First();
                Console.WriteLine(book.Title + " - " + book.ISBN);
            }
            catch (Exception e)
            {
                throw;
            }
            */

            //--- .FirstOrDefault() ---
            //this will retrieve the first book from the Books table - when table is empty here does NOT throw exception, instead just return the default value
            //var book will be null if db table is empty
            /*
            try
            {
                var book = context.Books.FirstOrDefault();
                Console.WriteLine(book.Title + " - " + book.ISBN);
            }
            catch (Exception e)
            {
                throw;
            }
            */


            //this will retrieve the first book from the Books table - when table is empty here does NOT throw exception, instead just return the default value
            //var book will be null if db table is empty
            /*
            try
            {
                var book = context.Fluent_Books.FirstOrDefault();
                Console.WriteLine(book.Title + " - " + book.ISBN);
            }
            catch (Exception e)
            {
                throw;
            }
            */

            //--- .Where() to filter to some specific Books, this retrieves multiple records based on the filter condition ---
            //this returns an -->> IQueryable<Book> books
            /*
            try
            {
                var books = context.Books.Where(u => u.Publisher_Id == 3);
                foreach (var book in books)
                {
                    Console.WriteLine(book.Title + " - " + book.ISBN);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            */

            //--- .Where() to filter to some specific Books, then FirstOrDefault() retrieve first record from retrieved records, or default value if no records were found in db table ---
            //this returns an -->> Book book
            /*
            try
            {
                var book = context.Books.Where(u => u.Publisher_Id == 3).FirstOrDefault();
                Console.WriteLine(book.Title + " - " + book.ISBN);
            }
            catch (Exception e)
            {
                throw;
            }
            */

            //--- .Where() to filter based on multiple conditions, then retrieve first record from retrieved records, or default value if no records were found in db table ---
            /*
            try
            {
                var book = context.Books.Where(u => u.Publisher_Id == 3 && u.Price > 30).FirstOrDefault();
                Console.WriteLine(book.Title + " - " + book.ISBN);
            }
            catch (Exception e)
            {
                throw;
            }
            */

            //--- with only .Where() it returns IQuerable<Book> ---
            /*
            try
            {
                var book = context.Books.Where(u => u.Title == "Cookie Jar");
                Console.WriteLine(book.Title + " - " + book.ISBN); //this will throw exception because book is IQueryable<Book> and not Book, so it cannot find props Title or ISBN in IQuerable<Book>
            }
            catch (Exception e)
            {
                throw;
            }
            */

            //--- with filtering condition passed inside FirstOrDefault() ---  <<<=== NOT so secure
            //this returns an -->> Book book
            /*
            try
            {
                var book = context.Books.FirstOrDefault(u => u.Title == "Cookie Jar");
                Console.WriteLine(book.Title + " - " + book.ISBN);
            }
            catch (Exception e)
            {
                throw;
            }
            */

            //--- with filtering condition passed inside FirstOrDefault() ---  <<<=== MORE secure with string values passed as var input
            //this returns an -->> Book book
            /*
            try
            {
                var input = "Cookie Jar";
                var book = context.Books.FirstOrDefault(u => u.Title == input);
                Console.WriteLine(book.Title + " - " + book.ISBN);
            }
            catch (Exception e)
            {
                throw;
            }
            */

            //--- Find() searches based on PK of the record ---
            //this returns an -->> Book book
            /*
            try
            {
                var book = context.Books.Find(7); //this will search for the book with BookId = 7, BookId is the PK for the table Books
                Console.WriteLine(book.Title + " - " + book.ISBN);
            }
            catch (Exception e)
            {
                throw;
            }
            */

            //--- Single() returns a single record from db table ---
            //this returns an -->> Book book (does not return an IQuerable<Book>)
            /*
            try
            {
                var book = context.Books.Single(u => u.BookId == 7);
                Console.WriteLine(book.Title + " - " + book.ISBN);
            }
            catch (Exception e)
            {
                throw;
            }
            */

            //--- SingleOrDefault() returns a single record from db table ---
            //this returns an -->> Book book (does not return an IQuerable<Book>)
            /*
            try
            {
                var book = context.Books.SingleOrDefault(u => u.BookId == 7);
                Console.WriteLine(book.Title + " - " + book.ISBN);
            }
            catch (Exception e)
            {
                throw;
            }
            */


            //--- .Contains() retrieves multiple records that all contain a substring "aaa" ---
            //this returns an -->> IQueryable<Book> books

            /*try
            {
                var books = context.Books.Where(u => u.ISBN.Contains("12"));
                foreach (var book in books)
                {
                    Console.WriteLine(book.Title + " - " + book.ISBN);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            */

            //--- .Like() retrieves multiple records that all start with a substring "aaa" ---
            //this returns an -->> IQueryable<Book> books
            /*
            try
            {
                var books = context.Books.Where(u => EF.Functions.Like(u.ISBN, "12%"));
                foreach (var book in books)
                {
                    Console.WriteLine(book.Title + " - " + book.ISBN);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            */

            //--- .Max() retrieves single column value that has the max value from records in db table ---
            //this returns an -->> decimal price
            /*
            try
            {
                var price = context.Books.Max(u => u.Price);
            }
            catch (Exception e)
            {
                throw;
            }
            */

            //--- .Min() retrieves single column value that has the min value from records in db table ---
            //this returns an -->> decimal price

            /*try
            {
                var price = context.Books.Min(u => u.Price);
            }
            catch (Exception e)
            {
                throw;
            */

            //--- .Count() will return an integer representing the number of found records through the filering condition ---
            //this returns an -->> integer count
            /*
            try
            {
                var count = context.Books.Where(u => EF.Functions.Like(u.ISBN, "12%")).Count();
            }
            catch (Exception e)
            {
                throw;
            }
            */

            //--- Deferred Execution in EF Core ---
            //here the SQL query/command sent to db is NOT executed when this line of code "var books = context.Books;" is processed
            //the execution will happen when we do smth. with var books in line of code "foreach (var book in books)"
            /*
            try
            {
                var books = context.Books;
                foreach (var book in books)
                {
                    Console.WriteLine(book.Title + " - " + book.ISBN);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            */

            //--- .OrderBy() used to order asc or desc the records retrieved from db ---
            /*
            try
            {
                var books = context.Books.OrderBy(u => u.Title); //by default will order the records retrieved in ASCENDING order
                var books2 = context.Books.OrderBy(u => u.Title).OrderByDescending(u => u.ISBN); //here PROBLEM: first OrderBY() is ignored and only second OrderByDescending() is executed !!!
                var books3 = context.Books.OrderBy(u => u.Title).ThenByDescending(u => u.ISBN); //orders first by Title ASC and then if we have duplicated titles orders by ISBN DESC
                var books4 = context.Books.Where(u => u.Price > 10).OrderBy(u => u.Title).ThenByDescending(u => u.ISBN);
                foreach (var book in books)
                {
                    Console.WriteLine(book.Title + " - " + book.ISBN);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            */

            //--- Pagination ---
            /*
            try
            {
                var books = context.Books.Skip(0).Take(2);
                foreach (var book in books)
                {
                    Console.WriteLine(book.Title + " - " + book.ISBN);
                }

                books = context.Books.Skip(4).Take(1);
                foreach (var book in books)
                {
                    Console.WriteLine(book.Title + " - " + book.ISBN);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            */



        }

        public static void GetAllBooks()
        {
            //we create the DbContext used to retrieve the data from db
            //using var context = new ApplicationDbContext();
            //note: in next line execution of SQL query/command was right away because we used First(), FirstOrDefault(), Single(), SingleOrDefault(), ToList()
            /*
            var books = context.Books.ToList();
            foreach (var book in books)
            {
                Console.WriteLine(book.Title + " - " + book.ISBN);
            }
            */
        }

        public static async void GetAllBooksAsync()
        {
            //we create the DbContext used to retrieve the data from db
            //using var context = new ApplicationDbContext();
            //note: in next line execution of SQL query/command was right away because we used First(), FirstOrDefault(), Single(), SingleOrDefault(), ToList()
            /*
            var books = await context.Books.ToListAsync();
            foreach (var book in books)
            {
                Console.WriteLine(book.Title + " - " + book.ISBN);
            }
            */
        }

        public static void AddBook()
        {
            //Book book = new() { Title="New EF Core Book", ISBN = "99999", Price = 10.93m, Publisher_Id = 1 };
            //we create the DbContext used to retrieve the data from db
            //using var context = new ApplicationDbContext();
            /*
            var books = context.Books.Add(book);
            context.SaveChanges();
            */
        }

        public static async void AddBookAsync()
        {
            //Book book = new() { Title = "New EF Core Book", ISBN = "99999", Price = 10.93m, Publisher_Id = 1 };
            //we create the DbContext used to retrieve the data from db
            //using var context = new ApplicationDbContext();
            /*
            var books = await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
            */
        }

        
    }
}
