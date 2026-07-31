namespace Application.DTOs;

public class ReservaCrearDto
{
    public int MesaId { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public string EmailCliente { get; set; } = string.Empty;
    public string TelefonoCliente { get; set; } = string.Empty;
    public DateTime FechaHoraInicioUtc { get; set; }
    public DateTime FechaHoraFinUtc { get; set; }
    public int CantidadComensales { get; set; }
    public decimal MontoSena { get; set; }
}