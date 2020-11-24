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

        public Task<List<Course>> GetListAsync() {    
            return db.Courses.ToListAsync();
        }

        public Task<List<Course>> SearchListAsync(string search)
        {
            search ??= "";
            return db.Courses.Where(x => x.Name.Contains(search)).ToListAsync();
        }
    }
}

//firstly used this inside function GetListAsync:

//List<Course> courses = new List<Course>();

////alle courses opvragen uit db.
//var qry = from c in db.Courses
//          orderby c.Code
//          select c;

////voor elke course ga je dan een nieuw course model aanmaken en deze in de list steken.
//foreach (var course in qry) {
//    var temp = new Course { 
//        CourseId = course.CourseId, 
//        Name = course.Name, 
//        Code = course.Code, 
//        Description = course.Description, 
//        ImgLoc = course.ImgLoc, 
//        Semester = course.Semester, 
//        StartDate = course.StartDate, 
//        EndDate = course.EndDate};

//    courses.Add(temp);
//}