using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ReservaCrearDto
    {
        [Required(ErrorMessage = "Debe seleccionar una mesa.")]
        [Range(1, int.MaxValue, ErrorMessage = "Mesa inválida.")]
        public int MesaId { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string NombreCliente { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un email válido.")]
        public string EmailCliente { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Ingrese un número de teléfono válido.")]
        public string TelefonoCliente { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha y hora de inicio es obligatoria.")]
        public DateTime FechaHoraInicioUtc { get; set; }

        [Required(ErrorMessage = "La fecha y hora de fin es obligatoria.")]
        public DateTime FechaHoraFinUtc { get; set; }

        [Required(ErrorMessage = "Ingrese la cantidad de comensales.")]
        [Range(1, 50, ErrorMessage = "La cantidad de comensales debe ser entre 1 y 50.")]
        public int CantidadComensales { get; set; }

        [Range(0, 1000000, ErrorMessage = "El monto de seña no puede ser negativo.")]
        public decimal MontoSena { get; set; }
    }
}