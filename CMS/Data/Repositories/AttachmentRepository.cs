using CMS.Data.Entities;
using CMS.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Data.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
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
            await db.SaveChangesAsync();
            // Setversion
            await SetVersion(entity.Entity, version);
            // Attachment returnen
            return entity.Entity;
        }

        public Task<Attachment> GetAttachmentAsync(int attachmentId)
        {
            return db.Attachments.Include(x => x.CurrentVersion).Include(x => x.Versions)
                .SingleOrDefaultAsync(x => x.AttachmentId == attachmentId);
        }

        public async Task<Attachment> AddVersionToAttachmentAsync(int attachmentId, AttachmentVersion version)
        {
            var attachment = await GetAttachmentAsync(attachmentId);
            attachment.Versions.Add(version);
            await db.SaveChangesAsync();

            await SetVersion(attachment, version);
            return attachment;

        }

        public async Task<Attachment> changeVersionOfAttachmentAsync(int attachmentId, int versionId)
        {
            var attachment = await GetAttachmentAsync(attachmentId);
            var version = attachment.Versions.Single(x => x.AttachmentVersionId == versionId);
            await SetVersion(attachment, version);
            return attachment;
        }

        private async Task SetVersion(Attachment attachment, AttachmentVersion version)
        {
            attachment.CurrentVersion = version;
            await db.SaveChangesAsync();
        }
    }
}
