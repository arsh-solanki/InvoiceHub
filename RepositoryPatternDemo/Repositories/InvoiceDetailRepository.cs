using Microsoft.Data.SqlClient;
using RepositoryPatternDemo.Base;
using RepositoryPatternDemo.Models;
using System.Data;

namespace RepositoryPatternDemo.Repositories
{
    public class InvoiceDetailRepository : BaseRepository, IInvoiceDetailRepo
    {
        public InvoiceDetailRepository(IConfiguration configuration) : base(configuration)
        { }

        public async Task<List<InvoiceDetail>> GetAllInvoices()
        {
            List<InvoiceDetail> invoices = new List<InvoiceDetail>();

            DataTable table = await ExecuteDataTableAsync(
        "InvoiceDetailSP", new SqlParameter("@mode", SqlDbType.NVarChar, 20)
        { Value="select"},
        new SqlParameter("@InvoiceID", SqlDbType.Int)
        { Value=0});

            foreach(DataRow row in table.Rows)
            {
                InvoiceDetail invoice = new InvoiceDetail();
                invoice.InvoiceID = Convert.ToInt32(row["InvoiceID"]);
                invoice.InventoryID = Convert.ToInt32(row["InventoryID"]);
                invoice.ProductName = row["ProductName"].ToString();
                invoice.ProductSKU = row["ProductSKU"].ToString();
                invoice.Quantity = Convert.ToInt32(row["Quantity"]);
                invoice.UnitPrice = Convert.ToDecimal(row["UnitPrice"]);
                invoice.DiscountAmount = Convert.ToDecimal(row["DiscountAmount"]);
                invoice.TaxAmount = Convert.ToDecimal(row["TaxAmount"]);

                invoices.Add(invoice);
            }
            return invoices;
        }

        //public async Task<int> InsertInvoiceDetail(InvoiceDetail detail)
        //{
        //    return await ExecuteNonQueryAsync(
        //        "InvoiceDetailSP",
        //         new SqlParameter("@mode", SqlDbType.NVarChar, 20)
        //         { Value = "insert" },
        //          new SqlParameter("@InvoiceID", SqlDbType.Int)
        //          { Value = detail.InvoiceID },
        //           new SqlParameter("@InventoryID", SqlDbType.Int)
        //           { Value = detail.InventoryID },
        //            new SqlParameter("@ProductName", SqlDbType.NVarChar, 50)
        //            { Value = (object)detail.ProductName ?? DBNull.Value },
        //             new SqlParameter("@ProductSKU", SqlDbType.NVarChar, 10)
        //             { Value = (object)detail.ProductSKU ?? DBNull.Value },
        //              new SqlParameter("@Quantity", SqlDbType.Int)
        //              { Value = detail.Quantity },
        //               new SqlParameter("@UnitPrice", SqlDbType.Decimal)
        //               { Value = detail.UnitPrice },
        //                new SqlParameter("@DiscountAmount", SqlDbType.Decimal)
        //                { Value = detail.DiscountAmount },
        //                 new SqlParameter("@TaxAmount", SqlDbType.Decimal)
        //                 { Value = detail.TaxAmount }
        //     );
        //}
    }
}
