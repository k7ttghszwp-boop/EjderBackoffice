using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ejder.Application.Tours.DTOs;
using Ejder.Domain.Entities;
using Ejder.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Ejder.Application.Tours.Commands;

public class CreateTourCommand : IRequest<Guid>
{
    public CreateTourDto Dto { get; set; }

    public CreateTourCommand(CreateTourDto dto)
    {
        Dto = dto;
    }
}

public class CreateTourCommandHandler : IRequestHandler<CreateTourCommand, Guid>
{
    private readonly ITourRepository _repository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    private readonly IEmailService _emailService;

    public CreateTourCommandHandler(ITourRepository repository, IMapper mapper, IWebHostEnvironment env, IEmailService emailService)
    {
        _repository = repository;
        _mapper = mapper;
        _env = env;
        _emailService = emailService;
    }

    public async Task<Guid> Handle(CreateTourCommand request, CancellationToken cancellationToken)
    {
        var tour = _mapper.Map<Tour>(request.Dto);
        tour.Id = Guid.NewGuid();
        tour.CreatedAt = DateTime.UtcNow;

        if (request.Dto.ImageFile != null && request.Dto.ImageFile.Length > 0)
        {
            var extension = Path.GetExtension(request.Dto.ImageFile.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            
            // Yol => wwwroot/uploads/tours
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "tours");
            
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await request.Dto.ImageFile.CopyToAsync(fileStream, cancellationToken);
            }

            // Kaydedilen yolu Tour.ImageUrl'e ata
            tour.ImageUrl = $"/uploads/tours/{uniqueFileName}";
        }

        await _repository.AddAsync(tour);

        // Notify Admin
        var tourUrl = $"https://backoffice.ejderturizm.com/Tour/Detail/{tour.Id}"; // Örnek URL
        await _emailService.SendNewTourNotificationAsync(tour.Name_TR, tourUrl);

        return tour.Id;
    }
}
