using RepositoryPatternDemo.Models;

namespace RepositoryPatternDemo.Repositories
{
    public interface IProductRepo
    {
        Task<List<Inventory>> GetAllProducts();
    }
}
