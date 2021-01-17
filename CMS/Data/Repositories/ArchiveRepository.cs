using CMS.Mappers;
using CMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Data.Repositories
{
    public class ArchiveRepository
    {
        private readonly DataContext _dataContext;

        public ArchiveRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task ArchiveCourseAsync(int courseId) {
            //opvragen, include attachments => framework koppelt los
            var course = await _dataContext.Courses.Include(x => x.Subjects)
                .ThenInclude(x => x.Attachments).SingleAsync(x => x.CourseId == courseId);

            //mappen
            var courseArchive = course.ToArchive();

            //toevoegen
            await _dataContext.CourseArchive.AddAsync(courseArchive);

            //verwijderen
            _dataContext.Courses.Remove(course);
            _dataContext.Subjects.RemoveRange(course.Subjects);

            //opslaan
            await _dataContext.SaveChangesAsync();
        }

        public List<CourseModel> GetAllArchivedCourses() {
            var result = new List<CourseModel>();
            try
            {
                var list = _dataContext.CourseArchive.ToListAsync().Result;
                foreach (var course in list) {
                    result.Add(course.ArchiveToCourseModel());
                }
                return result;
            }
            catch (Exception e) {
                return result;
            }
        }

        public List<CourseModel> GetByName(string search) {
            var result = new List<CourseModel>();
            try
            {
                var list = _dataContext.CourseArchive.Where(x => x.Name.StartsWith(search) || search == null).ToListAsync().Result;
                foreach (var course in list)
                {
                    result.Add(course.ArchiveToCourseModel());
                }
                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }

        public List<CourseModel> GetByCode(string search)
        {
            var result = new List<CourseModel>();
            try
            {
                var list = _dataContext.CourseArchive.Where(x => x.Code.StartsWith(search) || search == null).ToListAsync().Result;
                foreach (var course in list)
                {
                    result.Add(course.ArchiveToCourseModel());
                }
                return result;
            }
            catch (Exception e)
            {
                return result;
            }

        }

        public List<CourseModel> GetBySemester(int search)
        {
            var result = new List<CourseModel>();
            try
            {
                var list = _dataContext.CourseArchive.Where(x => x.Semester == search || search == 0).ToListAsync().Result;
                foreach (var course in list)
                {
                    result.Add(course.ArchiveToCourseModel());
                }
                return result;
            }
            catch (Exception e)
            {
                return result;
            }

        }
    }
}