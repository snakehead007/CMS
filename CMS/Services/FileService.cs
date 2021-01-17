using CMS.Interfaces.Repositories;
using Microsoft.AspNetCore.Hosting; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Services
{
    public class FileService : IFileService
    {
        private const string Folder = "Files";

        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnviroment)
        {
            _webHostEnvironment = webHostEnviroment;
        }
        public async Task<string> SaveFileAsync(string filename, Stream filestream)
        {
            string name = Guid.NewGuid().ToString() + Path.GetExtension(filename);

            string FolderPath = Path.Combine(_webHostEnvironment.WebRootPath, Folder);

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            string physicalPath = Path.Combine(FolderPath, name);
            string virtualPath = Path.Combine(Folder, name);

            using (FileStream fs = File.Create(physicalPath))
            {
                await filestream.CopyToAsync(fs);
            }

            return virtualPath;
            

        }

        public Task<Stream> GetFileAsync(string virtualPath)
        {
            string physicalPath = Path.Combine(_webHostEnvironment.WebRootPath, virtualPath);
            return Task.FromResult((Stream)File.OpenRead(physicalPath));
        }
    }
}
