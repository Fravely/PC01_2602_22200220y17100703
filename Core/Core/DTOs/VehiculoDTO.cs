using System.ComponentModel.DataAnnotations;
namespace PC1Web.CORE.Core.DTOs;

public class VehiculoCreateDTO
{
    [Required, StringLength(10)]
    public string Placa { get; set; } = string.Empty;
    [Required, StringLength(50)]
    public string Marca { get; set; } = string.Empty;
    [Required, StringLength(50)]
    public string Modelo { get; set; } = string.Empty;
    [Range(1886, 2100)]
    public int Anio { get; set; }
    [Range(1, int.MaxValue)]
    public int ClienteId { get; set; }
}
public class VehiculoUpdateDTO : VehiculoCreateDTO
{
    [Range(1, int.MaxValue)]
    public int Id { get; set; }
}
public class VehiculoListDTO
{
    public int Id { get; set; }
    public string Placa { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int Anio { get; set; }
    public int ClienteId { get; set; }
    public string ClienteNombre { get; set; } = string.Empty;
}
