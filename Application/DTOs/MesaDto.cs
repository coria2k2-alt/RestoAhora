using Domain.Enums;

namespace Application.DTOs;

public class MesaDto
{
    public int Id { get; set; }
    public string Numero { get; set; } = string.Empty;
    public int Capacidad { get; set; }
    public string Ubicacion { get; set; } = string.Empty;
    public EstadoMesa Estado { get; set; }
    public bool Activo { get; set; }
}