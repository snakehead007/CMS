using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class AttachmentViewModel
    {
        public int? AttachmentId { get; set; }
        public string Name { get; set; }
        public AttachmentVersionViewModel CurrentVersion { get; set; }
        public List<AttachmentVersionViewModel> Versions { get; set; }
    }
}
