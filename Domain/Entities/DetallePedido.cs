namespace Domain.Entities
{
    public class DetallePedido
    {
        public int Id { get; set; }

        // Relación con el Pedido principal
        public int PedidoId { get; set; }
        public Pedidos? Pedido { get; set; }

        public int productoId { get; set; }
        public Producto? producto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal => Cantidad * PrecioUnitario;

    }
}