using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace src.Models
{
    public class PropertiesPagingModel
    {
        public SelectList Categories;
        public SelectList Countries;
        public IEnumerable<Property> FeaturedProperties;
        public IEnumerable<Property> PagingProperies;
    }
}
