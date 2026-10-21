using Microsoft.AspNetCore.Mvc;
using RepositoryPatternDemo.Services;

namespace RepositoryPatternDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : Controller
    {
        private readonly IFileUploadService _fileUploadService;

        public FileUploadController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        { 
            if(file == null || file.Length == 0)
            { return BadRequest("pls select"); }

            try
            {
                int fileId = await _fileUploadService.UploadFileAsync(file);

                return Ok(new
                {
                    Message = "file uploaded successfully",
                    FileID = fileId,
                    FileName = Path.GetFileName(file.FileName)
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (IOException ex)
            {
                return Conflict(new
                { Message = "already exist! could not be saved.." });
            }
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
