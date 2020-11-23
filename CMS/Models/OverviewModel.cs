using CMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class OverviewModel
    {
        private List<Course> courses;

        public List<Course> Courses { get { return this.courses;  } set { this.courses = value;  } }

        public OverviewModel()
        {
            courses = new List<Course>();
        }

        public OverviewModel(List<Course> courses)
        {
            this.courses = courses;
        }

        public void addCourse(Course course) {
            courses.Add(course);
        }

        public List<Course> GetCourses() {
            return courses;
        }
    }
}
