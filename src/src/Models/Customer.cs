using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [
            Required(ErrorMessage = "Name cannot empty"), 
            StringLength(30, MinimumLength = 3, ErrorMessage = "MinimumLength = 3; MaximumLength = 20")
        ]
        public string Name { get; set; }

        [
            Required(ErrorMessage = "Contact cannot empty"),
            StringLength(20, MinimumLength = 3, ErrorMessage = "MinimumLength = 3; MaximumLength = 20")
        ]
        public string Contact { get; set; }

        [
            Required(ErrorMessage = "Address cannot empty"),
            StringLength(255, MinimumLength = 3, ErrorMessage = "MinimumLength = 3; MaximumLength = 255")
        ]
        public string Address { get; set; }
        [
            Required(ErrorMessage = "Email cannot empty"),
            EmailAddress(ErrorMessage = "Invalid Email")
        ]
        public string Email { get; set; }

        public string Image { get; set; }

        [
             Required(ErrorMessage = "Username cannot empty"),
             StringLength(30, MinimumLength = 3, ErrorMessage = "MinimumLength = 3; MaximumLength = 20")
        ]
        public string Username { get; set; }

        [
             Required(ErrorMessage = "Password cannot empty"),
             StringLength(30, MinimumLength = 3, ErrorMessage = "MinimumLength = 3; MaximumLength = 20")
        ]
        public string Password { get; set; }
        public Boolean Is_verified { get; set; }
        public Boolean Is_active { get; set; }
        public Boolean Has_contact_form { get; set; }

        [Required(ErrorMessage = "Type cannot empty")]
        public string Type { get; set; }

        public ICollection<Property> Properties { get; set; }
        
    }
}
