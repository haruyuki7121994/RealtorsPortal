using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class Properties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\s]*$"), Required, StringLength(30, MinimumLength = 3)]
        public string Title { get; set; }
        [RegularExpression(@"[a-zA-Z0-9\s]*$"), Required, StringLength(30, MinimumLength = 3)]
        public string Introduction { get; set; }
        public string Description { get; set; }
        public string Features { get; set; }
        public string Method { get; set; }
        [Range(0, 100000000000000)]
        public double Price { get; set; }
        [Range(0, 100000000000000)]
        public double Deposit { get; set; }
        public bool Is_featured { get; set; }
        public DateTime Create_at
        {
            get
            {
                return DateTime.Now;
            }
        }
        public DateTime Ended_at { get; set; }
        public int Area_id { get; set; }
        public int Category_id { get; set; }
        public int Customer_id { get; set; }
    }
}
