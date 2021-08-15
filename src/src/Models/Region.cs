﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace src.Models
{
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9\s]*$"), Required, StringLength(30, MinimumLength = 3)]

        public string Name { get; set; }
        [DisplayName("Active")]
        public bool Is_active { get; set; }
        [ForeignKey("Country_id")]
        public Country Country { get; set; }

        public int Country_id { get; set; }
       
        public ICollection<City> Cities { get; set; }
    }
}
