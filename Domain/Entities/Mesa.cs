using Domain.Entities;
using Domain.Enums;

namespace Domain.Enums;

public class Mesa
{
    public int Id { get; set; }
    public string Numero { get; set; } = string.Empty;
    public int Capacidad { get; set; }
    public string Ubicacion { get; set; } = string.Empty;
    public EstadoMesa Estado { get; set; } = EstadoMesa.disponible;
    public bool Activo { get; set; } = true;

    public ICollection<ReservaMesa> Reservas { get; set; } = new List<ReservaMesa>();

}

