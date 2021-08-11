using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [EmailAddress]
        public string FromEmail { get; set; }
        [EmailAddress]
        public string ToEmail { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
