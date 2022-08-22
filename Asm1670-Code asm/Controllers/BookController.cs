using Asm1670.Data;
using Asm1670.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AsmDotNet.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext context;
        public BookController(ApplicationDbContext Dbcontext)
        {
            this.context = Dbcontext;
        }

        [Authorize(Roles = "Store Owner, Admin")]
        public IActionResult ViewBook()
        {

            var books = context.Book.OrderByDescending(m => m.Id).ToList();
            return View(books);
        }

        [Authorize(Roles = "Customer, Storeowner, Admin")]
        //hiển thị giao diện dạng card cho khách hàng order sản phẩm
        public IActionResult Store()
        {
            return View(context.Book.ToList());
        }


        [Authorize(Roles = "Admin, Store Owner")]
        public IActionResult Delete(int id)
        {
            var book = context.Book.Find(id);
            context.Book.Remove(book);
            context.SaveChanges();
            TempData["Message"] = "Delete a book from the store successfully !";
            return RedirectToAction("ViewBook");
        }


        [Authorize(Roles = "Admin, Customer, Store Owner")]
        public IActionResult Detail(int id)
        {
            var book = context.Book.Include(m => m.Category).FirstOrDefault(b => b.Id == id);
            return View(book);
        }

        [Authorize(Roles = "Admin, Store Owner")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categorys = context.Category.ToList();
            return View();
        }
        [Authorize(Roles = "Admin, Store Owner")]
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                context.Add(book);
                context.SaveChanges();
                TempData["Message"] = "Add a new book successfully !";
                return RedirectToAction(nameof(ViewBook));
            }
            return View(book);
        }

        [Authorize(Roles = "Admin, Store Owner")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categorys = context.Category.ToList();
            return View(context.Book.Find(id));
        }

        [Authorize(Roles = "Admin, Store Owner")]
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                context.Update(book);
                context.SaveChanges();
                TempData["Message"] = "Edit the chosen book successfully !";
                return RedirectToAction(nameof(ViewBook));
            }
            return View(book);
        }

    }
}
