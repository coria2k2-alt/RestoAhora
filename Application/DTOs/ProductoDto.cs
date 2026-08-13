using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ProductoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, 1000000.00, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal Precio { get; set; }

        public bool Disponible { get; set; } = true;

        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        [Display(Name = "Categoría")]
        public int CategoriaProductoId { get; set; }

        public string? NombreCategoria { get; set; }
    }
}