using RepositoryPatternDemo.Models;

namespace RepositoryPatternDemo.Services
{
    public interface IInvoiceMasterService
    {
        Task<List<InvoiceMaster>> GetAllInvoices();
        Task<int> SaveInvoiceAsync(SaveInvoiceRequest request);

        Task<bool> UpdateInvoiceAsync(int invoiceID, SaveInvoiceRequest request);

        Task<bool> DeleteInvoiceAsync(int invoiceID);
    }
}
