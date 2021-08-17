using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    [Table("Payment_package")]
    public class PaymentPackage
    {
        [NotMapped]
        public static int PENDING_STATUS = 0;
        [NotMapped]
        public static int APPROVED_STATUS = 1;
        [NotMapped]
        public static int EXPIRED_STATUS = 2;

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
        public DateTime Created_at {get; set;}
        public DateTime Updated_at {get; set;}
        public int Package_id { get; set; }
        public int Customer_id { get; set; }
        [ForeignKey("Package_id")]
        public Package Package { get; set; }

        [ForeignKey("Customer_id")]
        public Customer Customer { get; set; }

        [NotMapped]
        public string PackageName { get; set; }

    }
}
