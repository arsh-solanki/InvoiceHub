using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;
namespace RepositoryPatternDemo.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _uploadFolder;

        public FileStorageService(IConfiguration iconfiguration, IWebHostEnvironment environment)
        {
            string configuredFolder = iconfiguration["FileStorage:UploadFolder"];

            _uploadFolder = Path.IsPathRooted(configuredFolder) ? configuredFolder
                : Path.Combine(environment.ContentRootPath);

        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            string filename = Path.GetFileName(file.FileName);

            Directory.CreateDirectory(_uploadFolder);

            string filePath = Path.Combine(_uploadFolder,filename);

            await using FileStream stream = new FileStream
                (filePath,FileMode.CreateNew, FileAccess.Write, FileShare.None);

            await file.CopyToAsync(stream);

            return filename;
        }
    }
}
