using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using CodingWiki_Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace CodingWiki_Web.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IQueryable<Book> objList = _db.Books
                .Include(u => u.Publisher)
                .Include(u=>u.BookAuthorMap).ThenInclude(u=>u.Author);

            var temp = objList.Where(u=>u.BookId == 1).ToList();



            //this does same as above with Eager Loading, but is less efficient
            /*
            List<Book> objList = _db.Books.ToList();
            foreach(var obj in objList)
            {
                _db.Entry(obj).Reference(u => u.Publisher).Load();
                _db.Entry(obj).Collection(u => u.BookAuthorMap).Load();
                foreach(var bookAuth in obj.BookAuthorMap)
                {
                    _db.Entry(bookAuth).Reference(u => u.Author).Load();

                }

            }
            */

            return View(objList);
        }

        
        public IActionResult Upsert(int? id)
        {
            BookVM obj = new();
            //here we use projection in EF Core to create a collection of anonymous objects named "i" which will retrieve only props Name and Publisher_id from Publishers table in db
            obj.PublisherList = _db.Publishers.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Publisher_Id.ToString()
            });
            if ( id == null || id == 0)
            {
                //create
                return View(obj);
            }
            //edit
            obj.Book = _db.Books.FirstOrDefault(u=>u.BookId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(BookVM obj)
        {
            if (obj.Book.BookId == 0)
            {
                //create
                await _db.Books.AddAsync(obj.Book);
            }
            else
            {
                //update
                _db.Books.Update(obj.Book);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //create
            BookDetail obj = new();
            
            //edit
            obj = _db.BookDetails.Include(u=>u.Book).FirstOrDefault(u => u.Book_Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(BookDetail obj)
        {
            if (obj.BookDetail_Id == 0)
            {
                //create
                await _db.BookDetails.AddAsync(obj);
            }
            else
            {
                //update
                _db.BookDetails.Update(obj);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Book obj = new();
            obj = _db.Books.FirstOrDefault(u => u.BookId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Books.Remove(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ManageAuthors(int id)
        {
            BookAuthorVM obj = new()
            {
                BookAuthorList = _db.BookAuthorMaps.Include(u=>u.Author).Include(u=>u.Book).Where(u => u.Book_Id == id).ToList(),
                BookAuthor = new()
                {
                    Book_Id = id
                },
                Book = _db.Books.FirstOrDefault(u=>u.BookId == id)
            };

            List<int> tempListOfAssignedAuthors = obj.BookAuthorList.Select(u=>u.Author_Id).ToList();

            //NOT IN clause
            //get all the authors whos id is NOT in the tempListOfAssignedAuthors

            var tempList = _db.Authors.Where(u => !tempListOfAssignedAuthors.Contains(u.Author_Id)).ToList();
            obj.AuthorList = tempList.Select(i => new SelectListItem()
            {
                Text = i.FullName,
                Value = i.Author_Id.ToString()
            });

            return View(obj);
        }

        [HttpPost]
        public IActionResult ManageAuthors (BookAuthorVM bookAuthorVM)
        {
            if(bookAuthorVM.BookAuthor.Book_Id != 0 && bookAuthorVM.BookAuthor.Author_Id != 0)
            {
                _db.BookAuthorMaps.Add(bookAuthorVM.BookAuthor);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorVM.BookAuthor.Book_Id });
        }

        [HttpPost]
        public IActionResult RemoveAuthors(int authorId, BookAuthorVM bookAuthorVM)
        {
            int bookId = bookAuthorVM.Book.BookId;
            BookAuthorMap bookAuthorMap = _db.BookAuthorMaps.FirstOrDefault(
                u=>u.Author_Id == authorId && u.Book_Id == bookId);

            
            _db.BookAuthorMaps.Remove(bookAuthorMap);
            _db.SaveChanges();
            
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookId });
        }

        public async Task<IActionResult> Playground()
        {
            //#####--- Deffered Execution ---#####
            /*
            //because we have here .FirstOrDefault() the retrieval code is executed right away -> this means call to db is made right away
            var bookTemp = _db.Books.FirstOrDefault();
            bookTemp.Price = 100;

            //here the retrieval code is NOT executed yet -> this means call to db is NOT made yet
            var bookCollection = _db.Books;
            decimal totalPrice = 0;
            //so actually here when it needs to do something with the data inside var bookCollection
            //it will execute the call to db and retrieve the books
            foreach (var book in bookCollection)
            {
                totalPrice += book.Price;
            }

            //because we have here .ToList() the retrieval code is executed right away -> this means call to db is made right away
            var bookList = _db.Books.ToList();
            foreach (var book in bookList)
            {
                totalPrice += book.Price;
            }

            //here the retrieval code is NOT executed yet -> this means call to db is NOT made yet
            var bookCollection2 = _db.Books;
            //so actually here when it needs to do something with the data inside var bookCollection2
            //but it only executes the SQL query to db to Count() method (so does not retrieve all the books)
            var bookCount1 = bookCollection2.Count();


            //here the retrieval code is executed right away -> this means call to db is made right away
            //but it only executes the SQL query to db to Count() method (so does not retrieve all the books)
            var bookCount2 = _db.Books.Count();
            */


            //#####--- IEnumerable vs IQuerable Execution ---#####
            /*
            IEnumerable<Book> BookList1 = _db.Books;
            var FilteredBook1 = BookList1.Where(b => b.Price > 50).ToList();

            IQueryable<Book> BookList2 = _db.Books;
            var filteredBook2 = BookList2.Where(b => b.Price > 50).ToList();
            */


            //#####--- Views ---#####
            var viewList = _db.MainBookDetails.ToList();
            var viewList1 = _db.MainBookDetails.FirstOrDefault();
            var viewList2 = _db.MainBookDetails.Where(u=>u.Price>30);

            //#####--- Raw SQL ---#####
            var bookraw = _db.Books.FromSqlRaw("SELECT * FROM dbo.Books").ToList();

            var bookraw2 = _db.Books.FromSqlRaw("SELECT * FROM dbo.Books WHERE bookId={0}", 5).ToList();


            var id = 1;
            var bookraw1 = _db.Books.FromSqlInterpolated($"SELECT * FROM dbo.Books WHERE bookId={id}").ToList();


            //#####--- Executing Store Procedure ---#####
            var booksproc = _db.Books.FromSqlRaw("EXEC dbo.getBookDetailById @bookId={0}", 1).ToList();

            var myId = 6;
            var booksproc2 = _db.Books.FromSqlInterpolated($"EXEC dbo.getBookDetailById {myId}").ToList();

            //if we need more parameters in the store procedure:
            var myparam1 = 1;
            var myparam2 = "test";
            var myparam3 = true;
            var booksproc3 = _db.Books.FromSqlInterpolated($"EXEC dbo.getBookDetailById {myparam1}, {myparam2}, {myparam3}").ToList();


            return RedirectToAction(nameof(Index));
        }



    }
}
