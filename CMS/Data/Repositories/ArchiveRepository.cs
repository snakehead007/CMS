using CMS.Mappers;
using Microsoft.EntityFrameworkCore;
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
    }
}