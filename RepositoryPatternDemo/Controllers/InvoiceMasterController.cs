using Microsoft.AspNetCore.Mvc;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Services;
namespace RepositoryPatternDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceMasterController : Controller
    {
        private readonly IInvoiceMasterService _invoiceMasterService;

        public InvoiceMasterController(IInvoiceMasterService invoiceMasterService)
        {
            _invoiceMasterService = invoiceMasterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            List<InvoiceMaster> invoices = await _invoiceMasterService.GetAllInvoices();
            return Ok(invoices);
        }

        public async Task<IActionResult> CreateInvoice(
            [FromBody] SaveInvoiceRequest request)
        {
            try
            {
                int invoiceID = await _invoiceMasterService.SaveInvoiceAsync(request);
                return Ok(new
                {
                    message = "invoice create successfully",
                    InvoiceID = invoiceID
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message= ex.Message});
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateInvoice(
    int id,
    [FromBody] SaveInvoiceRequest request)
        {
            try
            {
                bool updated = await _invoiceMasterService.UpdateInvoiceAsync(
                    id, request);

                if (!updated)
                    return NotFound(new { Message = "Invoice not found." });

                return Ok(new
                {
                    Message = "Invoice updated successfully.",
                    InvoiceID = id
                });
            }
            catch (ArgumentException ex)
            {
                if (ex.Message == "Invoice not found.")
                    return NotFound(new { Message = ex.Message });

                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            try
            {
                bool deleted =
                    await _invoiceMasterService.DeleteInvoiceAsync(id);

                if (!deleted)
                {
                    return NotFound(new
                    {
                        Message = "Invoice not found."
                    });
                }

                return Ok(new
                {
                    Message = "Invoice and its details deleted successfully.",
                    InvoiceID = id
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
