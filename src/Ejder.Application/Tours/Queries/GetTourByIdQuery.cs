using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ejder.Application.Tours.DTOs;
using Ejder.Domain.Interfaces;
using MediatR;

namespace Ejder.Application.Tours.Queries;

public class GetTourByIdQuery : IRequest<TourDto?>
{
    public Guid Id { get; set; }

    public GetTourByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetTourByIdQueryHandler : IRequestHandler<GetTourByIdQuery, TourDto?>
{
    private readonly ITourRepository _repository;
    private readonly IMapper _mapper;

    public GetTourByIdQueryHandler(ITourRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TourDto?> Handle(GetTourByIdQuery request, CancellationToken cancellationToken)
    {
        var tour = await _repository.GetByIdAsync(request.Id);
        if (tour == null) return null;

        return _mapper.Map<TourDto>(tour);
    }
}
