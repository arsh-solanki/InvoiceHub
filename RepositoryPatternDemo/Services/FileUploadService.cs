using Microsoft.AspNetCore.Http;
using RepositoryPatternDemo.Repositories;

namespace RepositoryPatternDemo.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IFileUploadRepository _fileUploadRepository;

        public FileUploadService(
           IFileStorageService fileStorageService,
          IFileUploadRepository fileUploadRepository)
        {
            _fileStorageService = fileStorageService;
            _fileUploadRepository = fileUploadRepository;
        }

        public async Task<int> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("select a file");
            }
            string fileName = await _fileStorageService.SaveFileAsync(file);
            try
            {
                int fileId = await _fileUploadRepository.SaveFileRecordAsync(fileName);
                return fileId;
            }
            catch
            {
                string uploadFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),"uploads");

                string filePath = Path.Combine(uploadFolder,fileName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                throw;
            }
        }
    }
}
