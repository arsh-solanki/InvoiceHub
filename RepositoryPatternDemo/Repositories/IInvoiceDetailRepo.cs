using RepositoryPatternDemo.Models;

namespace RepositoryPatternDemo.Repositories
{
    public interface IInvoiceDetailRepo
    {
        Task<List<InvoiceDetail>> GetAllInvoices();
        //Task<int> InsertInvoiceDetail(InvoiceDetail detail);
    }
}
