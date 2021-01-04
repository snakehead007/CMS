using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Data.Entities;
using CMS.Interfaces.Repositories;
using CMS.Mappers;
using CMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class AttachmentController : Controller
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly FileService _fileService;
        private readonly ICourseRepository _courseRepository;
        public AttachmentController(ICourseRepository courseRepository,FileService fileService, IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
            _fileService = fileService;
            _courseRepository = courseRepository;

        }
        [HttpGet]
        public async Task<IActionResult> ViewAttachment(int attachmentId)
        {
            var attachment = await _attachmentRepository.GetAttachmentAsync(attachmentId);
            return View(attachment.ToModel());
        }

        [HttpPost]
        public async Task<IActionResult> UploadAttachment(int subjectId, IFormFile file)
        {
            string virtualPath = await _fileService.SaveFileAsync(file.FileName, file.OpenReadStream());
            var version = new AttachmentVersion
            {
                Location = virtualPath,
                Size = (int)file.Length,
                CreatedOn = DateTime.UtcNow
            };
            var attachment = await _attachmentRepository.createAttachment(file.FileName, version);
            _courseRepository.AddAttachmentToSubjectAsync(subjectId, attachment);
            return RedirectToAction("ViewAttachment", new { attachmentId = attachment.AttachmentId});

        }

    }
}
