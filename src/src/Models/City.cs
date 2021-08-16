using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace src.Models
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("City Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9\s]*$"), Required, StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [ForeignKey("Region_id")]
        public Region region { get; set; }
        public int Region_id { get; set; }
        

        public ICollection<Area> Areas { get; set; }
    }
}
