using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CRUDOperationsForBook.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(8)]
        public string password { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
    }
}
