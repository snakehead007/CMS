using CMS.Data.Repositories;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    public class ArchiveController : Controller
    {
        private readonly ArchiveRepository _archiveRepository;

        public ArchiveController(ArchiveRepository archiveRepository) 
        {
            _archiveRepository = archiveRepository;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult ArchivedCourses(string searchBy, string search) {
            List<CourseModel> archCourseList;
            switch (searchBy) 
            {
                case "Name":
                    archCourseList = _archiveRepository.GetByName(search);
                    break;

                case "Code":
                    archCourseList = _archiveRepository.GetByCode(search);
                    break;

                case "Semester":
                    //parse to int
                    var succes = int.TryParse(search, out int sem);

                    if (succes)
                    {
                        archCourseList = _archiveRepository.GetBySemester(sem);
                    }
                    else {
                        archCourseList = _archiveRepository.GetAllArchivedCourses();
                    }
                    break;

                default:
                    archCourseList = _archiveRepository.GetAllArchivedCourses();
                    break;
            }
            
            return View(archCourseList);
        }

    }
}
