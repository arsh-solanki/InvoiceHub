namespace RepositoryPatternDemo.Services
{
    public interface IFileUploadService
    {
        Task<int> UploadFileAsync(IFormFile file);
    }
}
