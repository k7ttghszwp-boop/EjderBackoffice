using Ejder.Application.Tours.Commands;
using Ejder.Application.Tours.DTOs;
using Ejder.Application.Tours.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ejder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToursController : ControllerBase
{
    private readonly IMediator _mediator;

    public ToursController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Aktif tüm turları getirir (Public)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _mediator.Send(new GetToursQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Sayfalı ve kategori filtreli tur listesi (Public)
    /// </summary>
    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] Guid? categoryId = null)
    {
        try
        {
            var result = await _mediator.Send(new GetToursPagedQuery(page, pageSize, categoryId));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Tek bir tur detayını getirir (Public)
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetTourByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Kategoriye göre tur listesi (Public)
    /// </summary>
    [HttpGet("category/{catId}")]
    public async Task<IActionResult> GetByCategory(Guid catId)
    {
        try
        {
            var result = await _mediator.Send(new GetToursByCategoryQuery(catId));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Yeni tur oluşturur (Authorize, multipart/form-data)
    /// </summary>
    [HttpPost]
    [Authorize]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateTourDto dto)
    {
        try
        {
            var id = await _mediator.Send(new CreateTourCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Tur günceller (Authorize, multipart/form-data)
    /// </summary>
    [HttpPut("{id}")]
    [Authorize]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateTourDto dto)
    {
        try
        {
            if (id != dto.Id) return BadRequest("ID mismatch");
            var result = await _mediator.Send(new UpdateTourCommand(dto));
            if (!result) return NotFound();
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Tur siler (Authorize)
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new DeleteTourCommand(id));
            if (!result) return NotFound();
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
