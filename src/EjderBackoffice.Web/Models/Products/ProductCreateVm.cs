using System.ComponentModel.DataAnnotations;

namespace EjderBackoffice.Web.Models.Products;

public class ProductCreateVm
{
    [Required, StringLength(120)]
    public string Name { get; set; } = null!;

    [Range(1, 60)]
    public int Days { get; set; }

    [Range(0, 1_000_000)]
    public decimal Price { get; set; }
}
