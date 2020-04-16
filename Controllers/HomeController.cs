using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private DeliciousContext dbContext;

        public HomeController(DeliciousContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Dish> AllDishes = dbContext.Dishes.OrderByDescending(u => u.CreatedAt).ToList();
            return View(AllDishes);
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("New");
            }
        }

        [HttpGet("/{id}")]
        public IActionResult SingleView(int id)
        {
            Dish viewDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == id);
            return View(viewDish);
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            Dish editDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == id);
            return View(editDish);
        }

        [HttpPost("update/{id}")]
        public IActionResult Update(int id, Dish editForm)
        {
            if (ModelState.IsValid)
            {
                Dish updateDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == id);
                updateDish.Name = editForm.Name;
                updateDish.Chef = editForm.Chef;
                updateDish.Tastiness = editForm.Tastiness;
                updateDish.Calories = editForm.Calories;
                updateDish.Description = editForm.Description;
                updateDish.UpdateAt = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("SingleView", new {id = updateDish.DishId});
            }
            else
            {
                editForm.DishId = id;
                return View("Edit", editForm);
            }
        }

        [HttpGet("/delete/{id}")]
        public IActionResult Delete(int id)
        {
            Dish deadDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == id);
            dbContext.Dishes.Remove(deadDish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
