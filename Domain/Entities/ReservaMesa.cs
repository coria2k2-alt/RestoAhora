using Domain.Enums;

namespace Domain.Entities;


public class ReservaMesa
{
    public int Id { get; set; }

    public int MesaId { get; set; }
    public Mesa? Mesa { get; set; } = null!;

    public string NombreCliente { get; set; } = string.Empty;
    public string EmailCliente { get; set; } = string.Empty;
    public string TelefonoCliente { get; set; } = string.Empty;
    
    public DateTime FechaHoraInicioUtc { get; set; }
    public DateTime FechaHoraFinUtc { get; set; }
    public int CantidadComensales { get; set; }
    public decimal MontoSeña { get; set; }

    public EstadoReserva Estado { get; set; } = EstadoReserva.pendiente;
    public DateTime FechaCreacionUtc { get; set; } = DateTime.UtcNow;
}
