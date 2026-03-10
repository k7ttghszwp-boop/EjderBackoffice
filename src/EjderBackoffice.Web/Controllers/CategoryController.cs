using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Ejder.Application.Categories.DTOs;

namespace EjderBackoffice.Web.Controllers;

[Authorize]
public class CategoryController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly string _apiUrl;

    public CategoryController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _apiUrl = _configuration["ApiSettings:BaseUrl"] + "categories";
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

    public async Task<IActionResult> Index()
    {
        var client = CreateClient();
        var response = await client.GetAsync(_apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<List<CategoryDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(categories);
        }

        TempData["Error"] = "Kategoriler yüklenirken bir hata oluştu.";
        return View(new List<CategoryDto>());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateCategoryDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        var client = CreateClient();
        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(_apiUrl, content);

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Kategori başarıyla oluşturuldu.";
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("", "Kategori oluşturulurken API hatası oluştu.");
        return View(dto);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var client = CreateClient();
        var response = await client.GetAsync($"{_apiUrl}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var category = JsonSerializer.Deserialize<UpdateCategoryDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(category);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateCategoryDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        var client = CreateClient();
        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"{_apiUrl}/{dto.Id}", content);

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Kategori başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("", "Kategori güncellenirken API hatası oluştu.");
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var client = CreateClient();
        var response = await client.DeleteAsync($"{_apiUrl}/{id}");

        if (response.IsSuccessStatusCode)
        {
            return Json(new { success = true });
        }

        return Json(new { success = false, message = "Kategori silinemedi." });
    }
}
