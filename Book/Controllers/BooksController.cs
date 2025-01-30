using Microsoft.AspNetCore.Mvc;
using BookstoreApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookstoreApp.Controllers
{
    public class BooksController : Controller
    {
        // In-memory list to store books
        private static List<Bookmodel> books = new List<Bookmodel>();

        // Display all books
        public IActionResult Index()
        {
            return View(books);
        }

        // Show form to add a new book
        public IActionResult Create()
        {
            return View();
        }

        // Handle adding a new book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Bookmodel book)
        {
            if (ModelState.IsValid)
            {
                book.Id = books.Count > 0 ? books.Max(b => b.Id) + 1 : 1;
                books.Add(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // Show form to edit an existing book
        public IActionResult Edit(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }

        // Handle editing an existing book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Bookmodel book)
        {
            if (ModelState.IsValid)
            {
                var existingBook = books.FirstOrDefault(b => b.Id == book.Id);
                if (existingBook != null)
                {
                    existingBook.Title = book.Title;
                    existingBook.Author = book.Author;
                    existingBook.Price = book.Price;
                    existingBook.PublicationYear = book.PublicationYear;
                }
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // Show confirmation page for deleting a book
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }

        // Handle deleting a book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                books.Remove(book);
            }
            return RedirectToAction("Index");
        }
    }
}
