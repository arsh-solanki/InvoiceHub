using RepositoryPatternDemo.Models;

namespace RepositoryPatternDemo.Services
{
    public interface IInvoiceDetailService
    {
        Task<List<InvoiceDetail>> GetAllInvoices();
        //Task<int> InsertInvoiceDetail(InvoiceDetail detail);
    }
}
