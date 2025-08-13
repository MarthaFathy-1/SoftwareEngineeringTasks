using CRUDOperationsForBook.Models;
using System.ComponentModel.DataAnnotations;

namespace CRUDOperationsForBook.DTOs
{
    public class AuthorWithBooks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }

        public List<string> Books { get; set; }
    }
}
