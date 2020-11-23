using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CMS.Models;
using CMS.Interfaces.Repositories;
using CMS.Data.Entities;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseRepository _courseRepository;

        public HomeController(ILogger<HomeController> logger, ICourseRepository courseRepository)
        {
            _logger = logger;
            this._courseRepository = courseRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<Course> courses = await _courseRepository.GetListAsync();

            OverviewModel overviewModel = new OverviewModel(courses);
            return View(overviewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
