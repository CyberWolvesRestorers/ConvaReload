using ConvaReload.Domain.Models;

namespace ConvaReload.Services;

public interface IFileService
{
    public Task<int> PostFileAsync(IFormFile fileData, string extension);
    public Task<IEnumerable<int>> PostMultiFileAsync(List<FileUploadModel> fileData);
    public Task DownloadFileById(int id);
}