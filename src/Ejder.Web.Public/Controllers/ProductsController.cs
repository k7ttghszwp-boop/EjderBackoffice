using Microsoft.AspNetCore.Mvc;

using Ejder.Application.Products.Services;
using Ejder.Application.Tours.Services;
using Ejder.Domain.Products;
using Ejder.Domain.Tours;

namespace Ejder.Web.Public.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _products;
    private readonly ITourProgramService _programs;
    private readonly ITourDocumentService _docs;

    public ProductsController(
        IProductService products,
        ITourProgramService programs,
        ITourDocumentService docs)
    {
        _products = products;
        _programs = programs;
        _docs = docs;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _products.GetAllAsync();
        return View(products);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _products.GetByIdAsync(id);
        if (product is null)
            return NotFound();

        var vm = new ProductDetailsVm
        {
            Product = product,
            Program = (await _programs.GetByProductAsync(id)).ToList(),
            Document = await _docs.GetByProductAsync(id)
        };

        return View(vm);
    }
}


public class ProductDetailsVm
{
    public required Product Product { get; set; }
    public List<TourProgramDay> Program { get; set; } = new();
    public TourDocument? Document { get; set; }
}
