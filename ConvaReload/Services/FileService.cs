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

    public async Task PostFileAsync(IFormFile fileData, string extension)
    {
        try
        {
            var fileDetails = new FileDetails()
            {
                Id = 0,
                FileName = fileData.FileName,
                Extension = extension
            };

            using (var stream = new MemoryStream())
            {
                fileData.CopyTo(stream);
                fileDetails.FileData = stream.ToArray();
            }

            var result = _context.FileDetails.Add(fileDetails);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    
    public async Task PostMultiFileAsync(List<FileUploadModel> fileData)
    {
        try
        {
            foreach (FileUploadModel file in fileData)
            {
                var fileDetails = new FileDetails()
                {
                    Id = 0,
                    FileName = file.FileDetails.FileName,
                    Extension = file.Extension
                };

                using (var stream = new MemoryStream())
                {
                    file.FileDetails.CopyTo(stream);
                    fileDetails.FileData = stream.ToArray();
                }

                var result = _context.FileDetails.Add(fileDetails);
            }
            await _context.SaveChangesAsync();
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