using PC1Web.CORE.Core.DTOs;
namespace PC1Web.CORE.Core.Interfaces;

public interface IVehiculoService
{
    Task<IEnumerable<VehiculoListDTO>> GetAll();
    Task<VehiculoListDTO?> GetById(int id);
    Task<ResultadoOperacion<VehiculoListDTO>> Create(VehiculoCreateDTO dto);
    Task<ResultadoOperacion<bool>> Update(int id, VehiculoUpdateDTO dto);
    Task<ResultadoOperacion<bool>> Delete(int id);
}
