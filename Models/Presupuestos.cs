using System;
using Microsoft.AspNetCore.Routing.Constraints;
using presupuestosDetalle;

namespace presupuestos
{

    public class Presupuestos
    {
        private int idPresupuesto;
        private string nombreDestinatario;
        private List<PresupuestoDetalle> detalle;
        private string fechaCreacion;

        public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
        public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
        public List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value; }
        public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }

        public Presupuestos(int idPresupuesto, string nombreDestinatario, string fechaCreacion, List<PresupuestoDetalle> detalle)
    {
        this.idPresupuesto = idPresupuesto;
        this.nombreDestinatario = nombreDestinatario;
        this.fechaCreacion = fechaCreacion;
        this.detalle = detalle;
    }

        public float MontoPresupuesto()
        {
            float acumuladorMonto = 0;
            foreach (var item in Detalle)
            {

                if (Detalle != null)
                {
                    acumuladorMonto = item.Producto.Precio;
                }

            }
            return acumuladorMonto;
        }

        public double MontoPresupuestoConIva()
        {
            double acumuladorMontoConIva = 0;
            foreach (var item in Detalle)
            {

                if (item != null)
                {
                    acumuladorMontoConIva = item.Producto.Precio + (item.Producto.Precio * 0.21) ;
                }

            }
            return acumuladorMontoConIva;
        }

        public int CantidadProductos()
        {
            int acumuladorCantidadProductos = 0;
            foreach (var item in Detalle){
                if (item != null){
                    acumuladorCantidadProductos++;
                }
            }
            return acumuladorCantidadProductos;
        }
    }

}