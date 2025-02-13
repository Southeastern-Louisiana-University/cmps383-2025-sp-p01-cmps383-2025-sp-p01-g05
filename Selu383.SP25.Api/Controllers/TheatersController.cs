using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.Api.Dtos;

namespace Selu383.SP25.Api.Controllers;

[Route("api/theaters")]
[ApiController]
public class TheatersController : ControllerBase
{
    private readonly DataContext _context;

    public TheatersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<TheaterDto>>> GetTheaters()
    {
        return await _context.Theaters
            .Select(x => new TheaterDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                SeatCount = x.SeatCount
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TheaterDto>> GetTheater(int id)
    {
        var theater = await _context.Theaters.FindAsync(id);
        if (theater == null)
        {
            return NotFound();
        }

        var dto = new TheaterDto
        {
            Id = theater.Id,
            Name = theater.Name,
            Address = theater.Address,
            SeatCount = theater.SeatCount
        };

        return dto;
    }

    [HttpPost]
    public async Task<ActionResult<TheaterDto>> CreateTheater(TheaterDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length > 120 ||
            string.IsNullOrWhiteSpace(dto.Address) || dto.SeatCount < 1)
        {
            return BadRequest();
        }

        var theater = new Theater
        {
            Name = dto.Name,
            Address = dto.Address,
            SeatCount = dto.SeatCount
        };
    
        _context.Theaters.Add(theater);
        await _context.SaveChangesAsync();

        dto.Id = theater.Id;
        return CreatedAtAction(nameof(GetTheater), new { id = theater.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TheaterDto>> UpdateTheater(int id, TheaterDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length > 120 ||
            string.IsNullOrWhiteSpace(dto.Address))
        {
            return BadRequest();
        }

        var theater = await _context.Theaters.FindAsync(id);
        if (theater == null)
        {
            return NotFound();
        }

        theater.Name = dto.Name;
        theater.Address = dto.Address;
        theater.SeatCount = dto.SeatCount;

        await _context.SaveChangesAsync();
        return dto;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTheater(int id)
    {
        var theater = await _context.Theaters.FindAsync(id);
        if (theater == null)
        {
            return NotFound();
        }

        _context.Theaters.Remove(theater);
        await _context.SaveChangesAsync();
        return Ok();
    }
}