using WorkTrace.Data.Models;

namespace WorkTrace.Application.Services;

public interface IGeocodingService
{
    Task<GeoPoint?> GetGeoPointAsync(string addressOrUrl);
}
