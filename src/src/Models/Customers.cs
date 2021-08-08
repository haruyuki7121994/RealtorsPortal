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
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Is_verified { get; set; }
        public bool Is_active { get; set; }
        public string Type { get; set; }

        public ICollection<Properties> Properties { get; set; }
        public ICollection<Payment_package> Payment_Package { get; set; }
        public ICollection<Payment_subscription> Payment_Subscription{ get; set; }
        
    }
}
