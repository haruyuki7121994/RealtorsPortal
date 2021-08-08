﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class Payment_subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
        public string Transaction_id { get; set; }
        public float Payment_price { get; set; }
        public DateTime Create_at
        {
            get
            {
                return DateTime.Now;
            }
        }
        public DateTime Update_at
        {
            get
            {
                return DateTime.Now;
            }
        }
        
        public int Customer_id { get; set; }

    }
}
