using PC1Web.CORE.Core.DTOs;
using PC1Web.CORE.Core.Entities;
using PC1Web.CORE.Core.Interfaces;
namespace PC1Web.CORE.Infrastructure.Services;

public class OrdenServicioService(IOrdenServicioRepository repository) : IOrdenServicioService
{
    private static OrdenServicioListDTO Map(OrdenServicio e) => new()
    {
        Id = e.Id,
        FechaIngreso = e.FechaIngreso,
        DescripcionProblema = e.DescripcionProblema,
        CostoEstimado = e.CostoEstimado,
        Estado = e.Estado,
        VehiculoId = e.VehiculoId,
        TipoServicioId = e.TipoServicioId,
        VehiculoPlaca = e.Vehiculo.Placa,
        TipoServicioNombre = e.TipoServicio.Nombre
    };
    private static void Assign(OrdenServicio entity, OrdenServicioCreateDTO dto)
    {
        if (dto.FechaIngreso.HasValue) entity.FechaIngreso = dto.FechaIngreso.Value;
        entity.DescripcionProblema = dto.DescripcionProblema.Trim();
        entity.CostoEstimado = dto.CostoEstimado;
        entity.Estado = dto.Estado.Trim();
        entity.VehiculoId = dto.VehiculoId;
        entity.TipoServicioId = dto.TipoServicioId;
    }
    private async Task<string?> Validate(OrdenServicioCreateDTO dto)
    {
        if (!await repository.VehiculoExiste(dto.VehiculoId)) return "El vehículo no existe.";
        if (!await repository.TipoServicioExiste(dto.TipoServicioId)) return "El tipo de servicio no existe.";
        if (dto.FechaIngreso == DateTime.MinValue) return "La fecha de ingreso no es válida.";
        if (decimal.Round(dto.CostoEstimado, 2) != dto.CostoEstimado) return "El costo admite como máximo dos decimales.";
        return null;
    }
    public async Task<IEnumerable<OrdenServicioListDTO>> GetAll() => (await repository.GetAll()).Select(Map);
    public async Task<OrdenServicioListDTO?> GetById(int id)
    {
        var entity = await repository.GetById(id);
        return entity is null ? null : Map(entity);
    }
    public async Task<ResultadoOperacion<OrdenServicioListDTO>> Create(OrdenServicioCreateDTO dto)
    {
        var error = await Validate(dto);
        if (error is not null) return new(EstadoOperacion.Invalido, Error: error);
        var entity = new OrdenServicio();
        Assign(entity, dto);
        await repository.Create(entity);
        return new(EstadoOperacion.Correcto, await GetById(entity.Id));
    }
    public async Task<ResultadoOperacion<bool>> Update(int id, OrdenServicioUpdateDTO dto)
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

        await repository.Delete(entity);
        return new(EstadoOperacion.Correcto, true);
    }
}
