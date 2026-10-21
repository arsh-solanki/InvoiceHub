using Microsoft.Data.SqlClient;
using RepositoryPatternDemo.Base;
using RepositoryPatternDemo.Models;
using System.Data;

namespace RepositoryPatternDemo.Repositories
{
    public class InvoiceMasterRepository : BaseRepository, IInvoiceMasterRepo
    {
        public InvoiceMasterRepository(IConfiguration configuration) : base(configuration)
        { }

        public async Task<List<InvoiceMaster>> GetAllInvoices()
        {
            List<InvoiceMaster> invoices = new List<InvoiceMaster>();

            DataTable table = await ExecuteDataTableAsync(
                "InvoiceMasterSP",
                new SqlParameter("@mode", SqlDbType.NVarChar, 20)
                { Value = "select" },
                 new SqlParameter("@InvoiceID", SqlDbType.NVarChar, 20)
                 { Value = 0 });

            foreach (DataRow row in table.Rows)
            {
                InvoiceMaster invoice = new InvoiceMaster();
                invoice.InvoiceID = Convert.ToInt32(row["InvoiceID"]);
                invoice.InvoiceNumber = row["InvoiceNumber"].ToString();
                invoice.CustomerID = Convert.ToInt32(row["CustomerID"]);
                invoice.InvoiceDate = Convert.ToDateTime(row["InvoiceDate"]);
                invoice.SubTotal = Convert.ToDecimal(row["SubTotal"]);
                invoice.DiscountAmount = Convert.ToDecimal(row["DiscountAmount"]);
                invoice.TaxAmount = Convert.ToDecimal(row["TaxAmount"]);
                invoice.GrandTotal = Convert.ToDecimal(row["GrandTotal"]);
                invoice.PaidAmount = Convert.ToDecimal(row["PaidAmount"]);
                invoice.UnpaidAmount = Convert.ToDecimal(row["UnpaidAmount"]);
                invoice.PaymentStatus = row["PaymentStatus"].ToString();
                invoice.PaymentMethod = row["PaymentMethod"].ToString();
                invoice.Notes = row["Notes"].ToString();
                invoice.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                invoice.UpdatedDate = Convert.ToDateTime(row["UpdatedDate"]);
                invoice.IsActive = Convert.ToBoolean(row["IsActive"]);

                invoices.Add(invoice);
            }
            return invoices;
        }

        public async Task<int> SaveInvoiceAsync(SaveInvoiceRequest request)
        {
            await using SqlConnection connection = new SqlConnection(ConnectionString);
            await connection.OpenAsync();

            await using SqlTransaction transaction = (SqlTransaction)await connection.BeginTransactionAsync();

            try
            {
                decimal subTotal = 0;
                decimal itemDiscountTotal = 0;
                decimal itemTaxTotal = 0;

                var items = new List<(
                    int InventoryID,
                    string ProductName,
                    string ProductSKU,
                    int Quantity,
                    decimal UnitPrice,
                      decimal DiscountAmount,
                    decimal TaxAmount,
                    decimal TotalAmount)>();

                foreach (var item in request.Items)
                {
                    await using SqlCommand productCommand = new SqlCommand(
                        @"select ProductName, ProductSKU, ProductSellPrice
                    from InventoryMaster where InventoryID = @InventoryID
                    and IsActive = 1",
                        connection,transaction);

                    productCommand.Parameters.Add(
                        "@InventoryID", SqlDbType.Int).Value = item.InventoryID;

                    await using SqlDataReader reader =
                        await productCommand.ExecuteReaderAsync();

                    string productName = reader["ProductName"].ToString();
                    string productSKU = reader["ProductSKU"].ToString().Trim();
                    decimal unitPrice = Convert.ToDecimal(reader["ProductSellPrice"]);
                    await reader.DisposeAsync();

                    decimal gross = unitPrice * item.Quantity;
                    decimal lineTotal = gross - item.DiscountAmount + item.TaxAmount;

                    subTotal += gross;
                    itemDiscountTotal += item.DiscountAmount;
                    itemTaxTotal += item.TaxAmount;

                    items.Add((
                        item.InventoryID, 
                        productName, productSKU, item.Quantity, unitPrice
                        , item.DiscountAmount, item.TaxAmount, lineTotal));
                }
                decimal totalDiscount = itemDiscountTotal + request.DiscountAmount;
                decimal totalTax = itemTaxTotal + request.TaxAmount;
                decimal grandTotal = subTotal - totalDiscount + totalTax;
                decimal unpaidAmount = grandTotal - request.PaidAmount;

                string paymentStatus = unpaidAmount == 0
                    ? "Paid"
                    : request.PaidAmount > 0
                    ? "Partial"
                    : "Unpaid";

                SqlParameter invoiceIdParameter = new SqlParameter("@InvoiceID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                await ExecuteNonQueryAsync(
           connection,
           transaction,
           "InvoiceMasterSP",
           new SqlParameter("@mode", SqlDbType.NVarChar, 20)
           { Value = "insert" },
           new SqlParameter("@InvoiceNumber", SqlDbType.NVarChar, 50)
           { Value = request.InvoiceNumber },
           new SqlParameter("@CustomerID", SqlDbType.Int)
           { Value = request.CustomerID },
           new SqlParameter("@InvoiceDate", SqlDbType.NVarChar, 50)
           { Value = request.InvoiceDate.ToString("yyyy-MM-dd") },
           new SqlParameter("@SubTotal", SqlDbType.Decimal)
           { Precision = 18, Scale = 2, Value = subTotal },
           new SqlParameter("@DiscountAmount", SqlDbType.Decimal)
           { Precision = 18, Scale = 2, Value = totalDiscount },
           new SqlParameter("@TaxAmount", SqlDbType.Decimal)
           { Precision = 18, Scale = 2, Value = totalTax },
           new SqlParameter("@GrandTotal", SqlDbType.Decimal)
           { Precision = 18, Scale = 2, Value = grandTotal },
           new SqlParameter("@PaidAmount", SqlDbType.Decimal)
           { Precision = 18, Scale = 2, Value = request.PaidAmount },
           new SqlParameter("@UnpaidAmount", SqlDbType.Decimal)
           { Precision = 18, Scale = 2, Value = unpaidAmount },
           new SqlParameter("@PaymentStatus", SqlDbType.NChar, 20)
           { Value = paymentStatus },
           new SqlParameter("@PaymentMethod", SqlDbType.NChar, 20)
           { Value = (object?)request.PaymentMethod ?? DBNull.Value },
           new SqlParameter("@Notes", SqlDbType.NVarChar, 50)
           { Value = (object?)request.Notes ?? DBNull.Value },
           new SqlParameter("@IsActive", SqlDbType.Bit)
           { Value = true },
           invoiceIdParameter);

                int invoiceID = Convert.ToInt32(invoiceIdParameter.Value);

                foreach (var item in items)
                {
                    await ExecuteNonQueryAsync(
                        connection,
                        transaction,
                        "InvoiceDetailSP",
                        new SqlParameter("@mode", SqlDbType.NVarChar, 20)
                        { Value = "insert" },
                        new SqlParameter("@InvoiceID", SqlDbType.Int)
                        { Value = invoiceID },
                        new SqlParameter("@InventoryID", SqlDbType.Int)
                        { Value = item.InventoryID },
                        new SqlParameter("@ProductName", SqlDbType.NVarChar, 50)
                        { Value = item.ProductName },
                        new SqlParameter("@ProductSKU", SqlDbType.NChar, 10)
                        { Value = item.ProductSKU },
                        new SqlParameter("@Quantity", SqlDbType.Int)
                        { Value = item.Quantity },
                        new SqlParameter("@UnitPrice", SqlDbType.Decimal)
                        { Precision = 18, Scale = 2, Value = item.UnitPrice },
                        new SqlParameter("@DiscountAmount", SqlDbType.Decimal)
                        { Value = item.DiscountAmount },
                        new SqlParameter("@TaxAmount", SqlDbType.Decimal)
                        { Precision = 18, Scale = 2, Value = item.TaxAmount },
                        new SqlParameter("@TotalAmount", SqlDbType.Decimal)
                        { Precision = 18, Scale = 2, Value = item.TotalAmount });
                }
                await transaction.CommitAsync();
                return invoiceID;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdateInvoiceAsync(
    int invoiceID,
    SaveInvoiceRequest request)
        {
            if (invoiceID <= 0)
                throw new ArgumentException("Invalid invoice ID.");

            if (request.Items == null || request.Items.Count == 0)
                throw new ArgumentException(
                    "At least one invoice item is required.");

            if (request.DiscountAmount < 0 ||
                request.TaxAmount < 0 ||
                request.PaidAmount < 0)
            {
                throw new ArgumentException(
                    "Discount, tax and paid amount cannot be negative.");
            }

            await using SqlConnection connection =
                new SqlConnection(ConnectionString);

            await connection.OpenAsync();

            await using SqlTransaction transaction =
                (SqlTransaction)await connection.BeginTransactionAsync();

            try
            {
                // 1. Check whether the invoice exists.
                await using (SqlCommand checkCommand = new SqlCommand(
                    @"SELECT COUNT(1)
              FROM InvoiceMaster
              WHERE InvoiceID = @InvoiceID AND IsActive = 1",
                    connection, transaction))
                {
                    checkCommand.Parameters.Add(
                        "@InvoiceID", SqlDbType.Int).Value = invoiceID;

                    int exists = Convert.ToInt32(
                        await checkCommand.ExecuteScalarAsync());

                    if (exists == 0)
                        throw new ArgumentException("Invoice not found.");
                }

                // 2. Read product details and calculate totals.
                decimal subTotal = 0;
                decimal itemDiscountTotal = 0;
                decimal itemTaxTotal = 0;

                var items = new List<(
                    int InventoryID,
                    string ProductName,
                    string ProductSKU,
                    int Quantity,
                    decimal UnitPrice,
                    decimal DiscountAmount,
                    decimal TaxAmount,
                    decimal TotalAmount)>();

                foreach (var item in request.Items)
                {
                    if (item.InventoryID <= 0 || item.Quantity <= 0)
                        throw new ArgumentException(
                            "Each item needs a valid product and quantity.");

                    if (item.DiscountAmount < 0 || item.TaxAmount < 0)
                        throw new ArgumentException(
                            "Item discount and tax cannot be negative.");

                    await using SqlCommand productCommand = new SqlCommand(
                        @"SELECT ProductName, ProductSKU, ProductSellPrice
                  FROM InventoryMaster
                  WHERE InventoryID = @InventoryID
                    AND Isactive = 1",
                        connection, transaction);

                    productCommand.Parameters.Add(
                        "@InventoryID", SqlDbType.Int).Value = item.InventoryID;

                    string productName;
                    string productSKU;
                    decimal unitPrice;

                    await using (SqlDataReader reader =
                        await productCommand.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync())
                            throw new ArgumentException(
                                $"Active product {item.InventoryID} was not found.");

                        productName = reader["ProductName"].ToString()!;
                        productSKU = reader["ProductSKU"].ToString()!.Trim();
                        unitPrice = Convert.ToDecimal(
                            reader["ProductSellPrice"]);
                    }

                    decimal gross = unitPrice * item.Quantity;

                    if (item.DiscountAmount > gross)
                        throw new ArgumentException(
                            $"Discount exceeds the amount for {productName}.");

                    decimal lineTotal =
                        gross - item.DiscountAmount + item.TaxAmount;

                    subTotal += gross;
                    itemDiscountTotal += item.DiscountAmount;
                    itemTaxTotal += item.TaxAmount;

                    items.Add((
                        item.InventoryID,
                        productName,
                        productSKU,
                        item.Quantity,
                        unitPrice,
                        item.DiscountAmount,
                        item.TaxAmount,
                        lineTotal));
                }

                decimal totalDiscount =
                    itemDiscountTotal + request.DiscountAmount;

                decimal totalTax = itemTaxTotal + request.TaxAmount;

                if (totalDiscount > subTotal)
                    throw new ArgumentException(
                        "Total discount cannot exceed the subtotal.");

                decimal grandTotal = subTotal - totalDiscount + totalTax;

                if (request.PaidAmount > grandTotal)
                    throw new ArgumentException(
                        "Paid amount cannot exceed the invoice total.");

                decimal unpaidAmount = grandTotal - request.PaidAmount;

                string paymentStatus = unpaidAmount == 0
                    ? "Paid"
                    : request.PaidAmount > 0
                        ? "Partial"
                        : "Unpaid";

                // 3. Update the invoice header.
                await ExecuteNonQueryAsync(
                    connection,
                    transaction,
                    "InvoiceMasterSP",
                    new SqlParameter("@mode", SqlDbType.NVarChar, 20)
                    { Value = "update" },
                    new SqlParameter("@InvoiceID", SqlDbType.Int)
                    { Value = invoiceID },
                    new SqlParameter("@InvoiceNumber", SqlDbType.NVarChar, 50)
                    { Value = request.InvoiceNumber },
                    new SqlParameter("@CustomerID", SqlDbType.Int)
                    { Value = request.CustomerID },
                    new SqlParameter("@InvoiceDate", SqlDbType.NVarChar, 50)
                    { Value = request.InvoiceDate.ToString("yyyy-MM-dd") },
                    new SqlParameter("@SubTotal", SqlDbType.Decimal)
                    { Precision = 18, Scale = 2, Value = subTotal },
                    new SqlParameter("@DiscountAmount", SqlDbType.Decimal)
                    { Precision = 18, Scale = 2, Value = totalDiscount },
                    new SqlParameter("@TaxAmount", SqlDbType.Decimal)
                    { Precision = 18, Scale = 2, Value = totalTax },
                    new SqlParameter("@GrandTotal", SqlDbType.Decimal)
                    { Precision = 18, Scale = 2, Value = grandTotal },
                    new SqlParameter("@PaidAmount", SqlDbType.Decimal)
                    { Precision = 18, Scale = 2, Value = request.PaidAmount },
                    new SqlParameter("@UnpaidAmount", SqlDbType.Decimal)
                    { Precision = 18, Scale = 2, Value = unpaidAmount },
                    new SqlParameter("@PaymentStatus", SqlDbType.NChar, 20)
                    { Value = paymentStatus },
                    new SqlParameter("@PaymentMethod", SqlDbType.NChar, 20)
                    { Value = (object?)request.PaymentMethod ?? DBNull.Value },
                    new SqlParameter("@Notes", SqlDbType.NVarChar, 50)
                    { Value = (object?)request.Notes ?? DBNull.Value },
                    new SqlParameter("@IsActive", SqlDbType.Bit)
                    { Value = true });

                // 4. Remove the old detail rows within this transaction.
                await ExecuteNonQueryAsync(
                    connection,
                    transaction,
                    "InvoiceDetailSP",
                    new SqlParameter("@mode", SqlDbType.NVarChar, 20)
                    { Value = "oldinvoicedelete" },
                    new SqlParameter("@InvoiceID", SqlDbType.Int)
                    { Value = invoiceID });

                // 5. Insert the updated detail rows.
                foreach (var item in items)
                {
                    await ExecuteNonQueryAsync(
                        connection,
                        transaction,
                        "InvoiceDetailSP",
                        new SqlParameter("@mode", SqlDbType.NVarChar, 20)
                        { Value = "insert" },
                        new SqlParameter("@InvoiceID", SqlDbType.Int)
                        { Value = invoiceID },
                        new SqlParameter("@InventoryID", SqlDbType.Int)
                        { Value = item.InventoryID },
                        new SqlParameter("@ProductName", SqlDbType.NVarChar, 50)
                        { Value = item.ProductName },
                        new SqlParameter("@ProductSKU", SqlDbType.NChar, 10)
                        { Value = item.ProductSKU },
                        new SqlParameter("@Quantity", SqlDbType.Int)
                        { Value = item.Quantity },
                        new SqlParameter("@UnitPrice", SqlDbType.Decimal)
                        { Precision = 18, Scale = 2, Value = item.UnitPrice },
                        new SqlParameter("@DiscountAmount", SqlDbType.Decimal)
                        { Precision = 18, Scale = 2, Value = item.DiscountAmount },
                        new SqlParameter("@TaxAmount", SqlDbType.Decimal)
                        { Precision = 18, Scale = 2, Value = item.TaxAmount },
                        new SqlParameter("@TotalAmount", SqlDbType.Decimal)
                        { Precision = 18, Scale = 2, Value = item.TotalAmount });
                }

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteInvoiceAsync(int invoiceID)
        {
            if (invoiceID <= 0)
                throw new ArgumentException("Invalid invoice ID.");

            await using SqlConnection connection =
                new SqlConnection(ConnectionString);

            await connection.OpenAsync();

            await using SqlTransaction transaction =
                (SqlTransaction)await connection.BeginTransactionAsync();

            try
            {
                // 1. Check whether the invoice exists.
                await using SqlCommand checkCommand = new SqlCommand(
                    @"SELECT COUNT(1)
              FROM InvoiceMaster
              WHERE InvoiceID = @InvoiceID",
                    connection,
                    transaction);

                checkCommand.Parameters.Add(
                    "@InvoiceID", SqlDbType.Int).Value = invoiceID;

                int exists = Convert.ToInt32(
                    await checkCommand.ExecuteScalarAsync());

                if (exists == 0)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                // 2. Delete the invoice and its detail rows.
                SqlParameter invoiceIdParameter =
                    new SqlParameter("@InvoiceID", SqlDbType.Int)
                    {
                        Value = invoiceID,
                        Direction = ParameterDirection.InputOutput
                    };

                await ExecuteNonQueryAsync(
                    connection,
                    transaction,
                    "InvoiceMasterSP",
                    new SqlParameter("@mode", SqlDbType.NVarChar, 20)
                    {
                        Value = "delete"
                    },
                    invoiceIdParameter);

                // 3. Commit the deletion.
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
