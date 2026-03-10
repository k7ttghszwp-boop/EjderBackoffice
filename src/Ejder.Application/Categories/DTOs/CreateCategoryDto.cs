namespace Ejder.Application.Categories.DTOs;

public class CreateCategoryDto
{
    public string Name_TR { get; set; } = string.Empty;
    public string Name_EN { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
