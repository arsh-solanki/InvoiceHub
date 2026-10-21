using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories;

namespace RepositoryPatternDemo.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _customerRepo.GetAllCustomers();
        }

        public async Task<int> InsertCustomer(Customer customer)
        {
            return await _customerRepo.InsertCustomer(customer);
        }

        public async Task<int> UpdateCustomer(Customer customer)
        {
            return await _customerRepo.UpdateCustomer(customer);
        }

        public async Task<int> DeleteCustomer(int id)
        {
            return await _customerRepo.DeleteCustomer(id);    
        }
    }
}
