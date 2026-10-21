using Microsoft.AspNetCore.Mvc;
using RepositoryPatternDemo.Models;

namespace RepositoryPatternDemo.Repositories
{
    public interface ICustomerRepo
    {
        Task<List<Customer>> GetAllCustomers();
        Task<int> InsertCustomer(Customer customer);
        Task<int> UpdateCustomer(Customer customer);

        Task<int> DeleteCustomer(int id);
    }
}
