using Microsoft.AspNetCore.Http;

namespace WorkTrace.Application.Services;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file, string folder);
}