using Microsoft.Data.SqlClient;
using System.Data;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Base;

namespace RepositoryPatternDemo.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepo
    {
        public ProductRepository(IConfiguration configuration) : base(configuration)
        { 
        }

        public async Task<List<Inventory>> GetAllProducts()
        {
            List<Inventory> products = new List<Inventory>();

            DataTable table = await ExecuteDataTableAsync(
                "ProductDetail",
                new SqlParameter("@mode", SqlDbType.NVarChar, 20)
                { Value = "select" });

            foreach (DataRow row in table.Rows)
            {
                Inventory inventory = new Inventory();
                inventory.InventoryID = Convert.ToInt32(row["InventoryID"]);
                inventory.ProductName = row["ProductName"].ToString();
                inventory.ProductSellPrice = Convert.ToDecimal(row["ProductSellPrice"]);
                inventory.ProductQty = Convert.ToInt32(row["ProductQty"]);
                inventory.ProductSKU = row["ProductSKU"].ToString();

                products.Add(inventory);
            }
            return products;
        }
    }
}
