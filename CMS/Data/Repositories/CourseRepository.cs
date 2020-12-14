using CMS.Data.Entities;
using CMS.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public DataContext db;

        public CourseRepository(DataContext context)
        {
            this.db = context;
        }

        public async  Task<Course> AddCourseAsync(Course course)
        {
            var entityEntry = await db.Courses.AddAsync(course);
            await db.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<Course> AddSubjectToCourseAsync(int courseId, Subject subject)
        {
            var course = await GetSubjectsOfCourseAsync(courseId);
            course.Subjects ??= new List<Subject>();
            course.Subjects.Add(subject);

            await db.SaveChangesAsync();
            return course;
        }

        public async Task<Course> EditSubjectToCourseAsync(int courseId, Subject subject)
        {
            var course = await GetSubjectsOfCourseAsync(courseId);

            foreach(var subjectOfCourse in course.Subjects)
            {
                if(subjectOfCourse.SubjectId == subject.SubjectId)
                {
                    subjectOfCourse.Name = subject.Name;
                    subjectOfCourse.Description = subject.Description;
                }
            }
            await db.SaveChangesAsync();
            return course;
        }

        public Task<Course> GetCourseAsync(int courseId)
        {
            return db.Courses.FindAsync(courseId).AsTask();
        }
        public async Task<Course> GetCourseById(int id)
        {
            return await db.FindAsync<Course>(id);
        }

        public Task<List<Course>> GetListAsync() {    
            return db.Courses.ToListAsync();
        }

        public Task<Course> GetSubjectsOfCourseAsync(int courseId)
        {
            return db.Courses.Include(x => x.Subjects).SingleAsync(x => x.CourseId == courseId);
        }

        public Task<List<Course>> SearchListAsync(string search)
        {
            search ??= "";
            return db.Courses.Where(x => x.Name.Contains(search)).ToListAsync();
        }

        public async Task DeleteCourseById(int id)
        {
            try {
                Course course = db.FindAsync<Course>(id).Result;
                db.Remove(course);

                db.SaveChanges();
            }
            catch (Exception e) 
            { throw new Exception("Something went wrong: " + e.Message); }
        }

        public bool UpdateCourseById(int id, Course course)
        {
            var courseResult = (from c in db.Courses where c.CourseId == id select c).First();

            courseResult.Name = course.Name;
            courseResult.Code = course.Code;
            courseResult.Description = course.Description;
            courseResult.ImgLoc = course.ImgLoc;
            courseResult.Semester = course.Semester;
            courseResult.StartDate = course.StartDate;
            courseResult.EndDate = course.EndDate;

            try { 
                db.SaveChanges();
                return true; 
            }
            catch (Exception e) { return false; }
        }
    }
}