using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9\s]*$"), Required, StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        [Range(0, 100000000000000)]
        public double Price { get; set; }
        public int Limit_ads { get; set; }
        public int Limit_featured_ads { get; set; }
        public bool Is_active { get; set; }
    }
}
