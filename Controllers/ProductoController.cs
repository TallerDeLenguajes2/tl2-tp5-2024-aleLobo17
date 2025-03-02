using Microsoft.AspNetCore.Mvc;
using productoReposotory;
using productos;

namespace productoController{

[ApiController]
[Route("Controllers")]

public class ProductoController : ControllerBase{
    private readonly ProductosRepository _productosRepository;

    public ProductoController(){
        _productosRepository = new ProductosRepository();
    }

    [HttpPost("/api/Producto")]
    public IActionResult CrearProducto([FromBody] Productos newProd){
        try{
            _productosRepository.CrearProducto(newProd);
            return Ok("Producto creado satisfactoriamente");
        }
        catch(Exception ex){
            return StatusCode(500, $"ERROR {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public  IActionResult ModificarProductos(int id, [FromBody] Productos newProd){
        try{
            _productosRepository.ModificarProductos(id, newProd);
            return Ok("Productos modificado satisfactoriamente");
        }
        catch(Exception ex){
            return StatusCode(500, $"ERROR {ex.Message}");
        }
    }

    [HttpGet("/api/Producto")]
    public ActionResult<List<Productos>> ObtenerProductos(){
        try{
            List<Productos> listaProductos = _productosRepository.ObtenerProductos();
            return Ok(listaProductos);
        }
        catch(Exception ex){    
            return StatusCode(500, $"ERROR {ex.Message}");
        }
    }

    [HttpGet("/api/Producto/{id}")]
    public IActionResult ObtenerDetalleProducto(int id){
        try{
            var detallerDelProductos = _productosRepository.ObtenerDetalleProducto(id);
            return Ok(detallerDelProductos);
        }
        catch(Exception ex){
            return StatusCode(500, $"ERROR {ex.Message}");
        }
    }
    [HttpDelete("{id}")]
    public IActionResult EliminarProducto(int id){
        _productosRepository.EliminarProducto(id);
        return Ok("Producto eliminado satisfactoriamente");
    }

}
    


}

