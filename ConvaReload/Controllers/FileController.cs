using Microsoft.AspNetCore.Mvc;
using ConvaReload.Domain.Models;
using ConvaReload.Services;

namespace ConvaReload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _uploadService;

        public FileController(IFileService uploadService)
        {
            _uploadService = uploadService;
        }

        // POST: api/File/PostSingleFile
        [HttpPost("PostSingleFile")]
        public async Task<ActionResult> PostSingleFile([FromForm] FileUploadModel fileDetails)
        {
            if (fileDetails == null)
            {
                return BadRequest();
            }

            try
            {
                await _uploadService.PostFileAsync(fileDetails.FileDetails, fileDetails.Extension);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        // POST: api/File/PostMultipleFile
        [HttpPost("PostMultipleFile")]
        public async Task<ActionResult> PostMultipleFile([FromForm] List<FileUploadModel> fileDetails)
        {
            if (fileDetails == null)
            {
                return BadRequest();
            }

            try
            {
                await _uploadService.PostMultiFileAsync(fileDetails);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        // GET: api/File/DownloadFile
        [HttpGet("DownloadFile")]
        public async Task<ActionResult> DownloadFile(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            try
            {
                await _uploadService.DownloadFileById(id);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
