using Microsoft.AspNetCore.Http;
using WorkTrace.Application.Services;

namespace WorkTrace.Logic.Services;

public class FileService : IFileService
{
    public async Task<string> SaveFileAsync(IFormFile file, string folder)
    {
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var directory = Path.Combine("wwwroot", folder);

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        var path = Path.Combine(directory, fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return $"/{folder}/{fileName}";
    }
}
