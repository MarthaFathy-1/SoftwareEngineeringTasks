using System.ComponentModel.DataAnnotations;

namespace CRUDOperationsForBook.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }

        [StringLength(100)]
        public string Nationality { get; set; }

        [Url]
        public string Website { get; set; }

        public ICollection<Book>? Books { get; set; }

    }
}
