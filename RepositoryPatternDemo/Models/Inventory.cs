namespace RepositoryPatternDemo.Models
{
    
    public class Inventory
    {
        public int InventoryID { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCategory { get; set; }
        public decimal ProductBuyPrice { get; set; }
        public decimal ProductSellPrice { get; set; }
        public int ProductQty { get; set; }
        public string? ProductDescription { get; set; }
        public int ProductReorderQty { get; set; }
        public DateTime ProductCreatedDate { get; set; }
        public DateTime ProductUpdatedDate { get; set; }
        public bool Isactive { get; set; }
        public string? ProductSKU { get; set; }
        public string? ImagePath { get; set; }
    }
}
