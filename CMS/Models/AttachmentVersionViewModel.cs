using System;

namespace CMS.Models
{
    public class AttachmentVersionViewModel
    {
        public int? VersionId { get; set; }
        public string Location { get; set; }
        public int? Size { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}