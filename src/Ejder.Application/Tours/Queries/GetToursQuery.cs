using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ejder.Application.Tours.DTOs;
using Ejder.Domain.Interfaces;
using MediatR;

namespace Ejder.Application.Tours.Queries;

public class GetToursQuery : IRequest<IEnumerable<TourListDto>>
{
}

public class GetToursQueryHandler : IRequestHandler<GetToursQuery, IEnumerable<TourListDto>>
{
    private readonly ITourRepository _repository;
    private readonly IMapper _mapper;

    public GetToursQueryHandler(ITourRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TourListDto>> Handle(GetToursQuery request, CancellationToken cancellationToken)
    {
        var tours = await _repository.GetAllActiveAsync();
        return _mapper.Map<IEnumerable<TourListDto>>(tours);
    }
}
