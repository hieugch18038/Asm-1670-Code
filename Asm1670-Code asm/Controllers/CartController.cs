using Asm1670.Data;
using Asm1670.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Asm1670.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext context;
        public CartController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        public IActionResult ViewCart()
        {
            return View(context.Cart.ToList());
        }
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public IActionResult Make(int id, int quantity)
        {
            var cart = new Cart();
            var book = context.Book.Find(id);
            cart.Book = book;
            cart.BookId = id;
            cart.OrderQuantity = quantity;
            cart.OrderPrice = (int)book.Price * quantity;
            cart.OrderDate = DateTime.Now;
            cart.Email = User.Identity.Name;
            context.Cart.Add(cart);
            book.Quantity -= quantity;
            context.Book.Update(book);
            context.SaveChanges();
            TempData["Success"] = "Order Book Successfully";
            return RedirectToAction("Store","Book");

        }
        [Authorize(Roles = "Store Owner")]
        public IActionResult Delete(int Id)
        {
            var cart = context.Cart.Find(Id);
            context.Cart.Remove(cart);
            context.SaveChanges();
            TempData["Message"] = "Delete a book from the store successfully !";
            return RedirectToAction("ViewCart");
        }
    }
}
