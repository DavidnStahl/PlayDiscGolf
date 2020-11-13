using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models.DataModels
{
    public class Location
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid LocationID { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Name { get; set; }
        [Required]
        public decimal Latitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
