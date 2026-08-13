using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum EstadoPedido
    {
        Abierto = 1,     // La mesa está consumiendo / sumando ítems
        Entregado = 2,   // Los productos fueron entregados a la mesa
        Cerrado = 3,     // Se pagó la cuenta
        Cancelado = 4
    }
}
