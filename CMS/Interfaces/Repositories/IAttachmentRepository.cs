using CMS.Data.Entities;
using System.Threading.Tasks;

namespace CMS.Interfaces.Repositories
{
    public interface IAttachmentRepository
    {
        Task<Attachment> createAttachment(string filename, AttachmentVersion version);

        Task<Attachment> GetAttachmentAsync(int attachmentId);

        Task<Attachment> AddVersionToAttachmentAsync(int attachmentId, AttachmentVersion version);

        Task<Attachment> changeVersionOfAttachmentAsync(int attachmentId, int versionId);

    }
}
