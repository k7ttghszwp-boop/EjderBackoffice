using AutoMapper;
using Ejder.Application.Tours.DTOs;
using Ejder.Domain.Entities;

namespace Ejder.Application.Mapping;

public class TourMappingProfile : Profile
{
    public TourMappingProfile()
    {
        CreateMap<Tour, TourDto>()
            .ForMember(dest => dest.CategoryName_TR, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name_TR : string.Empty))
            .ForMember(dest => dest.CategoryName_EN, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name_EN : string.Empty));
            
        CreateMap<Tour, TourListDto>()
            .ForMember(dest => dest.CategoryName_TR, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name_TR : string.Empty))
            .ForMember(dest => dest.CategoryName_EN, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name_EN : string.Empty));

        CreateMap<CreateTourDto, Tour>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()); // ImageUrl Handler'da atanacak

        CreateMap<UpdateTourDto, Tour>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()); // ImageUrl Handler'da yönetilecek
    }
}
