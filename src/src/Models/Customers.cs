using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class Customers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[RegularExpression(@"^[A-Z]+[a-zA-Z0-9\s]*$"), Required, StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        [Phone]
        public string Contact { get; set; }
        [Required, StringLength(255, MinimumLength = 3)]
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Image { get; set; }
       // [RegularExpression(@"^[A-Z]+[a-zA-Z0-9\S]*$"), Required, StringLength(255, MinimumLength = 3)]
        public string Username { get; set; }
      // [RegularExpression(@"^[a-zA-Z0-9\S]{15}*$"), Required, StringLength(255, MinimumLength = 3)]
        public string Password { get; set; }
        public bool Is_verified { get; set; }
        public bool Is_active { get; set; }
        public string Type { get; set; }

        public ICollection<Properties> Properties { get; set; }
        
    }
}
