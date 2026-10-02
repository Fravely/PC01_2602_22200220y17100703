using PC1Web.CORE.Core.DTOs;
using PC1Web.CORE.Core.Entities;
using PC1Web.CORE.Core.Interfaces;
namespace PC1Web.CORE.Infrastructure.Services;

public class VehiculoService(IVehiculoRepository repository) : IVehiculoService
{
    private static VehiculoListDTO Map(Vehiculo e) => new()
    {
        Id = e.Id,
        Placa = e.Placa,
        Marca = e.Marca,
        Modelo = e.Modelo,
        Anio = e.Anio,
        ClienteId = e.ClienteId,
        ClienteNombre = $"{e.Cliente.Nombres} {e.Cliente.Paterno} {e.Cliente.Materno}"
    };
    private static void Assign(Vehiculo entity, VehiculoCreateDTO dto)
    {
        entity.Placa = dto.Placa.Trim();
        entity.Marca = dto.Marca.Trim();
        entity.Modelo = dto.Modelo.Trim();
        entity.Anio = dto.Anio;
        entity.ClienteId = dto.ClienteId;
    }
    private async Task<string?> Validate(VehiculoCreateDTO dto)
    {
        if (!await repository.ClienteExiste(dto.ClienteId)) return "El cliente no existe.";
        return null;
    }
    public async Task<IEnumerable<VehiculoListDTO>> GetAll() => (await repository.GetAll()).Select(Map);
    public async Task<VehiculoListDTO?> GetById(int id)
    {
        var entity = await repository.GetById(id);
        return entity is null ? null : Map(entity);
    }
    public async Task<ResultadoOperacion<VehiculoListDTO>> Create(VehiculoCreateDTO dto)
    {
        var error = await Validate(dto);
        if (error is not null) return new(EstadoOperacion.Invalido, Error: error);
        var entity = new Vehiculo();
        Assign(entity, dto);
        await repository.Create(entity);
        return new(EstadoOperacion.Correcto, await GetById(entity.Id));
    }
    public async Task<ResultadoOperacion<bool>> Update(int id, VehiculoUpdateDTO dto)
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
        if (await repository.TieneOrdenes(id)) return new(EstadoOperacion.Conflicto, Error: "El vehículo tiene órdenes asociadas.");
        await repository.Delete(entity);
        return new(EstadoOperacion.Correcto, true);
    }
}
