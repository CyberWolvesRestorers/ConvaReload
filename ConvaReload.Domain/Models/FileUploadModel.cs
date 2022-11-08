using Microsoft.AspNetCore.Http;

namespace ConvaReload.Domain.Models;

public class FileUploadModel
{
    public IFormFile FileDetails { get; set; }
    public string Extension { get; set; }
}