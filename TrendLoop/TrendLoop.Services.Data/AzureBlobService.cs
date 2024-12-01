using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using TrendLoop.Services.Data.Interfaces;
using Microsoft.Extensions.Configuration;

namespace TrendLoop.Services.Data
{
    public class AzureBlobService : IBlobService
    {
        private readonly BlobContainerClient blobContainerClient;

        public AzureBlobService(IConfiguration config)
        {
            var azureBlobConfig = config.GetSection("AzureBlobStorage");
            blobContainerClient = new BlobContainerClient(azureBlobConfig["ConnectionString"], azureBlobConfig["ContainerName"]);
            blobContainerClient.CreateIfNotExists(PublicAccessType.Blob);
        }

        // Upload file to Azure Blob Storage
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            string blobName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);

            await using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });

            return blobClient.Uri.ToString();
        }

        // Delete file from Azure Blob Storage
        public async Task DeleteFileAsync(string blobName)
        {
            BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }

        // Upload image from URL to Azure Blob Storage
        public async Task<string> UploadFileFromUrlAsync(string fileUrl)
        {
            using var httpClient = new HttpClient();

            try
            {
                // Get the image stream from the URL
                var response = await httpClient.GetAsync(fileUrl);
                response.EnsureSuccessStatusCode();
                await using var fileStream = await response.Content.ReadAsStreamAsync();

                // Extract file extension from URL
                var fileExtension = Path.GetExtension(new Uri(fileUrl).AbsolutePath);
                string blobName = Guid.NewGuid().ToString() + fileExtension;

                // Upload the image stream to Azure Blob Storage
                BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);
                await blobClient.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = response.Content.Headers.ContentType?.ToString() });

                return blobClient.Uri.ToString(); // Return the blob URL
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upload file from URL", ex);
            }
        }
    }
}
