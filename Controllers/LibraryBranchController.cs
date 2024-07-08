using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers{
    public class LibraryBranchController : Controller{
        private readonly AppDbContext _dbContext;

        public LibraryBranchController(AppDbContext dbContext){
            _dbContext = dbContext;
        }

        // GET: LibraryBranch
        public IActionResult Index(){
            var libraryBranches = _dbContext.LibraryBranches.ToList();
            return View(libraryBranches);
        }

        // GET: LibraryBranch/Create
        public IActionResult Create(){
            return View();
        }

        // POST: LibraryBranch/Create
        [HttpPost]
        public IActionResult Create(LibraryBranch libraryBranch){
            if (ModelState.IsValid){
                _dbContext.LibraryBranches.Add(libraryBranch);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(libraryBranch);
        }

        // GET: LibraryBranch/Edit/:id
        public IActionResult Edit(int? id){
            if (id == null){
                return NotFound();
            }

            var libraryBranch = _dbContext.LibraryBranches.Find(id);
            if (libraryBranch == null){
                return NotFound();
            }

            return View(libraryBranch);
        }

        // POST: LibraryBranch/Edit/:id
        [HttpPost]
        public IActionResult Edit(int id, [Bind("LibraryBranchId,BranchName")] LibraryBranch libraryBranch){
            if (id != libraryBranch.LibraryBranchId){
                return NotFound();
            }

            if (ModelState.IsValid){
                try{
                    _dbContext.Update(libraryBranch);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));

                }catch (DbUpdateConcurrencyException){
                    if (!_dbContext.LibraryBranches.Any(e => e.LibraryBranchId == id)){
                        return NotFound();
                    }else{
                        throw;
                    }
                }
            }
            return View(libraryBranch);
        }

        // GET: LibraryBranch/Delete/:id
        public IActionResult Delete(int? id){
            var branch = _dbContext.LibraryBranches.Find(id);
            if (branch == null){
                return NotFound();
            }
            _dbContext.LibraryBranches.Remove(branch);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}