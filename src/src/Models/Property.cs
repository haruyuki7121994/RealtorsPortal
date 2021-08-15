using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace src.Models
{
    public class Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [
            Required(ErrorMessage = "Title cannot empty"),
            StringLength(30, MinimumLength = 3, ErrorMessage = "MinimumLength = 3; MaximumLength = 20")
        ]

        public string Title { get; set; }
        [
            Required(ErrorMessage = "Introduction cannot empty"),
            StringLength(255, MinimumLength = 10, ErrorMessage = "MinimumLength = 10; MaximumLength = 255")
        ]
        public string Introduction { get; set; }

        [
            Required(ErrorMessage = "Description cannot empty"),
            StringLength(255, MinimumLength = 10, ErrorMessage = "MinimumLength = 10; MaximumLength = 255")
        ]
        public string Description { get; set; }
        public string Features { get; set; }
        public string Thumbnail_url { get; set; }

        [
            Required(ErrorMessage = "Method cannot empty")
        ]
        public string Method { get; set; }


        [
            Required(ErrorMessage = "Price cannot empty"),
            Range(1, 100000000000000)
        ]
        public float Price { get; set; }

        [
            Range(0, 100000000000000)
        ]
        public float Deposit { get; set; }
        [DisplayName("Featured")]
        public bool Is_featured { get; set; }
        public bool Is_active { get; set; }
        public DateTime Created_at { get; set; }
 [DisplayName("End At")]
        public DateTime Ended_at { get; set; }

        [Required]
        public int Area_id { get; set; }
        public int Category_id { get; set; }
       

        [ForeignKey("Category_id")]
        public Category Category { get; set; }

        [ForeignKey("Area_id")]
        public Area Area { get; set; }

        [ForeignKey("Customer_id")]
        public Customer Customer { get; set; }
        public int Customer_id { get; set; }
        [NotMapped]
        public City City { get; set; }
        [NotMapped]
        public Country Country { get; set; }
        [NotMapped]
        public Region Region { get; set; }
    }
}
