using Microsoft.Data.SqlClient;
using System.Data;
using RepositoryPatternDemo.Base;

namespace RepositoryPatternDemo.Repositories
{
    public class FileUploadRepository : BaseRepository, IFileUploadRepository
    {
        public FileUploadRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<int> SaveFileRecordAsync(string fileName)
        {
            object? result = await ExecuteScalarAsync(
                "FileUploadSP",
                new SqlParameter("@mode", SqlDbType.NVarChar, 20)
                { Value = "insert" },
                new SqlParameter("@FileName", SqlDbType.NVarChar, 255)
                { Value = fileName });

            return Convert.ToInt32(result);
        }
    }
}
