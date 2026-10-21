namespace RepositoryPatternDemo.Models
{
    public class SaveInvoiceRequest
    {
        public string InvoiceNumber { get; set; }
        public int CustomerID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Notes { get; set; }
        public List<InvoiceItemRequest> Items { get; set; } = new();
    }
}
