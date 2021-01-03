using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Interfaces.Repositories;
using CMS.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class AttachmentController : Controller
    {
        private readonly IAttachmentRepository _attachmentRepository;

        public AttachmentController(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }

        public async Task<IActionResult> ViewAttachment(int attachmentId)
        {
            var attachment = await  _attachmentRepository.GetAttachmentAsync(attachmentId);
            return View(attachment.ToModel());
        }
    }
}
