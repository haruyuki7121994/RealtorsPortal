﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class Categories
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9\s]*$"), Required, StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }
        public bool Is_active { get; set; }
        public ICollection<Properties> Properties { get; set; }
    }
}
