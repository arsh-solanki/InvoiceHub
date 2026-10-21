using Microsoft.AspNetCore.Mvc;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Services;

namespace RepositoryPatternDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailController : Controller
    {
            private readonly IInvoiceDetailService _invoiceDetailService;

            public InvoiceDetailController(IInvoiceDetailService invoiceDetailService)
            {
                _invoiceDetailService = invoiceDetailService;
            }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            List<InvoiceDetail> invoices = await _invoiceDetailService.GetAllInvoices();
            return Ok(invoices);
        }


        //[HttpPost]
        //public async Task<IActionResult> InsertInvoiceDetail([FromBody] InvoiceDetail detail)
        //{
        //    await _invoiceDetailService.InsertInvoiceDetail(detail);

        //    return Ok(new { message = "insert successfully" });
        //}
    }
}
        //public IActionResult Index()
        //{
        //    return View();
        //}