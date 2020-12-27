using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Data.Entities
{
    public class Attachment
    {
        public int AttachmentId { get; set; }
        public string Name { get; set; }
        public AttachmentVersion CurrentVersion { get; set; }
        public List<AttachmentVersion> Versions { get; set; }
    }
}
