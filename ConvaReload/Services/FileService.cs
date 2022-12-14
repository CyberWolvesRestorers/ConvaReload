using ConvaReload.DataAccess;
using ConvaReload.Domain.Entities;
using ConvaReload.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ConvaReload.Services;

public class FileService : IFileService
{
    private readonly ApplicationContext _context;

    public FileService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<int> PostFileAsync(IFormFile fileData, string extension)
    {
        try
        {
            var fileDetails = new FileDetails()
            {
                Id = 0,
                FileName = fileData.FileName,
                Extension = extension,
                Size = fileData.Length,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                UpdatedAt = DateTime.Now.ToUniversalTime()
            };

            using (var stream = new MemoryStream())
            {
                await fileData.CopyToAsync(stream);
                fileDetails.FileData = stream.ToArray();
            }

            _context.FileDetails.Add(fileDetails);
            await _context.SaveChangesAsync();
            return fileDetails.Id;
        }
        catch (Exception)
        {
            throw;
        }
    }
    
    public async Task<IEnumerable<int>> PostMultiFileAsync(List<FileUploadModel> fileData)
    {
        try
        {
            List<int> ids = new List<int>();
            foreach (FileUploadModel file in fileData)
            {
                var fileDetails = new FileDetails()
                {
                    Id = 0,
                    FileName = file.FileDetails.FileName,
                    Extension = file.Extension,
                    Size = file.FileDetails.Length,
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    UpdatedAt = DateTime.Now.ToUniversalTime()
                };

                using (var stream = new MemoryStream())
                {
                    await file.FileDetails.CopyToAsync(stream);
                    fileDetails.FileData = stream.ToArray();
                }

                _context.FileDetails.Add(fileDetails);
                ids.Add(fileDetails.Id);
            }
            await _context.SaveChangesAsync();
            return ids;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task DownloadFileById(int id)
    {
        try
        {
            var file = _context.FileDetails.Where(x => x.Id == id).FirstOrDefaultAsync();

            using (var content = new MemoryStream(file.Result.FileData))
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Downloads", file.Result.FileName);
                using (var destination = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
                {
                    await content.CopyToAsync(destination);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}