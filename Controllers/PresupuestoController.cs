using Microsoft.AspNetCore.Mvc;
using presupuestoRepository;
using presupuestos;
using presupuestosDetalle;

namespace presupuestoController
{
    [ApiController]
    [Route("Controllers")]

    public class PresupuestoController : ControllerBase
    {
        private readonly PresupuestosRepository _presupuestoRepository;

        public PresupuestoController()
        {
            _presupuestoRepository = new PresupuestosRepository();
        }

        [HttpPost("/api/Presupuesto")]
        public IActionResult CrearProducto([FromBody] Presupuestos newPresu)
        {
            try
            {
                _presupuestoRepository.CrearPresupuesto(newPresu);
                return Ok("Presupuesto creado satisfactoriamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERROR {ex.Message}");
            }
        }

        [HttpGet("/api/Presupuesto")]
        public ActionResult<List<Presupuestos>> ObtenerPresupuestos()
        {
            try
            {
                List<Presupuestos> listaPresupuestos = _presupuestoRepository.ObtenerPresupuestos();
                return Ok(listaPresupuestos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERROR {ex.Message}");
            }
        }

        [HttpGet("/api/Presupuesto/{id}")]
        public IActionResult ObtenerPresupuestoConDetalles(int id)
        {
            try
            {
                var presupuesto = _presupuestoRepository.ObtenerPresupuestoConDetalles(id);
                
                if (presupuesto == null)
                {
                    return NotFound("Presupuesto no encontrado.");
                }

                return Ok(presupuesto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERROR {ex.Message}");
            }
        }


        [HttpPost("/api/Presupuesto/{id}")]
        public IActionResult AgregarProductoAPresupuesto(int id, [FromBody] PresupuestoDetalle detalle)
        {
            try
            {
                _presupuestoRepository.AgregarProductoAPresupuesto(id, detalle.Producto.IdProducto, detalle.Cantidad);
                return Ok("Producto agregado satisfactoriamente al presupuesto.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERROR {ex.Message}");
            }

        }

        [HttpDelete("/api/Presupuesto/{id}")]
    public IActionResult EliminarPresupuestoPorId(int id){
        _presupuestoRepository.EliminarPresupuestoPorId(id);
        return Ok("Producto eliminado satisfactoriamente");
    }

    }
}