using CRUDOperationsForBook.Data;
using CRUDOperationsForBook.DTOs;
using CRUDOperationsForBook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDOperationsForBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext context;

        public AuthorController(AppDbContext context)
        {
            this.context = context;
        }

        // Add a new author
        [HttpPost]
        public IActionResult AddAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                context.Authors.Add(author);
                context.SaveChanges();
                return Created();
            }
            return BadRequest(ModelState);
        }

        // Retrieve all authors
        [HttpGet]
        public IActionResult GetAll()
        {
            //Database Object
            List<Author> authors = context.Authors.Include(a=>a.Books).ToList();
            //DTO Object
            List<AuthorWithBooks> authorsDTO = new List<AuthorWithBooks>();
            foreach(var author in authors)
            {
                AuthorWithBooks authorWithBooks = new AuthorWithBooks();
                authorWithBooks.Id = author.Id;
                authorWithBooks.Name = author.Name;
                authorWithBooks.Birthdate = author.Birthdate;
                authorWithBooks.Books = author.Books?.Select(b => b.Title).ToList() ?? new List<string>();
                authorsDTO.Add(authorWithBooks);
            }
            return Ok(authorsDTO);
        }

        //Get Author by Id
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var author = context.Authors.Include(a=>a.Books).FirstOrDefault(a => a.Id == id);
            var authorWithBooks = new AuthorWithBooks();
            if (author != null)
            {
                authorWithBooks.Id = author.Id;
                authorWithBooks.Name = author.Name;
                authorWithBooks.Birthdate = author.Birthdate;
                authorWithBooks.Books = author.Books?.Select(b => b.Title).ToList() ?? new List<string>();
                return Ok(authorWithBooks);
            }
            return NotFound();
        }

        //Edit author
        [HttpPut("{id:int}")]
        public IActionResult EditAuthor(int id, Author newAuthor)
        {
            if (ModelState.IsValid)
            {
                var author = context.Authors.FirstOrDefault(a => a.Id == id);

                if (author == null)
                {
                    return NotFound();
                }
                author.Name = newAuthor.Name;
                author.Birthdate = newAuthor.Birthdate;
                author.Nationality = newAuthor.Nationality;
                author.Website = newAuthor.Website;
                context.Update(author);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        //Delete a specific author
        [HttpDelete("{id:int}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = context.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
                return NotFound();

            context.Remove(author);
            context.SaveChanges();
            return Ok();
        }


        //Retrieve all books for a specific author
        [HttpGet("Books")]
        public IActionResult GetBooksForAuthor(int id)
        {
            var author = context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            var authorWithBooks = new AuthorWithBooks
            {
                Id = author.Id,
                Name = author.Name,
                Birthdate = author.Birthdate,
                Books = author.Books?.Select(b => b.Title).ToList() ?? new List<string>()
            };
            return Ok(authorWithBooks);
        }

        // Add a new book for a specific author
        [HttpPost("{authorId:int}/Books")]
        public IActionResult AddNewBookToAuthor(int authorId, Book book)
        {
            var author = context.Authors.Include(a=>a.Books).FirstOrDefault(a => a.Id == authorId);
            if (author == null)
            {
                return NotFound();
            }
            var existingBook = context.Books.FirstOrDefault(b => b.Title == book.Title && b.AuthorId == authorId);
            if (existingBook != null)
            {
                return BadRequest("This book already exists for this author.");
            }
            var bookToAdd = new Book
            {
                Title = book.Title,
                AuthorName = author.Name,
                Genre = book.Genre,
                ISBN = book.ISBN,
                Description = book.Description,
                PublishedDate = book.PublishedDate,
                Price = book.Price,
                Publisher = book.Publisher,
                Pages = book.Pages,
                Language = book.Language,
                CoverImageUrl = book.CoverImageUrl,
                AuthorId = authorId
            };
            if (ModelState.IsValid)
            {
                context.Books.Add(bookToAdd);
                context.SaveChanges();
                return CreatedAtAction(nameof(GetBooksForAuthor), new { id = authorId }, book);
            }
            return BadRequest(ModelState);
        }

        //Retireve a specific book by Id for a specific author
        [HttpGet("{authorId:int}/Books/{bookId:int}")]
        public IActionResult GetSpecificBookForSpecificAuthor(int authorId, int bookId)
        {
            var existingBook = context.Books.FirstOrDefault(b => b.Id == bookId && b.AuthorId == authorId);
            if(existingBook == null)
            {
                return NotFound("There is no book with this Id");
            }
            return Ok(existingBook);
        }

        //Delete a specific book by ID for a specific author
        [HttpDelete("{authorId:int}/Books/{bookId:int}")]
        public IActionResult DeleteSpecificBookForSpecificAuthor(int authorId, int bookId)
        {
            var existingBook = context.Books.FirstOrDefault(b => b.Id == bookId && b.AuthorId == authorId);
            if (existingBook == null)
            {
                return NotFound("There is no book with this Id");
            }
            context.Books.Remove(existingBook);
            context.SaveChanges();
            return Ok();
        }

        //Edit a specific book by ID for a specific author
        [HttpPut("{authorId:int}/Books/{bookId:int}")]
        public IActionResult EditSpecificBookForSpecificAuthor(int authorId, int bookId, Book updatedBook)
        {
            var existingBook = context.Books.FirstOrDefault(b => b.Id == bookId && b.AuthorId == authorId);
            if(existingBook == null)
            {
                return NotFound("There is no book with this id");
            }

            existingBook.Title = updatedBook.Title;
            existingBook.AuthorName = updatedBook.AuthorName;
            existingBook.Genre = updatedBook.Genre;
            existingBook.ISBN = updatedBook.ISBN;
            existingBook.Description = updatedBook.Description;
            existingBook.PublishedDate = updatedBook.PublishedDate;
            existingBook.Price = updatedBook.Price;
            existingBook.Publisher = updatedBook.Publisher;
            existingBook.Pages = updatedBook.Pages;
            existingBook.Language = updatedBook.Language;
            existingBook.CoverImageUrl = updatedBook.CoverImageUrl;
            existingBook.AuthorId = authorId;
            if (ModelState.IsValid)
            {
                context.Books.Update(existingBook);
                context.SaveChanges();
                return Ok(existingBook);
            }
            return BadRequest(ModelState);
        }

    }
}
