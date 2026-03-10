using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using Ejder.Application.Tours.DTOs;
using Ejder.Application.Categories.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EjderBackoffice.Web.Controllers;

[Authorize]
public class TourController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly string _apiUrl;

    public TourController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _apiUrl = _configuration["ApiSettings:BaseUrl"];
    }

    private string? GetJwtToken()
    {
        return Request.Cookies["jwt-token"];
    }

    private HttpClient CreateClient()
    {
        var client = _httpClientFactory.CreateClient();
        var token = GetJwtToken();
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return client;
    }

    public async Task<IActionResult> Index(Guid? categoryId = null)
    {
        var client = CreateClient();
        
        // Kategori listesini çek (Filtre için)
        var catResponse = await client.GetAsync($"{_apiUrl}categories");
        if (catResponse.IsSuccessStatusCode)
        {
            var catContent = await catResponse.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<List<CategoryDto>>(catContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            ViewBag.Categories = new SelectList(categories, "Id", "Name_TR", categoryId);
        }

        // Tur listesini çek
        var url = $"{_apiUrl}tours";
        if (categoryId.HasValue) url = $"{_apiUrl}tours/category/{categoryId}";

        var response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var tours = JsonSerializer.Deserialize<List<TourListDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(tours);
        }

        return View(new List<TourListDto>());
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var client = CreateClient();
        var catResponse = await client.GetAsync($"{_apiUrl}categories");
        if (catResponse.IsSuccessStatusCode)
        {
            var catContent = await catResponse.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<List<CategoryDto>>(catContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            ViewBag.Categories = new SelectList(categories, "Id", "Name_TR");
        }
        return View(new CreateTourDto { StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7) });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTourDto dto)
    {
        var client = CreateClient();
        using var content = new MultipartFormDataContent();

        // Basit alanları ekle
        content.Add(new StringContent(dto.Name_TR), nameof(dto.Name_TR));
        content.Add(new StringContent(dto.Name_EN), nameof(dto.Name_EN));
        content.Add(new StringContent(dto.Description_TR), nameof(dto.Description_TR));
        content.Add(new StringContent(dto.Description_EN), nameof(dto.Description_EN));
        content.Add(new StringContent(dto.ShortDescription_TR), nameof(dto.ShortDescription_TR));
        content.Add(new StringContent(dto.ShortDescription_EN), nameof(dto.ShortDescription_EN));
        content.Add(new StringContent(dto.Price.ToString()), nameof(dto.Price));
        if (dto.DiscountedPrice.HasValue)
            content.Add(new StringContent(dto.DiscountedPrice.Value.ToString()), nameof(dto.DiscountedPrice));
        content.Add(new StringContent(dto.StartDate.ToString("o")), nameof(dto.StartDate));
        content.Add(new StringContent(dto.EndDate.ToString("o")), nameof(dto.EndDate));
        content.Add(new StringContent(dto.MaxParticipants.ToString()), nameof(dto.MaxParticipants));
        content.Add(new StringContent(dto.IsActive.ToString()), nameof(dto.IsActive));
        content.Add(new StringContent(dto.CategoryId.ToString()), nameof(dto.CategoryId));

        // Dosyayı ekle
        if (dto.ImageFile != null)
        {
            var fileContent = new StreamContent(dto.ImageFile.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
            content.Add(fileContent, "ImageFile", dto.ImageFile.FileName);
        }

        var response = await client.PostAsync($"{_apiUrl}tours", content);
        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Tur başarıyla oluşturuldu.";
            return RedirectToAction(nameof(Index));
        }

        TempData["Error"] = "Tur oluşturulurken hata!";
        return View(dto);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var client = CreateClient();
        
        // Kategoriler
        var catResponse = await client.GetAsync($"{_apiUrl}categories");
        var catContent = await catResponse.Content.ReadAsStringAsync();
        var categories = JsonSerializer.Deserialize<List<CategoryDto>>(catContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        // Tur detayı
        var response = await client.GetAsync($"{_apiUrl}tours/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var tour = JsonSerializer.Deserialize<UpdateTourDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            ViewBag.Categories = new SelectList(categories, "Id", "Name_TR", tour.CategoryId);
            return View(tour);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateTourDto dto)
    {
        var client = CreateClient();
        using var content = new MultipartFormDataContent();

        content.Add(new StringContent(dto.Id.ToString()), nameof(dto.Id));
        content.Add(new StringContent(dto.Name_TR), nameof(dto.Name_TR));
        content.Add(new StringContent(dto.Name_EN), nameof(dto.Name_EN));
        content.Add(new StringContent(dto.Description_TR), nameof(dto.Description_TR));
        content.Add(new StringContent(dto.Description_EN), nameof(dto.Description_EN));
        content.Add(new StringContent(dto.ShortDescription_TR), nameof(dto.ShortDescription_TR));
        content.Add(new StringContent(dto.ShortDescription_EN), nameof(dto.ShortDescription_EN));
        content.Add(new StringContent(dto.Price.ToString()), nameof(dto.Price));
        if (dto.DiscountedPrice.HasValue)
            content.Add(new StringContent(dto.DiscountedPrice.Value.ToString()), nameof(dto.DiscountedPrice));
        content.Add(new StringContent(dto.StartDate.ToString("o")), nameof(dto.StartDate));
        content.Add(new StringContent(dto.EndDate.ToString("o")), nameof(dto.EndDate));
        content.Add(new StringContent(dto.MaxParticipants.ToString()), nameof(dto.MaxParticipants));
        content.Add(new StringContent(dto.IsActive.ToString()), nameof(dto.IsActive));
        content.Add(new StringContent(dto.CategoryId.ToString()), nameof(dto.CategoryId));

        if (dto.ImageFile != null)
        {
            var fileContent = new StreamContent(dto.ImageFile.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
            content.Add(fileContent, "ImageFile", dto.ImageFile.FileName);
        }

        var response = await client.PutAsync($"{_apiUrl}tours/{dto.Id}", content);
        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Tur başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var client = CreateClient();
        var response = await client.DeleteAsync($"{_apiUrl}tours/{id}");

        if (response.IsSuccessStatusCode)
        {
            return Json(new { success = true });
        }

        return Json(new { success = false, message = "Tur silinemedi." });
    }
}
