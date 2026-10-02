using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PC1Web.CORE.Core.DTOs;
using PC1Web.CORE.Core.Entities;
using PC1Web.CORE.Infrastructure.Data;
namespace PC1Web.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TipoServicioController(TallerMecanicoContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await db.TipoServicio.AsNoTracking().OrderBy(e => e.Id)
        .Select(e => new TipoServicioListDTO { Id = e.Id, Nombre = e.Nombre, PrecioBase = e.PrecioBase }).ToListAsync());
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var entity = await db.TipoServicio.FindAsync(id);
        return entity is null ? NotFound() : Ok(new TipoServicioListDTO { Id = entity.Id, Nombre = entity.Nombre, PrecioBase = entity.PrecioBase });
    }
    [HttpPost]
    public async Task<IActionResult> Create(TipoServicioCreateDTO dto)
    {
        if (decimal.Round(dto.PrecioBase, 2) != dto.PrecioBase) return BadRequest(new { error = "El precio admite como máximo dos decimales." });
        var entity = new TipoServicio { Nombre = dto.Nombre.Trim(), PrecioBase = dto.PrecioBase };
        db.TipoServicio.Add(entity);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, new TipoServicioListDTO { Id = entity.Id, Nombre = entity.Nombre, PrecioBase = entity.PrecioBase });
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, TipoServicioUpdateDTO dto)
    {
        if (id != dto.Id) return BadRequest(new { error = "Los IDs de la ruta y del cuerpo deben coincidir." });
        if (decimal.Round(dto.PrecioBase, 2) != dto.PrecioBase) return BadRequest(new { error = "El precio admite como máximo dos decimales." });
        var entity = await db.TipoServicio.FindAsync(id);
        if (entity is null) return NotFound();
        entity.Nombre = dto.Nombre.Trim();
        entity.PrecioBase = dto.PrecioBase;
        await db.SaveChangesAsync();
        return NoContent();
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await db.TipoServicio.FindAsync(id);
        if (entity is null) return NotFound();
        if (await db.OrdenServicio.AnyAsync(o => o.TipoServicioId == id)) return Conflict(new { error = "El tipo de servicio tiene órdenes asociadas." });
        db.TipoServicio.Remove(entity);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
