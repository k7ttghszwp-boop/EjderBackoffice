using System;

namespace Ejder.Domain.Entities
{
    public class Tour
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
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // FK and Navigation property
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
