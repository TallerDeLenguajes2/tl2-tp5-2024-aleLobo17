using System;
using productos;

namespace presupuestosDetalle{

    public class PresupuestoDetalle{
        private Productos producto;
        private int cantidad;

        public Productos Producto { get => producto; set => producto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public PresupuestoDetalle(Productos producto, int cantidad)
    {
        this.producto = producto;
        this.cantidad = cantidad;
    }
    }
}

