using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pedidos
    {
        public int Id { get; set; }
        public int MesaId{ get; set; }
        public Mesa? Mesa { get; set; } = null!;
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public EstadoPedido estado { get; set; } = EstadoPedido.Abierto;


        // Lista de renglones/productos cargados en la comanda
        public List<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();

        public decimal Total => Detalles?.Sum(d => d.Subtotal) ?? 0;
    }
}
