using CRUDOperationsForBook.Data;
using CRUDOperationsForBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;

namespace CRUDOperationsForBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext context;

        public BookController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var books = context.Books.ToList();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            var book = context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
                return Ok(book);
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult AddBook(Book book)
        {
            if (ModelState.IsValid)
            {
                context.Books.Add(book);
                context.SaveChanges();
                return Created();
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult Edit(int id, Book newBook)
        {
            if(ModelState.IsValid)
            {
                var book = context.Books.FirstOrDefault(b => b.Id == id);
                if (newBook == null)
                {
                    return NotFound();
                }
                book.AuthorName = newBook.AuthorName;
                book.CoverImageUrl = newBook.CoverImageUrl;
                book.Description = newBook.Description;
                book.Genre = newBook.Genre;
                book.ISBN = newBook.ISBN;
                book.Language = newBook.Language;
                book.Pages = newBook.Pages;
                book.Publisher = newBook.Publisher;
                book.Price = newBook.Price;
                book.PublishedDate = newBook.PublishedDate;
                book.Title = newBook.Title;
                context.Books.Update(book);
                context.SaveChanges();
                return Ok(book);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBook(int id)
        {
            var book = context.Books.FirstOrDefault(b => b.Id == id);
            if(book == null)
            {
                return NotFound();
            }
            context.Books.Remove(book);
            context.SaveChanges();
            return Ok();
        }
    }
}
