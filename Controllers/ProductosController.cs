using Microsoft.AspNetCore.Mvc;
using tp5;
using System.Collections.Generic;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private static List<Producto> productos = new List<Producto>();

        [HttpPost]
        public ActionResult<Producto> CrearProducto([FromBody] Producto producto)
        {
            productos.Add(producto);
            return CreatedAtAction(nameof(ObtenerProductos), new { id = producto.IdProducto }, producto);
        }

        [HttpGet]
        public ActionResult<List<Producto>> ObtenerProductos()
        {
            return Ok(productos);
        }

        [HttpPut("{id}")]
        public ActionResult ModificarProducto(int id, [FromBody] string nuevoNombre)
        {
            var producto = productos.Find(p => p.IdProducto == id);
            if (producto == null) return NotFound();

            producto.Descripcion = nuevoNombre;
            return NoContent();
        }
    }
}