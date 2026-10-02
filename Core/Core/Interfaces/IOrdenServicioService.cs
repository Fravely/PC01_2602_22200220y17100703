using PC1Web.CORE.Core.DTOs;
namespace PC1Web.CORE.Core.Interfaces;

public interface IOrdenServicioService
{
    Task<IEnumerable<OrdenServicioListDTO>> GetAll();
    Task<OrdenServicioListDTO?> GetById(int id);
    Task<ResultadoOperacion<OrdenServicioListDTO>> Create(OrdenServicioCreateDTO dto);
    Task<ResultadoOperacion<bool>> Update(int id, OrdenServicioUpdateDTO dto);
    Task<ResultadoOperacion<bool>> Delete(int id);
}
