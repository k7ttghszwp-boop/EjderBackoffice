using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Ejder.Application.Tours.DTOs;
using Ejder.Application.Categories.DTOs;
using Ejder.Application.Tours.Queries;

namespace Ejder.Web.Public.Controllers;

public class ToursController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly string _apiUrl;

    public ToursController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _apiUrl = _configuration["ApiSettings:BaseUrl"];
    }

    [Route("{lang}/tours")]
    [Route("tours")]
    public async Task<IActionResult> Index(string lang = "tr", int page = 1, Guid? categoryId = null)
    {
        ViewBag.Lang = lang;
        var client = _httpClientFactory.CreateClient();

        // Kategorileri çek (Filtre için)
        var catResponse = await client.GetAsync($"{_apiUrl}categories");
        if (catResponse.IsSuccessStatusCode)
        {
            var catContent = await catResponse.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<List<CategoryDto>>(catContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            ViewBag.Categories = categories;
        }

        // Turları çek (Paged/Filtered)
        var tourResponse = await client.GetAsync($"{_apiUrl}tours/paged?page={page}&pageSize=6&categoryId={categoryId}");
        if (tourResponse.IsSuccessStatusCode)
        {
            var tourContent = await tourResponse.Content.ReadAsStringAsync();
            var pagedResult = JsonSerializer.Deserialize<PagedResult<TourListDto>>(tourContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            ViewBag.CurrentPage = page;
            ViewBag.CategoryId = categoryId;
            return View(pagedResult);
        }

        return View(new PagedResult<TourListDto>());
    }

    [Route("{lang}/tours/{id}")]
    [Route("tours/{id}")]
    public async Task<IActionResult> Detail(Guid id, string lang = "tr")
    {
        ViewBag.Lang = lang;
        var client = _httpClientFactory.CreateClient();
        
        var response = await client.GetAsync($"{_apiUrl}tours/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var tour = JsonSerializer.Deserialize<TourDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(tour);
        }

        return NotFound();
    }
}
