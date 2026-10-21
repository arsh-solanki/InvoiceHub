
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Services;

namespace RepositoryPatternDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            List<Customer> customers = await _customerService.GetAllCustomers();

            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCustomer([FromBody] Customer customer)
        {
            await _customerService.InsertCustomer(customer);
            return Ok(new { message = "customer inserted successfully"});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(
            int id, [FromBody] Customer customer)
        {
            await _customerService.UpdateCustomer(customer);
            return Ok(new { message="customer updated successfully"});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomer(id);
            return Ok(new {message="customer delete successfully"});
        }
    }
}
        //public IActionResult Index()
        //{
        //    return View();
        //}
