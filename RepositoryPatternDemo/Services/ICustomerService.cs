using RepositoryPatternDemo.Models;

namespace RepositoryPatternDemo.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomers();
        Task<int> InsertCustomer(Customer customer);

        Task<int> UpdateCustomer(Customer customer);

        Task<int> DeleteCustomer(int id);
    }
}
