using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers{
    public class CustomerController : Controller{
        private readonly AppDbContext _dbContext;

        public CustomerController(AppDbContext dbContext){
            _dbContext = dbContext;
        }

        // GET: Customer
        public IActionResult Index(){
            var customers = _dbContext.Customers.ToList();
            return View(customers);
        }

        // GET: Customer/Create
        public IActionResult Create(){
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public IActionResult Create(Customer customer){
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id){
            if (id == null){
                return NotFound();
            }

            var customer = _dbContext.Customers.Find(id);
            if (customer == null){
                return NotFound();
            }

            return View(customer);
        }
        // GET: Customer/Edit/:id
        [HttpPost]
        public IActionResult Edit(int id,[Bind("CustomerId,Name")] Customer customer){
            if (id != customer.CustomerId){
                return NotFound();
            }

            if (ModelState.IsValid){
                try{
                    _dbContext.Update(customer);
                    _dbContext.SaveChanges();
                }catch (DbUpdateConcurrencyException){
                    if (!_dbContext.Customers.Any(e => e.CustomerId == id)){
                        return NotFound();
                    }else{
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public IActionResult Delete(int id){
            var customer = _dbContext.Customers.Find(id);
            if (customer == null){
                return NotFound();
            }
            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}