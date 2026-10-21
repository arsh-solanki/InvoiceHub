namespace RepositoryPatternDemo.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public int AddressID { get; set; }
        public bool IsActive { get; set; }
        public int CountryId { get; set; }
    }
}
