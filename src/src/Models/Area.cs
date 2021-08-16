using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace src.Models
{
    public class Area
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Area Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9\s]*$"), Required, StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }
        [DisplayName("Active")]
        public bool Is_active { get; set; }
        [ForeignKey("City_id")]
        public City city { get; set; }
        public int City_id { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
