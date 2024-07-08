using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BookController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Book
        public IActionResult Index()
        {
            var books = _dbContext.Books.ToList(); 
            return View(books);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Book/Create
        [HttpPost]
        public IActionResult Create(Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _dbContext.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        // GET: Book/Edit/:id
        [HttpPost]
        public IActionResult Edit(int id,[Bind("BookId,AuthorId,Title,LibraryBranchId")] Book Book)
        {
            if (id != Book.BookId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(Book);
                    _dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dbContext.Books.Any(e => e.BookId == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Book);
        }

        // GET: Book/Delete/:id
        public IActionResult Delete(int id)
        {
            var book = _dbContext.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}