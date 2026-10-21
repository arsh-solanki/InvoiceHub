using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories;

namespace RepositoryPatternDemo.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;

        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;        
        }

        public async Task<List<Inventory>> GetAllProducts()
        {
            return await _productRepo.GetAllProducts();
        }
    }
}
