using System.ComponentModel.DataAnnotations;
namespace PC1Web.CORE.Core.DTOs;

public class OrdenServicioCreateDTO
{
    public DateTime? FechaIngreso { get; set; }
    [Required, StringLength(500)]
    public string DescripcionProblema { get; set; } = string.Empty;
    [Range(typeof(decimal), "0", "99999999.99")]
    public decimal CostoEstimado { get; set; }
    [Required, StringLength(30), RegularExpression("^(Pendiente|En proceso|Finalizado)$", ErrorMessage = "Estado válido: Pendiente, En proceso o Finalizado.")]
    public string Estado { get; set; } = string.Empty;
    [Range(1, int.MaxValue)]
    public int VehiculoId { get; set; }
    [Range(1, int.MaxValue)]
    public int TipoServicioId { get; set; }
}
public class OrdenServicioUpdateDTO : OrdenServicioCreateDTO
{
    [Range(1, int.MaxValue)]
    public int Id { get; set; }
}
public class OrdenServicioListDTO
{
    public int Id { get; set; }
    public DateTime FechaIngreso { get; set; }
    public string DescripcionProblema { get; set; } = string.Empty;
    public decimal CostoEstimado { get; set; }
    public string Estado { get; set; } = string.Empty;
    public int VehiculoId { get; set; }
    public int TipoServicioId { get; set; }
    public string VehiculoPlaca { get; set; } = string.Empty;
    public string TipoServicioNombre { get; set; } = string.Empty;
}
