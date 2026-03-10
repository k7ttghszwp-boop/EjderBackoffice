using System;
using Microsoft.AspNetCore.Http;

namespace Ejder.Application.Tours.DTOs;

public class UpdateTourDto
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
    public int MaxParticipants { get; set; }
    public bool IsActive { get; set; }
    public Guid CategoryId { get; set; }
    
    // Existing image tracking (for UI display or keeping existing file if not updated)
    public string? ExistingImageUrl { get; set; }
    
    // For uploading a new image to replace the old one
    public IFormFile? ImageFile { get; set; }
}
