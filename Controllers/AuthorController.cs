using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AuthorController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: All Author
        public IActionResult Index()
        {
            var authors = _dbContext.Authors.ToList();
            return View(authors);
        }

        // GET: Author/Create Form
        public IActionResult Create()
        {
            return View();
        }

        // POST: Author/Create Submit
        [HttpPost]
        public IActionResult Create(Author author)
        {
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _dbContext.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }
        // GET: Author/Edit/:id  submit
        [HttpPost]
        public IActionResult Edit(int id,[Bind("AuthorId,Name")] Author author)
        {
            if (id != author.AuthorId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(author);
                    _dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dbContext.Authors.Any(e => e.AuthorId == id))
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
            return View(author);
        }

        // GET: Author/Delete/:id
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _dbContext.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}