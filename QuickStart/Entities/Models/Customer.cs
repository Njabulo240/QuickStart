using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Customer
    {
        [Column("CustomerId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address can not be loner then 100 characters")]
        public string? Address { get; set; }

        public string? Country { get; set; }

        public Boolean status { get; set; } = true;
        public ICollection<Account>? Accounts { get; set; }
    }
}
