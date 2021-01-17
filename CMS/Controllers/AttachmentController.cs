using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Data.Entities;
using CMS.Interfaces.Repositories;
using CMS.Mappers;
using CMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Authorize]
    public class AttachmentController : Controller
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IFileService _fileService;
        private readonly ICourseRepository _courseRepository;
        public AttachmentController(ICourseRepository courseRepository, IFileService fileService, IAttachmentRepository attachmentRepository)
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
        [Authorize(Roles ="Admin,Lector")]
        public async Task<IActionResult> UploadAttachment(int subjectId, IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("No file was posted");
            }
            string virtualPath = await _fileService.SaveFileAsync(file.FileName, file.OpenReadStream());
            var version = new AttachmentVersion
            {
                Location = virtualPath,
                Size = (int)file.Length,
                CreatedOn = DateTime.UtcNow
            };
            var attachment = await _attachmentRepository.createAttachment(file.FileName, version);
            await _courseRepository.AddAttachmentToSubjectAsync(subjectId, attachment);
            return RedirectToAction("ViewAttachment", new { attachmentId = attachment.AttachmentId});

        }

        [HttpPost]
        [Authorize(Roles = "Admin,Lector")]
        public async Task<IActionResult> UploadVersion(int attachmentId, IFormFile file)
        {
            string virtualPath = await _fileService.SaveFileAsync(file.FileName, file.OpenReadStream());
            var version = new AttachmentVersion
            {
                Location = virtualPath,
                Size = (int)file.Length,
                CreatedOn = DateTime.UtcNow
            };
            var attachment = await _attachmentRepository.AddVersionToAttachmentAsync(attachmentId, version);
            return RedirectToAction("ViewAttachment", new { attachmentId = attachment.AttachmentId });
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Lector")]
        public async Task<IActionResult> ChangeVersion(int attachmentId, int versionId)
        {
            var attachment = await _attachmentRepository.changeVersionOfAttachmentAsync(attachmentId, versionId);
            return RedirectToAction("ViewAttachment", new { attachmentId = attachment.AttachmentId });
        }
        [HttpGet]
        public async Task<IActionResult> GetFileContent(int attachmentId)
        {
            var attachment = await _attachmentRepository.GetAttachmentAsync(attachmentId);
            var stream = await _fileService.GetFileAsync(attachment.CurrentVersion.Location);
            return File(stream, "application/octet-stream", attachment.Name);
        }
    }
}
