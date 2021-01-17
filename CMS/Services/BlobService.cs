using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CMS.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Services
{

    public class BlobService : IFileService
    {
        public string Randomize(string prefix = "sample") =>
            $"{prefix}-{Guid.NewGuid()}";

        private readonly IConfiguration _config;
        public BlobService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> SaveFileAsync(string filename, Stream filestream)
        {
            // Create a unique file location
            string location = Guid.NewGuid().ToString()
            + Path.GetExtension(filename);
            // Get the blob client
            var client = await GetBlobAsync(location);
            // Upload the file
            await client.UploadAsync(filestream, true);
            // Return the location to this blob.
            return location;
        }
        public async Task<Stream> GetFileAsync(string location)
        {
            // Get the blob client
            var client = await GetBlobAsync(location);
            // Download the file
            BlobDownloadInfo downloadInfo = await client.DownloadAsync();
            // Return the stream of this blob
            return downloadInfo.Content;
        }

        private async Task<BlobClient> GetBlobAsync(string location)
        {
            // Create a BlobServiceClient object
            BlobServiceClient blobServiceClient =
            new BlobServiceClient(_config.GetValue<string>(
                "BlobStorage:ConnectionString"));
            // Create the containerClient
            BlobContainerClient containerClient =
            blobServiceClient.GetBlobContainerClient(Randomize("blob"));
            // Create the container if it doesn't exist
            await containerClient.CreateIfNotExistsAsync();
            // Return the reference to this blob
            return containerClient.GetBlobClient(location);
        }
    }
}
