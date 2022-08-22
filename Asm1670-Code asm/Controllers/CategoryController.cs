using Asm1670.Data;
using Asm1670.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Asm1670.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext context;
        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        public IActionResult Index()
        {
            return View(context.Category.ToList());
        }
        [Authorize(Roles = "Admin, Store Owner")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin, Store Owner")]
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                context.Add(category);
                context.SaveChanges();
                TempData["Message"] = "Add Category successfully !";
                return RedirectToAction(nameof(Index));
            }
            return View(context.Category.ToList());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var category = context.Category.Find(id);
            context.Category.Remove(category);
            context.SaveChanges();
            TempData["Message"] = "Delete Category successfully !";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(context.Category.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                context.Update(category);
                context.SaveChanges();
                TempData["Message"] = "Edit Category successfully !";
                return RedirectToAction(nameof(Index));
            }
            return View(category);

        }
        public IActionResult Detail(int id)
        {
            var category = context.Category.Include(m => m.Book)
                                     .FirstOrDefault(b => b.Id == id);
            return View(category);
        }
    }
}
