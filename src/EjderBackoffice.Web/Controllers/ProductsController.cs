using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Ejder.Application.Products.Services;
using Ejder.Application.Products.Dtos;

using Ejder.Application.Tours.Services;
using Ejder.Domain.Tours;

// Ürün bilgisini ViewBag.Product için şimdilik Core’dan alıyoruz.
// (PR4+’ta bunu da Application/Domain’a taşıyabiliriz.)
using Ejder.Core.Repositories;

namespace EjderBackoffice.Web.Controllers;

[Authorize]
public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ITourProgramService _tourProgramService;
    private readonly ITourDocumentService _tourDocumentService;

    public ProductsController(
        IProductService productService,
        ITourProgramService tourProgramService,
        ITourDocumentService tourDocumentService)
    {
        _productService = productService;
        _tourProgramService = tourProgramService;
        _tourDocumentService = tourDocumentService;
    }

    // =====================================================
    // ✅ PRODUCTS (LIST / CREATE)  -> Application Layer
    // =====================================================

    public IActionResult Index()
    {
        var products = _productService.GetAll();
        return View(products);
    }

    public IActionResult Create()
    {
        return View(new ProductCreateDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProductCreateDto model)
    {
        if (!ModelState.IsValid)
            return View(model);

        _productService.Create(model);

        TempData["Success"] = "Tur başarıyla eklendi ✅";
        return RedirectToAction(nameof(Index));
    }

    // =====================================================
    // ✅ TUR PROGRAMI (GÜN GÜN) -> Application Layer
    // =====================================================

    public IActionResult Program(int id)
    {
        // Şimdilik sadece başlık göstermek için Core ProductRepository
        var product = ProductRepository.GetAll().FirstOrDefault(x => x.Id == id);
        if (product == null) return NotFound();

        ViewBag.Product = product;

        var days = _tourProgramService.GetByProduct(id);
        return View(days);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddDay(TourProgramDay model)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Program), new { id = model.ProductId });

        _tourProgramService.AddDay(model);

        TempData["Success"] = "Program günü eklendi ✅";
        return RedirectToAction(nameof(Program), new { id = model.ProductId });
    }

    // =====================================================
    // ✅ TUR DOKÜMANI (PDF) -> Application Layer
    // =====================================================

    public IActionResult Documents(int id)
    {
        ViewBag.ProductId = id;
        var doc = _tourDocumentService.GetByProduct(id);
        return View(doc);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UploadPdf(int productId, IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            TempData["Error"] = "Lütfen bir PDF dosyası seçin.";
            return RedirectToAction(nameof(Documents), new { id = productId });
        }

        var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs");
        Directory.CreateDirectory(folder);

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var fullPath = Path.Combine(folder, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        _tourDocumentService.Save(new TourDocument
        {
            ProductId = productId,
            FileName = file.FileName,
            FilePath = "/docs/" + fileName
        });

        TempData["Success"] = "PDF başarıyla yüklendi ✅";
        return RedirectToAction(nameof(Documents), new { id = productId });
    }
}
