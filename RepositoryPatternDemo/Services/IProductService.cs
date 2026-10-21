using RepositoryPatternDemo.Models;

namespace RepositoryPatternDemo.Services
{
    public interface IProductService
    {
        Task<List<Inventory>> GetAllProducts();

    }
}
