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
                throw new ResourceNotFoundException("Library Branch ID is required.");
            }

            var libraryBranch = _dbContext.LibraryBranches.Find(id);
            if (libraryBranch == null){
                throw new ResourceNotFoundException("Library Branch not found.");
            }

            return View(libraryBranch);
        }

        // POST: LibraryBranch/Edit/:id
        [HttpPost]
        public IActionResult Edit(int id, [Bind("LibraryBranchId,BranchName")] LibraryBranch libraryBranch){
            if (id != libraryBranch.LibraryBranchId){
                throw new ResourceNotFoundException("Library Branch not found.");
            }

            if (ModelState.IsValid){
                try{
                    _dbContext.Update(libraryBranch);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));

                }catch (DbUpdateConcurrencyException){
                    if (!_dbContext.LibraryBranches.Any(e => e.LibraryBranchId == id)){
                        throw new ResourceNotFoundException("Library Branch not found.");
                    }else{
                        throw;
                    }
                }
            }
            return View(libraryBranch);
        }

        // GET: LibraryBranch/Delete/:id
        public IActionResult Delete(int? id){
            if (id == null){
                throw new CustomValidationException("Library Branch ID is required.");
            }
            var branch = _dbContext.LibraryBranches.Find(id);
            if (branch == null){
                throw new ResourceNotFoundException("Library Branch not found.");
            }
            _dbContext.LibraryBranches.Remove(branch);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}