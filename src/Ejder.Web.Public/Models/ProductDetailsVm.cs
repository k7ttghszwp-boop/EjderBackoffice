using Ejder.Domain.Products;
using Ejder.Domain.Tours;

namespace Ejder.Web.Public.Models;

public class ProductDetailsVm
{
    public Product Product { get; set; } = default!;
    public List<TourProgramDay> Program { get; set; } = new();
    public TourDocument? Document { get; set; }
}
