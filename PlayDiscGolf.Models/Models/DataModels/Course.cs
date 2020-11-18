
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models.Models.DataModels
{
    public class Course
    {
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid CourseID { get; set; }

        [Required]
        public string ApiID { get; set; }

        public string ApiParentID { get; set; }
        [Required]
        public string CountryCode{ get; set; }

        [Required]
        public string Country { get; set; }
        [Required]
        public bool Main { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Area { get; set; }


        public int HolesTotal { get; set; }


        public int TotalParValue { get; set; }

        public int TotalDistance { get; set; }
        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Longitude { get; set; }
        public virtual ICollection<ScoreCard> ScoreCards { get; set; }

        public virtual ICollection<Hole> Holes { get; set; }
    }
}
