using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ejder.Application.Tours.DTOs;
using Ejder.Domain.Interfaces;
using MediatR;

namespace Ejder.Application.Tours.Queries;

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
}

public class GetToursPagedQuery : IRequest<PagedResult<TourListDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public Guid? CategoryId { get; set; }

    public GetToursPagedQuery(int page, int pageSize, Guid? categoryId = null)
    {
        Page = page;
        PageSize = pageSize;
        CategoryId = categoryId;
    }
}

public class GetToursPagedQueryHandler : IRequestHandler<GetToursPagedQuery, PagedResult<TourListDto>>
{
    private readonly ITourRepository _repository;
    private readonly IMapper _mapper;

    public GetToursPagedQueryHandler(ITourRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResult<TourListDto>> Handle(GetToursPagedQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetPagedAsync(request.Page, request.PageSize, request.CategoryId);
        
        return new PagedResult<TourListDto>
        {
            Items = _mapper.Map<IEnumerable<TourListDto>>(result.Items),
            TotalCount = result.TotalCount
        };
    }
}
