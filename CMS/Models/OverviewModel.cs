using CMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class OverviewModel
    {
        private List<CourseModel> courses;

        public List<CourseModel> Courses { get { return this.courses;  } set { this.courses = value;  } }

        public OverviewModel()
        {
            courses = new List<CourseModel>();
        }

        public OverviewModel(List<CourseModel> courses)
        {
            this.courses = courses;
        }

        public void addCourse(CourseModel course) {
            courses.Add(course);
        }

        public List<CourseModel> GetCourses() {
            return courses;
        }
    }
}
