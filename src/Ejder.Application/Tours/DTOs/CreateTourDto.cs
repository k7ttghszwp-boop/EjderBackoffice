using System;
using Microsoft.AspNetCore.Http;

namespace Ejder.Application.Tours.DTOs;

public class CreateTourDto
{
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
    public bool IsActive { get; set; } = true;
    public Guid CategoryId { get; set; }
    
    public IFormFile? ImageFile { get; set; }
}
