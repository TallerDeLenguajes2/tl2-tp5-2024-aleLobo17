using Microsoft.AspNetCore.Mvc;
using tp5;
using System.Collections.Generic;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresupuestosController : ControllerBase
    {
        private static List<Presupuesto> presupuestos = new List<Presupuesto>();
        private static List<Producto> productos = new List<Producto>(); // Suponiendo que tienes una lista de productos

        [HttpPost]
        public ActionResult<Presupuesto> CrearPresupuesto([FromBody] Presupuesto presupuesto)
        {
            presupuestos.Add(presupuesto);
            return CreatedAtAction(nameof(ObtenerPresupuestos), new { id = presupuesto.IdPresupuesto }, presupuesto);
        }

        [HttpPost("{id}/ProductoDetalle")]
        public ActionResult AgregarProductoDetalle(int id, [FromBody] PresupuestoDetalle detalle)
        {
            var presupuesto = presupuestos.Find(p => p.IdPresupuesto == id);
            if (presupuesto == null) return NotFound();

            var producto = productos.Find(p => p.IdProducto == detalle.Producto.IdProducto);
            if (producto == null) return NotFound("Producto no encontrado.");

            presupuesto.Detalle.Add(detalle);
            return NoContent();
        }

        [HttpGet]
        public ActionResult<List<Presupuesto>> ObtenerPresupuestos()
        {
            return Ok(presupuestos);
        }

        [HttpGet("{id}")]
        public ActionResult<Presupuesto> ObtenerPresupuestoPorId(int id)
        {
            var presupuesto = presupuestos.Find(p => p.IdPresupuesto == id);
            if (presupuesto == null) return NotFound();
            return Ok(presupuesto);
        }
    }
}