using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ejder.Application.Tours.DTOs;
using Ejder.Domain.Interfaces;
using MediatR;

namespace Ejder.Application.Tours.Queries;

public class GetToursByCategoryQuery : IRequest<IEnumerable<TourListDto>>
{
    public Guid CategoryId { get; set; }

    public GetToursByCategoryQuery(Guid categoryId)
    {
        CategoryId = categoryId;
    }
}

public class GetToursByCategoryQueryHandler : IRequestHandler<GetToursByCategoryQuery, IEnumerable<TourListDto>>
{
    private readonly ITourRepository _repository;
    private readonly IMapper _mapper;

    public GetToursByCategoryQueryHandler(ITourRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TourListDto>> Handle(GetToursByCategoryQuery request, CancellationToken cancellationToken)
    {
        var tours = await _repository.GetAllActiveByCategoryAsync(request.CategoryId);
        return _mapper.Map<IEnumerable<TourListDto>>(tours);
    }
}
