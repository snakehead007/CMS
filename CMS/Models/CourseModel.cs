using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class CourseModel
    {
        public int CourseId { get; set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string ImgLoc { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Semester { get; set; }
    }
}
