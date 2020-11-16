using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class ErrorViewModel
    {
        private string _requestId;
        public string RequestId { get { return _requestId; } set { ShowRequestId = true; _requestId = value; } }
        public bool ShowRequestId;
    }
}
