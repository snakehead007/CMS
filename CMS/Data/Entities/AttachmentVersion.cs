using System;

namespace CMS.Data.Entities
{
    public class AttachmentVersion
    {
        public int AttachmentVersionId { get; set; }
        public string Location { get; set; }
        public int Size { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}