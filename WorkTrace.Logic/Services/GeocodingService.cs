using Microsoft.Extensions.Configuration;
using System.Text.Json;
using WorkTrace.Application.Services;
using WorkTrace.Data.Models;

namespace WorkTrace.Logic.Services;

public class GeocodingService : IGeocodingService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GeocodingService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["GoogleMaps:ApiKey"];
    }

    public async Task<GeoPoint?> GetGeoPointAsync(string addressOrUrl)
    {
        var endpoint =
            $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(addressOrUrl)}&key={_apiKey}";

        var response = await _httpClient.GetAsync(endpoint);
        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);

        var root = doc.RootElement;

        if (!root.TryGetProperty("results", out var results) || results.GetArrayLength() == 0)
            return null;

        var location = results[0]
            .GetProperty("geometry")
            .GetProperty("location");

        return new GeoPoint
        {
            Latitude = location.GetProperty("lat").GetDouble(),
            Longitude = location.GetProperty("lng").GetDouble(),
            UpdatedAt = DateTime.UtcNow
        };
    }
}
