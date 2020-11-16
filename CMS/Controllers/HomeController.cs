using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CMS.Models;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogDebug("Retrieving list of courses.");
            OverviewModel overviewModel = new OverviewModel
            {
                Courses = new List<CourseModel>
                {
                    new CourseModel
                    {
                        Name = "Course A",
                        Code = "CAD-1",
                        Description = "Course A semester 1",
                        ImgLoc = "/images/placeholder.jpg",
                        Semester = 1
                    },
                    new CourseModel
                    {
                        Name = "Course B",
                        Code = "CBD-2",
                        Description = "Course B semester 2",
                        ImgLoc = "/images/placeholder.jpg",
                        Semester = 2
                    }
                }
            };
            return View(overviewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
