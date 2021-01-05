using CMS.Data.Entities;
using CMS.Models;
using System.Collections.Generic;
using System.Linq;

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

        public static CourseArchive ToArchive(this Course course)
        {
            return new CourseArchive
            {
                CourseId = course.CourseId,
                Name = course.Name,
                Code = course.Code,
                Description = course.Description,
                Semester = course.Semester != null ? (int)course.Semester : -1,
                ImgLoc = course.ImgLoc,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                Subjects = course.Subjects?.Select(x => x.ToArchive()).ToList() ?? new List<SubjectArchive>()
            };
        }
    }
}
