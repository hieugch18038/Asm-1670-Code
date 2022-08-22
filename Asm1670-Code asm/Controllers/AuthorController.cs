using Asm1670.Data;
using Asm1670.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Asm1670.Controllers
{
    public class AuthorController : Controller
    {
        private ApplicationDbContext context;
        public AuthorController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        public IActionResult Index()
        {
            return View(context.Author.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin, Author")]
        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                context.Add(author);
                context.SaveChanges();
                TempData["Message"] = "Add Author successfully !";
                return RedirectToAction(nameof(Index));
            }
            return View(context.Author.ToList());
        }
        [Authorize(Roles = "Admin, Author")]
        public IActionResult Delete(int id)
        {
            var author = context.Author.Find(id);
            context.Author.Remove(author);
            context.SaveChanges();
            TempData["Message"] = "Delete Author successfully !";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, Author")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(context.Author.Find(id));
        }

        [Authorize(Roles = "Admin, Author")]
        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                context.Update(author);
                context.SaveChanges();
                TempData["Message"] = "Edit author successfully !";
                return RedirectToAction(nameof(Index));
            }
            return View(author);

        }
        [Authorize(Roles = "Admin, Author")]
        public IActionResult Detail(int id)
        {
            var author = context.Author.Include(m => m.Book)
                                     .FirstOrDefault(b => b.Id == id);
            return View(author);
        }
    }
}
