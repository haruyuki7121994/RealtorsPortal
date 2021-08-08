using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class Admins
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Is_verified { get; set; }
        public bool Is_active { get; set; }
        public string Role { get; set; }

    }
}
