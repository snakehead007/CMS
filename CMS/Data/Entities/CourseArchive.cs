using System;
using System.Collections.Generic;

namespace CMS.Data.Entities
{
    public class CourseArchive
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string Code { get; set; }

        public string? ImgLoc { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? Semester { get; set; }

        public List<SubjectArchive> Subjects { get; set; }
    }
}
