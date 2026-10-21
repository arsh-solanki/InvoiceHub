namespace RepositoryPatternDemo.Models
{
    public class InvoiceItemRequest
    {
        public int InventoryID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
    }
}
