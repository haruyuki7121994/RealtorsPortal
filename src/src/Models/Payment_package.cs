using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class Payment_package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Status { get; set; }
        public string Transaction_id { get; set; }
        public float Payment_price { get; set; }
        public int Limit_ads { get; set; }
        public int Limit_featured_ads { get; set; }
        public int Used_ads { get; set; }
        public int Used_featured_ads { get; set; }
        public DateTime Create_at {
            get
            {
                return DateTime.Now;
            }
        }
        public DateTime Update_at {
            get
            {
                return DateTime.Now;
            }
        }
        public int Package_id { get; set; }
        public int Customer_id { get; set; }

    }
}
