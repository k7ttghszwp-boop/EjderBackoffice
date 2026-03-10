using System;

namespace Ejder.Application.Tours.DTOs;

public class TourDto
{
    public Guid Id { get; set; }
    public string Name_TR { get; set; } = string.Empty;
    public string Name_EN { get; set; } = string.Empty;
    public string Description_TR { get; set; } = string.Empty;
    public string Description_EN { get; set; } = string.Empty;
    public string ShortDescription_TR { get; set; } = string.Empty;
    public string ShortDescription_EN { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? DiscountedPrice { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int MaxParticipants { get; set; }
    public bool IsActive { get; set; }
    
    // Category Details
    public Guid CategoryId { get; set; }
    public string CategoryName_TR { get; set; } = string.Empty;
    public string CategoryName_EN { get; set; } = string.Empty;
}
