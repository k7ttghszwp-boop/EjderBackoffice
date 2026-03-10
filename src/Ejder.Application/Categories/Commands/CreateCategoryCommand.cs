using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ejder.Application.Categories.DTOs;
using Ejder.Domain.Entities;
using Ejder.Domain.Interfaces;
using MediatR;

namespace Ejder.Application.Categories.Commands;

public class CreateCategoryCommand : IRequest<Guid>
{
    public CreateCategoryDto Dto { get; set; }

    public CreateCategoryCommand(CreateCategoryDto dto)
    {
        Dto = dto;
    }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request.Dto);
        category.Id = Guid.NewGuid();
        category.CreatedAt = DateTime.UtcNow;

        await _repository.AddAsync(category);

        return category.Id;
    }
}
