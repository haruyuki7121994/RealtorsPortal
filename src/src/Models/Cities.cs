using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class Cities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9\s]*$"), Required, StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        public bool Is_active { get; set; }
        public int Region_id { get; set; }
        public ICollection<Areas> Areas { get; set; }
    }
}
