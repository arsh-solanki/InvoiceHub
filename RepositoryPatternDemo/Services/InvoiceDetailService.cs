using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories;

namespace RepositoryPatternDemo.Services
{
    public class InvoiceDetailService : IInvoiceDetailService
     {
        private readonly IInvoiceDetailRepo _invoiceDetailRepo;

        public InvoiceDetailService(IInvoiceDetailRepo invoiceDetailRepo)
        {
            _invoiceDetailRepo = invoiceDetailRepo; 
        }
    
        public async Task<List<InvoiceDetail>> GetAllInvoices()
        {
            return await _invoiceDetailRepo.GetAllInvoices();
        }
        //public async Task<int> InsertInvoiceDetail(InvoiceDetail detail)
        //{
        //    return await _invoiceDetailRepo.InsertInvoiceDetail(detail);
        //}
    }
}
