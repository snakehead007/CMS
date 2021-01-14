using CMS.Data.Entities;
using CMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Mappers
{
    public static class SubjectMapper
    {
        public static SubjectViewModel ToModel(this Subject subject)
        {
            return new SubjectViewModel
            {
                SubjectId = subject.SubjectId,
                Name = subject.Name,
                Description = subject.Description,
                Attachments = subject.Attachments?.Select(subject => subject.ToModel())?.ToList() ??
                    new List<AttachmentViewModel>()
            };
        }

        public static SubjectArchive ToArchive(this Subject subject)
        {
            return new SubjectArchive
            {
                SubjectId = subject.SubjectId,
                Name = subject.Name,
                Description = subject.Description
            };
        }
    }
}