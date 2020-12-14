using CMS.Data.Entities;
using CMS.Models;

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
                Description = subject.Description
            };
        }
    }
}
