using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ejder.Application.Categories.DTOs;
using Ejder.Domain.Interfaces;
using MediatR;

namespace Ejder.Application.Categories.Queries;

public class GetCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
{
}

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAllActiveAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }
}
