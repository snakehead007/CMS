using CMS.Mappers;
using CMS.Data.Entities;
using System.Linq;
using System.Threading.Tasks;
using CMS.Interfaces.Repositories;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Controllers
{
    [Authorize]
    public class SubjectController : Controller
    {
        private readonly ILogger<SubjectController> _logger;
        private readonly ICourseRepository _courseRepository;

        public SubjectController(ILogger<SubjectController> logger, ICourseRepository courseRepository)
        {
            _logger = logger;
            _courseRepository = courseRepository;
        }

        // Retrieve overview of Subjects of a course by courseId
        public async Task<IActionResult> SubjectOverview(int courseId)
        {
            var course = await _courseRepository.GetSubjectsOfCourseAsync(courseId);
            //Transform to overview Viewmodel
            var viewModel = new SubjectOverviewViewModel
            {
                Course = course.ToModel(),
                Subjects = course.Subjects.Select(x => x.ToModel()).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Lector")]
        public async Task<IActionResult> AddSubject(int courseId, string name, string description)
        {
            var subject = new Subject { Name = name, Description = description };

            // Subject presisteren
            var course = await _courseRepository.AddSubjectToCourseAsync(courseId, subject);
            // Viewmodel
            var viewModel = new SubjectOverviewViewModel
            {
                Course = course.ToModel(),
                Subjects = course.Subjects.Select(x => x.ToModel()).ToList()
            };

            return PartialView("PartialSubjectOverview",viewModel);
        }

        // Put editSubject
        [HttpPut]
        [Authorize(Roles = "Admin, Lector")]
        public async Task<IActionResult> EditSubject(int courseId, int subjectId, string description, string name)
        {
            var subject = new Subject { SubjectId = subjectId, Name = name, Description = description };

            // Aanpassingen presisteren in de db
            var course = await _courseRepository.EditSubjectToCourseAsync(courseId, subject);
            //Viewmodel
            var viewModel = new SubjectOverviewViewModel
            {
                Course = course.ToModel(),
                Subjects = course.Subjects.Select(x => x.ToModel()).ToList()
            };

            return PartialView("PartialSubjectOverview", viewModel);

        }

        // GET Subjects
        [HttpGet]
        public async Task<IActionResult> GetSubjects(int courseId)
        {

            var course = await _courseRepository.GetSubjectsOfCourseAsync(courseId);
            //Transform to overview Viewmodel
            var viewModel = new SubjectOverviewViewModel
            {
                Course = course.ToModel(),
                Subjects = course.Subjects.Select(x => x.ToModel()).ToList()
            };
            return PartialView("PartialSubjectOverview", viewModel);
        }
    }
}
