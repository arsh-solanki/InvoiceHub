using Microsoft.Data.SqlClient;
using System.Data;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Base;

namespace RepositoryPatternDemo.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepo
    {

        public CustomerRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            DataTable table = await ExecuteDataTableAsync(
                "CustomerDetail",
                new SqlParameter("@mode", SqlDbType.NVarChar, 20)
                { Value="select"});

                     foreach(DataRow row in table.Rows)
            {
                Customer customer = new Customer();
                customer.Id = Convert.ToInt32(row["Id"]);
                customer.CustomerName = row["CustomerName"].ToString();
                customer.Email = row["Email"].ToString();
                customer.AddressLine1 = row["AddressLine1"].ToString();
                customer.AddressLine2 = row["AddressLine2"].ToString();
                customer.City = row["City"].ToString();
                customer.State = row["State"].ToString();
                customer.Country = row["Country"].ToString();
                customer.PostalCode = row["PostalCode"].ToString();

                customers.Add(customer);
            }
            return customers;
        }

        public async Task<int> InsertCustomer(Customer customer)
        {
            return await ExecuteNonQueryAsync("CustomerDetail",
                new SqlParameter("@mode", SqlDbType.NVarChar, 20)
                { Value = "insert" },
                 new SqlParameter("@CustomerName", SqlDbType.NVarChar, 50)
                 { Value = customer.CustomerName },
                  new SqlParameter("@Email", SqlDbType.NVarChar, 50)
                  { Value = customer.Email },
                   new SqlParameter("@AddressID", SqlDbType.Int)
                   { Value = customer.AddressID },
                    new SqlParameter("@IsActive", SqlDbType.Bit)
                    { Value = customer.IsActive },
                     new SqlParameter("@CountryId", SqlDbType.NVarChar, 20)
                     { Value = customer.CountryId }

                );
        }

        public async Task<int> UpdateCustomer(Customer customer)
        {
            return await ExecuteNonQueryAsync("CustomerDetail",
                new SqlParameter("@mode", SqlDbType.NVarChar, 20)
                { Value = "update" },
                new SqlParameter("@Id", SqlDbType.Int)
                { Value = customer.Id},
                 new SqlParameter("@CustomerName", SqlDbType.NVarChar, 50)
                 { Value = customer.CustomerName },
                  new SqlParameter("@Email", SqlDbType.NVarChar, 50)
                  { Value = customer.Email },
                   new SqlParameter("@AddressID", SqlDbType.Int)
                   { Value = customer.AddressID },
                    new SqlParameter("@IsActive", SqlDbType.Bit)
                    { Value = customer.IsActive },
                     new SqlParameter("@CountryId", SqlDbType.NVarChar, 20)
                     { Value = customer.CountryId }
                );
        }

        public async Task<int> DeleteCustomer(int id)
        {
            return await ExecuteNonQueryAsync(
                "CustomerDetail",
                new SqlParameter("@mode", SqlDbType.NVarChar, 20)
                { Value="delete"},
                new SqlParameter("@Id", SqlDbType.Int)
                { Value= id});
        }
    }
}

// using var connection = new SqlConnection(_connectionString);
// using var command = new SqlCommand("CustomerDetail", connection);
// command.CommandType = CommandType.StoredProcedure;
// command.Parameters.AddWithValue("@mode","select");
//await connection.OpenAsync();

// SqlDataReader dr = await command.ExecuteReaderAsync();
// while (await dr.ReadAsync())
//private readonly string _connectionString;
//_connectionString = configuration.GetConnectionString("DefaultConnection");
