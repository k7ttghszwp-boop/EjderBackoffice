using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ejder.Application.Categories.DTOs;
using Ejder.Domain.Interfaces;
using MediatR;

namespace Ejder.Application.Categories.Commands;

public class UpdateCategoryCommand : IRequest<bool>
{
    public UpdateCategoryDto Dto { get; set; }

    public UpdateCategoryCommand(UpdateCategoryDto dto)
    {
        Dto = dto;
    }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await _repository.GetByIdAsync(request.Dto.Id);
        if (existingCategory == null) return false;

        _mapper.Map(request.Dto, existingCategory);
        
        await _repository.UpdateAsync(existingCategory);

        return true;
    }
}
