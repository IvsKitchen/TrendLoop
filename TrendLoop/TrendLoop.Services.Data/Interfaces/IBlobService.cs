using Microsoft.AspNetCore.Http;

namespace TrendLoop.Services.Data.Interfaces
{
    public interface IBlobService
    {
        public Task<string> UploadFileAsync(IFormFile file);

        public Task DeleteFileAsync(string blobName);

        public Task<string> UploadFileFromUrlAsync(string fileUrl);

    }
}
