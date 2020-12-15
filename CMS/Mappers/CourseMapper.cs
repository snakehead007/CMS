using CMS.Data.Entities;
using CMS.Models;

namespace CMS.Mappers
{
    public static class CourseMapper
    {
        public static CourseModel ToModel(this Course course)
        {
            return new CourseModel
            {
                CourseId = course.CourseId,
                Name = course.Name,
                Code = course.Code,
                Description = course.Description,
                Semester = course.Semester != null ? (int)course.Semester : -1,
                ImgLoc = course.ImgLoc,
                StartDate = course.StartDate,
                EndDate = course.EndDate
            };
        }

        public static Course ToEntity(this CourseModel course)
        {
            return new Course
            {
                CourseId = course.CourseId,
                Name = course.Name,
                Code = course.Code,
                Description = course.Description,
                Semester = course.Semester != null ? (int)course.Semester : -1,
                ImgLoc = course.ImgLoc,
                StartDate = course.StartDate,
                EndDate = course.EndDate
            };
        }

    }
}
