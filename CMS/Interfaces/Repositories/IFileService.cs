using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Interfaces.Repositories
{
    public interface IFileService
    {
        
        public Task<string> SaveFileAsync(string filename, Stream filestream);

        public Task<Stream> GetFileAsync(string virtualPath);
    }
}
