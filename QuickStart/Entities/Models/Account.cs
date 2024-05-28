using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Account
    {
        [Column("AccountId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Date created is required")]
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage = "Account type is required")]
        public string? AccountType { get; set; }
        [Required(ErrorMessage = "Customer Id is required")]

        [ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
