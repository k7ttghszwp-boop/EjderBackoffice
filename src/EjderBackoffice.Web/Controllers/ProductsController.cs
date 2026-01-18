using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ejder.Core.Repositories;
using Ejder.Core.Models;

namespace EjderBackoffice.Web.Controllers;

[Authorize]
public class ProductsController : Controller
{
    // 📌 Tur listesi
    public IActionResult Index()
    {
        var products = ProductRepository.GetAll();
        return View(products);
    }

    // 📌 Yeni tur (GET)
    public IActionResult Create()
    {
        return View(new Product());
    }

    // 📌 Yeni tur (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product model)
    {
        if (!ModelState.IsValid)
            return View(model);

        ProductRepository.Add(model);

        TempData["Success"] = "Tur başarıyla eklendi ✅";
        return RedirectToAction("Index");
    }

    // =====================================================
    // 🔽 TUR PROGRAMI (GÜN GÜN)
    // =====================================================

    // 📌 Program yönetimi
    public IActionResult Program(int id)
    {
        var product = ProductRepository.GetAll()
            .FirstOrDefault(x => x.Id == id);

        if (product == null)
            return NotFound();

        ViewBag.Product = product;
        var days = TourProgramRepository.GetByProduct(id);

        return View(days);
    }

    // 📌 Gün ekle
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddDay(TourProgramDay model)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("Program", new { id = model.ProductId });

        TourProgramRepository.Add(model);

        TempData["Success"] = "Program günü eklendi ✅";
        return RedirectToAction("Program", new { id = model.ProductId });
    }

    // =====================================================
    // 🔽 TUR DOKÜMANI (PDF)
    // =====================================================

    // 📌 PDF yönetimi
    public IActionResult Documents(int id)
    {
        ViewBag.ProductId = id;
        var doc = TourDocumentRepository.GetByProduct(id);
        return View(doc);
    }

    // 📌 PDF yükleme
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UploadPdf(int productId, IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            TempData["Error"] = "Lütfen bir PDF dosyası seçin.";
            return RedirectToAction("Documents", new { id = productId });
        }

        var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs");
        Directory.CreateDirectory(folder);

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var fullPath = Path.Combine(folder, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        TourDocumentRepository.Save(new TourDocument
        {
            ProductId = productId,
            FileName = file.FileName,
            FilePath = "/docs/" + fileName
        });

        TempData["Success"] = "PDF başarıyla yüklendi ✅";
        return RedirectToAction("Documents", new { id = productId });
    }
}
