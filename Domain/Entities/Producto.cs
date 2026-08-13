using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Producto
    {
        public int id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public bool Disponible { get; set; } = true;

        // Clave foránea y relación con Categoría
        public int CategoriaProductoID { get; set; }
        public CategoriaProducto? Categoria { get; set; }

    }
}
