using Microsoft.AspNetCore.Mvc;
using PC1Web.CORE.Core.DTOs;
using PC1Web.CORE.Core.Interfaces;
namespace PC1Web.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiculoController(IVehiculoService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await service.GetAll());
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var entity = await service.GetById(id);
        return entity is null ? NotFound() : Ok(entity);
    }
    [HttpPost]
    public async Task<IActionResult> Create(VehiculoCreateDTO dto)
    {
        var result = await service.Create(dto);
        if (result.Estado != EstadoOperacion.Correcto) return BadRequest(new { error = result.Error });
        return CreatedAtAction(nameof(GetById), new { id = result.Datos!.Id }, result.Datos);
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, VehiculoUpdateDTO dto) => Respond(await service.Update(id, dto));
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) => Respond(await service.Delete(id));
    private IActionResult Respond(ResultadoOperacion<bool> result) => result.Estado switch
    {
        EstadoOperacion.Correcto => NoContent(),
        EstadoOperacion.NoEncontrado => NotFound(),
        EstadoOperacion.Conflicto => Conflict(new { error = result.Error }),
        _ => BadRequest(new { error = result.Error })
    };
}
