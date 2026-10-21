using Microsoft.AspNetCore.Http;

namespace RepositoryPatternDemo.Services
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file);
    }
}
