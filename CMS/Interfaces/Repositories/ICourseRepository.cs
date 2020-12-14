using CMS.Data.Entities;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Interfaces.Repositories
{
    public interface ICourseRepository
    {
        public Task<List<Course>> GetListAsync();
        public Task<Course> GetCourseAsync(int courseId);
        public Task<List<Course>> SearchListAsync(string search);
        public Task<Course> AddCourseAsync(Course course);
        public Task<Course> GetSubjectsOfCourseAsync(int courseId);
        public Task<Course> AddSubjectToCourseAsync(int courseId, Subject subject);
        public Task<Course> EditSubjectToCourseAsync(int courseId, Subject subject);
        public Task<Course> GetCourseById(int id);
        public Task DeleteCourseById(int id);
        public bool UpdateCourseById(int id, Course course);
    }
}
