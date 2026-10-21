using Microsoft.Data.SqlClient;
using System.Data;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Base;

namespace RepositoryPatternDemo.Repositories
    {
        public class InventoryRepository : BaseRepository, IInventoryRepo
        {
            public InventoryRepository(IConfiguration configuration) : base(configuration)
            {
            }

            public async Task<List<Inventory>> GetAllInventorys()
            {
                List<Inventory> inventories = new List<Inventory>();

                DataTable table = await ExecuteDataTableAsync(
                    "InventoryM",
                    new SqlParameter("@mode", SqlDbType.NVarChar, 20) { Value = "select" });

                foreach (DataRow row in table.Rows)
                {
                    Inventory inventory = new Inventory();
                    inventory.InventoryID = Convert.ToInt32(row["InventoryID"]);
                    inventory.ProductName = row["ProductName"].ToString();
                    inventory.ProductCategory = row["ProductCategory"].ToString();
                    inventory.ProductBuyPrice = Convert.ToDecimal(row["ProductBuyPrice"]);
                    inventory.ProductSellPrice = Convert.ToDecimal(row["ProductSellPrice"]);
                    inventory.ProductQty = Convert.ToInt32(row["ProductQty"]);
                    inventory.ProductDescription = row["ProductDescription"].ToString();
                    inventory.ProductReorderQty = Convert.ToInt32(row["ProductReorderQty"]);
                    inventory.ProductCreatedDate = Convert.ToDateTime(row["ProductCreatedDate"]);
                    inventory.ProductUpdatedDate = Convert.ToDateTime(row["ProductUpdatedDate"]);
                    inventory.Isactive = Convert.ToBoolean(row["IsActive"]);
                    inventory.ProductSKU = row["ProductSKU"].ToString();
                    inventory.ImagePath = row["ImagePath"].ToString();

                    inventories.Add(inventory);
                }
                return inventories;
            }

        // Stubs to satisfy the interface. Replace with real implementations.
        //ProductName, ProductCategory, ProductBuyPrice, ProductSellPrice, ProductQty,
        //ProductDescription, ProductReorderQty, ProductCreatedDate, ProductUpdatedDate,
        //Isactive, ProductSKU, ImagePath
        public async Task<int> InsertInventory(Inventory inventory)
            {
            return await ExecuteNonQueryAsync(
                "InventoryM",
                new SqlParameter("@mode", SqlDbType.NVarChar, 20)
                { Value="insert"},
                 new SqlParameter("@ProductName", SqlDbType.NVarChar, 20)
                 { Value = inventory.ProductName },
                  new SqlParameter("@ProductCategory", SqlDbType.NVarChar, 20)
                  { Value = inventory.ProductCategory },
                   new SqlParameter("@ProductBuyPrice", SqlDbType.Decimal, 20)
                   { Value = inventory.ProductBuyPrice},
                    new SqlParameter("@ProductSellPrice", SqlDbType.Decimal, 20)
                    { Value = inventory.ProductSellPrice },
                     new SqlParameter("@ProductQty", SqlDbType.Int, 20)
                     { Value = inventory.ProductQty },
                      new SqlParameter("@ProductDescription", SqlDbType.NVarChar, 20)
                      { Value = inventory.ProductDescription },
                       new SqlParameter("@ProductReorderQty", SqlDbType.Int, 20)
                       { Value = inventory.ProductReorderQty },
                        new SqlParameter("@ProductCreatedDate", SqlDbType.DateTime, 20)
                        { Value = inventory.ProductCreatedDate },
                         new SqlParameter("@ProductUpdatedDate", SqlDbType.DateTime, 20)
                         { Value = inventory.ProductUpdatedDate },
                          new SqlParameter("@Isactive", SqlDbType.Bit, 20)
                          { Value = inventory.Isactive },
                           new SqlParameter("@ProductSKU", SqlDbType.NVarChar, 50)
                           { Value = inventory.ProductSKU },
                            new SqlParameter("@ImagePath", SqlDbType.NVarChar, 50)
                            { Value = inventory.ImagePath });
                //throw new NotImplementedException();
            }

            public async Task<int> UpdateInventory(Inventory inventory)
            {
            return await ExecuteNonQueryAsync(
              "InventoryM",
              new SqlParameter("@mode", SqlDbType.NVarChar, 20)
              { Value = "update" },
              new SqlParameter("@InventoryID", SqlDbType.Int)
              { Value= inventory.InventoryID},
               new SqlParameter("@ProductName", SqlDbType.NVarChar, 20)
               { Value = inventory.ProductName },
                new SqlParameter("@ProductCategory", SqlDbType.NVarChar, 20)
                { Value = inventory.ProductCategory },
                 new SqlParameter("@ProductBuyPrice", SqlDbType.Decimal)
                 { Value = inventory.ProductBuyPrice },
                  new SqlParameter("@ProductSellPrice", SqlDbType.Decimal)
                  { Value = inventory.ProductSellPrice },
                   new SqlParameter("@ProductQty", SqlDbType.Int)
                   { Value = inventory.ProductQty },
                    new SqlParameter("@ProductDescription", SqlDbType.NVarChar, 20)
                    { Value = inventory.ProductDescription },
                     new SqlParameter("@ProductReorderQty", SqlDbType.Int)
                     { Value = inventory.ProductReorderQty },
                      new SqlParameter("@ProductCreatedDate", SqlDbType.DateTime)
                      { Value = inventory.ProductCreatedDate },
                       new SqlParameter("@ProductUpdatedDate", SqlDbType.DateTime)
                       { Value = inventory.ProductUpdatedDate },
                        new SqlParameter("@Isactive", SqlDbType.Bit)
                        { Value = inventory.Isactive },
                         new SqlParameter("@ProductSKU", SqlDbType.NVarChar, 50)
                         { Value = inventory.ProductSKU },
                          new SqlParameter("@ImagePath", SqlDbType.NVarChar, 50)
                          { Value = inventory.ImagePath });
            //throw new NotImplementedException();
        }

            public async Task<int> DeleteInventory(int id)
            {
            return await ExecuteNonQueryAsync(
              "InventoryM",
              new SqlParameter("@mode", SqlDbType.NVarChar, 20)
              { Value = "delete" },
              new SqlParameter("@InventoryID", SqlDbType.Int)
              { Value = id });
                //throw new NotImplementedException();
            }
        }
    }
