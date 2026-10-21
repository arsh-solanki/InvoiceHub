using Microsoft.Data.SqlClient;
using System.Data;

namespace RepositoryPatternDemo.Base
{
    public abstract class BaseRepository
    {
        protected readonly string ConnectionString;

        protected BaseRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        protected async Task<DataTable> ExecuteDataTableAsync(
            string procedureName,
            params SqlParameter[] parameters)
        {
            DataTable table = new DataTable();

            await using SqlConnection connection = new SqlConnection(ConnectionString);
            await using SqlCommand cmd = new SqlCommand(procedureName,connection);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);

            await connection.OpenAsync();

            await using SqlDataReader dr = await cmd.ExecuteReaderAsync();
          
            table.Load(dr);

            return table;
        }

        protected async Task<int> ExecuteNonQueryAsync(
            string procedureName,
            params SqlParameter[] parameters)
        {
            await using SqlConnection connection = new SqlConnection(ConnectionString);
            await using SqlCommand cmd = new SqlCommand(procedureName, connection);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);

            await connection.OpenAsync();

            return await cmd.ExecuteNonQueryAsync();
        }

        protected async Task<int> ExecuteNonQueryAsync(
            SqlConnection connection,
            SqlTransaction transaction,
            string procedureName,
            params SqlParameter[] parameters)
        {
            await using SqlCommand cmd = new SqlCommand(
                procedureName,
                connection,
                transaction);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);

            return await cmd.ExecuteNonQueryAsync();
        }

        protected async Task<object?> ExecuteScalarAsync(
            string procedureName,
            params SqlParameter[] parameters)
        {
            await using SqlConnection connection = new SqlConnection(ConnectionString);

            await using SqlCommand cmd = new SqlCommand(procedureName, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);

            await connection.OpenAsync();
            return await cmd.ExecuteScalarAsync();

        }
    }
}
        //protected async Task<List<T>> ExecuteReaderAsync<T>(
        //    string productName,
        //    SqlParameter[] parameters
        //    )
        //{
        //    var result = new List<T>();
        //    using var connection = new SqlConnection(_connectionString);
        //    using var command = new SqlCommand("CustomerDetail", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.AddRange(parameters);

        //    await connection.OpenAsync();
        //    using var reader = await command.ExecuteReaderAsync();

        //    while (await reader.ReadAsync())
        //    {
        //        //result.Add(reader);
        //    }
        //    return result;
        //}
