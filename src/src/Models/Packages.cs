using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class Packages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Limit_ads { get; set; }
        public int Limit_featured_ads { get; set; }
        public bool Is_active { get; set; }
        public ICollection<Payment_package> Payment_Package { get; set; }
    }
}
