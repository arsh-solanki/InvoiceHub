

using RepositoryPatternDemo.Models;

namespace RepositoryPatternDemo.Repositories
{
    public interface IInvoiceMasterRepo
    {
        Task<List<InvoiceMaster>> GetAllInvoices();
        Task<int> SaveInvoiceAsync(SaveInvoiceRequest request);
        Task<bool> UpdateInvoiceAsync(int invoiceID, SaveInvoiceRequest request);

        Task<bool> DeleteInvoiceAsync(int invoiceID);
    }
}
