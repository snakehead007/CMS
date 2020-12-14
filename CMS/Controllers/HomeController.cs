using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CMS.Models;
using CMS.Interfaces.Repositories;
using CMS.Data.Entities;
using System;
using Microsoft.AspNetCore.SignalR;
using CMS.Hubs;
using CMS.Mappers;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseRepository _courseRepository;
        private readonly IHubContext<CourseHub> _hubContext;

        public HomeController(ILogger<HomeController> logger, ICourseRepository courseRepository, IHubContext<CourseHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
            this._courseRepository = courseRepository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                List<Course> courses = await _courseRepository.GetListAsync();

                OverviewModel overviewModel = new OverviewModel
                {
                    Courses = courses.Select(course => course.ToModel()).ToList()
                };
                return View(overviewModel);
            }
            catch (Exception e) {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> AddCourse(CourseModel model)
        {
            try { 
                if(model.StartDate >= model.EndDate)
                {
                    ModelState.AddModelError("Start", "Start date must be before End date.");
                    ModelState.AddModelError("End", "End date must be before Start date.");
                }
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var course = model.ToEntity();
                var result = await _courseRepository.AddCourseAsync(course);
                await _hubContext.NotifyCourseChanged(result);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e) {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
        
        }

        [HttpGet]
        public async Task<IActionResult> SearchCourses(string search)
        {
            try { 
                var courses = await _courseRepository.SearchListAsync(search);

                OverviewModel overviewModel = new OverviewModel
                {
                    Courses = courses.Select(course => course.ToModel()).ToList()
                };
                return PartialView("PartialCourseOverview", overviewModel);
            }
            catch (Exception e) {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            Course course = _courseRepository.GetCourseById(id).Result;

            CourseModel courseModel = new CourseModel
            {
                Id = course.CourseId,
                Name = course.Name,
                Code = course.Code,
                Description = course.Description,
                ImgLoc = course.ImgLoc,
                Semester = (int)course.Semester,
                StartDate = course.StartDate,
                EndDate = course.EndDate
            };

            return View(courseModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(CourseModel model)
        {

            _courseRepository.UpdateCourseById(model.Id, new Course {
                Name = model.Name,
                Code = model.Code,
                Description = model.Description, 
                Semester = model.Semester,
                ImgLoc = model.ImgLoc,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            });

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteCourse(int id)
        {
            Console.WriteLine(id.ToString());

            Course course = _courseRepository.GetCourseById(id).Result;

            CourseModel courseModel = new CourseModel { 
                Id = course.CourseId,
                Name = course.Name,
                Code = course.Code,
                Description = course.Description,
                ImgLoc = course.ImgLoc,
                Semester = (int)course.Semester,
                StartDate = course.StartDate,
                EndDate = course.EndDate
            };

            return View(courseModel);
            //Return view confirmation page => are you sure you want to delete course...
        }

        [HttpPost]
        public IActionResult DeleteCourse(CourseModel course)
        {
            //ToDo : catch exception
            _courseRepository.DeleteCourseById(course.Id);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
