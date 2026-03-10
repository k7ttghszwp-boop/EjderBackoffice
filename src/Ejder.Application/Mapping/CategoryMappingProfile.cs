using AutoMapper;
using Ejder.Application.Categories.DTOs;
using Ejder.Domain.Entities;

namespace Ejder.Application.Mapping;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
    }
}
