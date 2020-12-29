using CMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Data.Repositories
{
    public class AttachmentRepository
    {
        public DataContext db;

        public AttachmentRepository(DataContext context)
        {
            this.db = context;
        }

        public async Task<Attachment> createAttachment(string filename, AttachmentVersion version) {
            Attachment attachment = new Attachment
            {
                Name = filename,
                Versions = new List<AttachmentVersion> { version},
                CurrentVersion = null
            };

            var entity = await db.Attachments.AddAsync(attachment);

            // Setversion

            // Attachment returnen
        }
    }
}
