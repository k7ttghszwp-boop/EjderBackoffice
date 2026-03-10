using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ejder.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Ejder.Application.Tours.Commands;

public class DeleteTourCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteTourCommand(Guid id)
    {
        Id = id;
    }
}

public class DeleteTourCommandHandler : IRequestHandler<DeleteTourCommand, bool>
{
    private readonly ITourRepository _repository;
    private readonly IWebHostEnvironment _env;

    public DeleteTourCommandHandler(ITourRepository repository, IWebHostEnvironment env)
    {
        _repository = repository;
        _env = env;
    }

    public async Task<bool> Handle(DeleteTourCommand request, CancellationToken cancellationToken)
    {
        var tour = await _repository.GetByIdAsync(request.Id);
        if (tour == null) return false;

        // Veritabanından sil (DeleteAsync veritabanı kaydını silmeli)
        await _repository.DeleteAsync(request.Id);

        // Fiziksel resmi de sil
        if (!string.IsNullOrEmpty(tour.ImageUrl))
        {
            var filePath = Path.Combine(_env.WebRootPath, tour.ImageUrl.TrimStart('/').Replace('/', '\\'));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        return true;
    }
}
