using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Ejder.Application.Tours.DTOs;

namespace Ejder.Web.Public.Controllers;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly string _apiUrl;

    public HomeController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _apiUrl = _configuration["ApiSettings:BaseUrl"];
    }

    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient();
        
        // Öne çıkan turları çek (API'den tüm turları alıp ilk 3'ü gösterelim şimdilik)
        var response = await client.GetAsync($"{_apiUrl}tours");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var tours = JsonSerializer.Deserialize<List<TourListDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(tours.Take(3).ToList());
        }

        return View(new List<TourListDto>());
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
