using System;

namespace Ejder.Application.Tours.DTOs;

public class TourListDto
{
    public Guid Id { get; set; }
    public string Name_TR { get; set; } = string.Empty;
    public string Name_EN { get; set; } = string.Empty;
    public string ShortDescription_TR { get; set; } = string.Empty;
    public string ShortDescription_EN { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? DiscountedPrice { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    
    public Guid CategoryId { get; set; }
    public string CategoryName_TR { get; set; } = string.Empty;
    public string CategoryName_EN { get; set; } = string.Empty;
}
