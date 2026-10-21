namespace RepositoryPatternDemo.Repositories
{
    public interface IFileUploadRepository
    {
        Task<int> SaveFileRecordAsync(string fileName);
    }
}
