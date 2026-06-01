using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;


namespace EventEaseSystem.Services
{
    public class BlobService
    {
        private readonly BlobContainerClient _containerClient;

        public BlobService(IConfiguration configuration)
        {
            string connectionString = configuration["StorageConnectionString"];
            var blobServiceClient = new BlobServiceClient(connectionString);
            _containerClient = blobServiceClient.GetBlobContainerClient("venue-images");
            _containerClient.CreateIfNotExists();
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            string blobName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var blobClient = _containerClient.GetBlobClient(blobName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
            }

            // Get the URL and manually convert to HTTP if needed
            string url = blobClient.Uri.ToString();

            // Force HTTP for local development (Azurite doesn't support HTTPS)
            if (url.StartsWith("https://127.0.0.1"))
            {
                url = url.Replace("https://", "http://");
            }

            return url;
        }
    }
}
