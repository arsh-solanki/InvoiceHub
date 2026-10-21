namespace RepositoryPatternDemo.Models
{
    public class InvoiceDetail
    {
        public int InvoiceID { get; set; }
        public int InventoryID { get; set; }
        public string? ProductName { get; set; }
        public string? ProductSKU { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
    }
}
