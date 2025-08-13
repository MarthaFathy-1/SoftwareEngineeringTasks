using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDOperationsForBook.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        [Display(Name = "Book Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Author name cannot be longer than 50 characters.")]
        public string AuthorName { get; set; }

        [StringLength(30, ErrorMessage = "Genre cannot be longer than 30 characters.")]
        public string Genre { get; set; }

        [Isbn(ErrorMessage = "Please enter a valid ISBN number.")]
        public string ISBN { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [ValidatePublishedDate(ErrorMessage = "Published date cannot be in the future.")]
        public DateTime PublishedDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        [StringLength(50, ErrorMessage = "Publisher name cannot be longer than 50 characters.")]
        public string Publisher { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Pages must be at least 1.")]
        public int Pages { get; set; }

        [StringLength(20, ErrorMessage = "Language cannot be longer than 20 characters.")]
        public string Language { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string CoverImageUrl { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
