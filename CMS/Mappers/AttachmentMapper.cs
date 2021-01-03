using CMS.Data.Entities;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Mappers
{
    public static class AttachmentMapper
    {
        public static AttachmentViewModel ToModel(this Attachment attachment)
        {
            return new AttachmentViewModel
            {
                AttachmentId = attachment.AttachmentId,
                Name = attachment.Name,
                CurrentVersion = attachment.CurrentVersion?.ToModel(),
                Versions = attachment.Versions?.Select(attachment => attachment.ToModel())?.ToList() ??
                    new List<AttachmentVersionViewModel>()
            };
        }
        public static AttachmentVersionViewModel ToModel(this AttachmentVersion version)
        {
            return new AttachmentVersionViewModel
            {
                VersionId = version.AttachmentVersionId,
                Location = version.Location,
                Size = version.Size,
                CreatedOn = version.CreatedOn
            };
        }
    }
}
