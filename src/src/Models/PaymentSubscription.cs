using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    [Table("Payment_subscription")]
    public class PaymentSubscription
    {
        [NotMapped]
        public static int APPROVED_STATUS = 1;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
        public string Transaction_id { get; set; }
        public float Payment_price { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public int Customer_id { get; set; }

        [NotMapped]
        public Customer Customer { get; set; }

    }
}
