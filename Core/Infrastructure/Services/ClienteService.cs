using PC1Web.CORE.Core.DTOs;
using PC1Web.CORE.Core.Entities;
using PC1Web.CORE.Core.Interfaces;
namespace PC1Web.CORE.Infrastructure.Services;

public class ClienteService(IClienteRepository repository) : IClienteService
{
    private static ClienteListDTO Map(Cliente e) => new()
    {
        Id = e.Id,
        Paterno = e.Paterno,
        Materno = e.Materno,
        Nombres = e.Nombres,
        Correo = e.Correo,
        Telefono = e.Telefono
    };
    private static void Assign(Cliente entity, ClienteCreateDTO dto)
    {
        entity.Paterno = dto.Paterno.Trim();
        entity.Materno = dto.Materno.Trim();
        entity.Nombres = dto.Nombres.Trim();
        entity.Correo = dto.Correo.Trim();
        entity.Telefono = dto.Telefono.Trim();
    }
    private Task<string?> Validate(ClienteCreateDTO dto) => Task.FromResult<string?>(null);
    public async Task<IEnumerable<ClienteListDTO>> GetAll() => (await repository.GetAll()).Select(Map);
    public async Task<ClienteListDTO?> GetById(int id)
    {
        var entity = await repository.GetById(id);
        return entity is null ? null : Map(entity);
    }
    public async Task<ResultadoOperacion<ClienteListDTO>> Create(ClienteCreateDTO dto)
    {
        var error = await Validate(dto);
        if (error is not null) return new(EstadoOperacion.Invalido, Error: error);
        var entity = new Cliente();
        Assign(entity, dto);
        await repository.Create(entity);
        return new(EstadoOperacion.Correcto, await GetById(entity.Id));
    }
    public async Task<ResultadoOperacion<bool>> Update(int id, ClienteUpdateDTO dto)
    {
        if (id != dto.Id) return new(EstadoOperacion.Invalido, Error: "Los IDs de la ruta y del cuerpo deben coincidir.");
        var entity = await repository.GetById(id);
        if (entity is null) return new(EstadoOperacion.NoEncontrado);
        var error = await Validate(dto);
        if (error is not null) return new(EstadoOperacion.Invalido, Error: error);
        Assign(entity, dto);
        await repository.Save();
        return new(EstadoOperacion.Correcto, true);
    }
    public async Task<ResultadoOperacion<bool>> Delete(int id)
    {
        var entity = await repository.GetById(id);
        if (entity is null) return new(EstadoOperacion.NoEncontrado);
        if (await repository.TieneVehiculos(id)) return new(EstadoOperacion.Conflicto, Error: "El cliente tiene vehículos asociados.");
        await repository.Delete(entity);
        return new(EstadoOperacion.Correcto, true);
    }
}
