using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class SubjectOverviewViewModel
    {
        public CourseModel Course { get; set; }
        public List<SubjectViewModel> Subjects { get; set; }
    }
}
