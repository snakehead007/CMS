using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class CourseModel
    {
        [Required]
        public string Name { get; set; }
        [Required, MaxLength(6)]
        public string Description { get; set; }
        [Required]
        public string Code { get; set; }
        [Required, Range(1,2)]
        public string ImgLoc { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public int Semester { get; set; }
    }
}
