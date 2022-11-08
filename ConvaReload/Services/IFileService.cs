using ConvaReload.Domain.Models;

namespace ConvaReload.Services;

public interface IFileService
{
    public Task PostFileAsync(IFormFile fileData, string extension);
    public Task PostMultiFileAsync(List<FileUploadModel> fileData);
    public Task DownloadFileById(int id);
}