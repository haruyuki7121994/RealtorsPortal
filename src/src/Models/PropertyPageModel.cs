using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class PropertyPageModel
    {
        public Property Property { get; set; }
        public List<Property> FeaturedProps { get; set; }
        public List<Image> Gallary { get; set; }
        public Comment Comment { get; set; }
    }
}
