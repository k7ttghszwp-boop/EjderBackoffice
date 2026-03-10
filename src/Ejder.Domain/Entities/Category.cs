using System;
using System.Collections.Generic;

namespace Ejder.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name_TR { get; set; } = string.Empty;
        public string Name_EN { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Navigation property
        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}
