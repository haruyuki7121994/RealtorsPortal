using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9\S]*$"), Required, StringLength(255, MinimumLength =3)]
        public string Username { get; set; }
        [RegularExpression(@"[a-zA-Z0-9\S]{15}*$"), Required, StringLength(255, MinimumLength = 3)]
        public string Password { get; set; }
        public bool Is_verified { get; set; }
        public bool Is_active { get; set; }
        public string Role { get; set; }

    }
}
