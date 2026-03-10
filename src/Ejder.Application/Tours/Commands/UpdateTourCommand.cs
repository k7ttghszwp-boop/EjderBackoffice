using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ejder.Application.Tours.DTOs;
using Ejder.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Ejder.Application.Tours.Commands;

public class UpdateTourCommand : IRequest<bool>
{
    public UpdateTourDto Dto { get; set; }

    public UpdateTourCommand(UpdateTourDto dto)
    {
        Dto = dto;
    }
}

public class UpdateTourCommandHandler : IRequestHandler<UpdateTourCommand, bool>
{
    private readonly ITourRepository _repository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;

    public UpdateTourCommandHandler(ITourRepository repository, IMapper mapper, IWebHostEnvironment env)
    {
        _repository = repository;
        _mapper = mapper;
        _env = env;
    }

    public async Task<bool> Handle(UpdateTourCommand request, CancellationToken cancellationToken)
    {
        var existingTour = await _repository.GetByIdAsync(request.Dto.Id);
        if (existingTour == null) return false;

        var oldImageUrl = existingTour.ImageUrl;

        _mapper.Map(request.Dto, existingTour);
        existingTour.UpdatedAt = DateTime.UtcNow;

        // Yeni resim yüklendiyse eskiyi sil, yenisini kaydet
        if (request.Dto.ImageFile != null && request.Dto.ImageFile.Length > 0)
        {
            var extension = Path.GetExtension(request.Dto.ImageFile.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            
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

            existingTour.ImageUrl = $"/uploads/tours/{uniqueFileName}";

            // Eski dosyayı sil
            if (!string.IsNullOrEmpty(oldImageUrl))
            {
                var oldFilePath = Path.Combine(_env.WebRootPath, oldImageUrl.TrimStart('/').Replace('/', '\\'));
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }
        }
        else
        {
            // Yeni resim yüklenmediyse, ImageUrl'yi koru (DTO'da null geldiyse mapper silebilirdi, düzeltelim)
            existingTour.ImageUrl = oldImageUrl;
        }

        await _repository.UpdateAsync(existingTour);

        return true;
    }
}
