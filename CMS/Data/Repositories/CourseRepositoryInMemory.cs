using CMS.Data.Entities;
using CMS.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Data.Repositories
{
    public class CourseRepositoryInMemory : ICourseRepository
    {
        private Dictionary<int, Course> _db;

        public CourseRepositoryInMemory()
        {
            this._db = new Dictionary<int, Course>();

            //default test values
            _db.Add(0, new Course 
            { 
                CourseId = 0, 
                Name = "Wiskunde", 
                Code = "CBD-1", 
                Description = "Statistieken", 
                Semester = 1, 
                ImgLoc = "/images/placeholder.jpg", 
                StartDate = null, 
                EndDate = null 
            });

            _db.Add(1, new Course 
            { 
                CourseId = 1, 
                Name = "CMS", 
                Code = "CMS-1", 
                Description = "CMS", 
                Semester = 2, 
                ImgLoc = "/images/placeholder.jpg",
                StartDate = null, 
                EndDate = null 
            });

            _db.Add(2, new Course
            {
                CourseId = 2,
                Name = "Gip",
                Code = "GIP-4",
                Description = "gip 4",
                Semester = 1,
                ImgLoc = "/images/placeholder.jpg",
                StartDate = null,
                EndDate = null
            });

        }

        public Task<Course> AddCourseAsync(Course course)
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetCourseById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Course>> GetListAsync()
        {
            return Task.Factory.StartNew(() => { return _db.Values.ToList(); }); //ContinueWith(x => _db.Values.ToList());
        }

        public Task<List<Course>> SearchListAsync(string search)
        {
            throw new NotImplementedException();
        }
        public Task<Course> GetCourseAsync(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetSubjectsOfCourseAsync(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<Course> AddSubjectToCourseAsync(int courseId, Subject subject)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCourseById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Course> EditSubjectToCourseAsync(int courseId, Subject subject)
        {
            throw new NotImplementedException();
        }
        public bool UpdateCourseById(int id, Course course)
        {
            throw new NotImplementedException();
        }
    }
}
