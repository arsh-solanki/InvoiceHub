using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories;

namespace RepositoryPatternDemo.Services
{
    public class InvoiceMasterService : IInvoiceMasterService
    {
        public readonly IInvoiceMasterRepo _invoiceMasterRepo;

        public InvoiceMasterService(IInvoiceMasterRepo invoiceMasterRepo)
        {
            _invoiceMasterRepo = invoiceMasterRepo;
        }

        public async Task<List<InvoiceMaster>> GetAllInvoices()
        {
            return await _invoiceMasterRepo.GetAllInvoices();
        }

        public async Task<int> SaveInvoiceAsync(SaveInvoiceRequest request)
        {
            return await _invoiceMasterRepo.SaveInvoiceAsync(request);
        }

        public async Task<bool> UpdateInvoiceAsync(
    int invoiceID,
    SaveInvoiceRequest request)
        {
            return await _invoiceMasterRepo.UpdateInvoiceAsync(
                invoiceID, request);
        }

        public async Task<bool> DeleteInvoiceAsync(int invoiceID)
        {
            return await _invoiceMasterRepo.DeleteInvoiceAsync(invoiceID);
        }
    }
}
