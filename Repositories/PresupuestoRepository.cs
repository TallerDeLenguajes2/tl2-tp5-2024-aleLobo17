using System;
using System.Net.Http.Headers;
using Microsoft.Data.Sqlite;
using presupuestos;
using presupuestosDetalle;
using productos;
using productoReposotory;

namespace presupuestoRepository
{
    public class PresupuestosRepository
    {
        private string CadenaDeConexion = "Data Source=db/Tienda.db;Cache=Shared";

        public void CrearPresupuesto(Presupuestos presupuesto)
    {
        ProductosRepository repoProductos = new ProductosRepository();

        

        string query = @"INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) 
        VALUES (@destinatario, @fecha)";

        string query2 = @"INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) 
        VALUES (@idP, @idPr, @cant)";

        string query3 = @"SELECT MAX(idPresupuesto) AS idMax FROM Presupuestos;";
        using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);

            command.Parameters.AddWithValue("@destinatario", presupuesto.NombreDestinatario);
            command.Parameters.AddWithValue("@fecha", presupuesto.FechaCreacion);
            command.ExecuteNonQuery();
            SqliteCommand command3 = new SqliteCommand(query3, connection);
            using (SqliteDataReader reader = command3.ExecuteReader())
            {
                if (reader.Read())
                {
                    foreach (var detalle in presupuesto.Detalle)
                    {
                        SqliteCommand command2 = new SqliteCommand(query2, connection);
                        command2.Parameters.AddWithValue("@idP", Convert.ToInt32(reader["idMax"]));
                        command2.Parameters.AddWithValue("@idPr", detalle.Producto.IdProducto);
                        command2.Parameters.AddWithValue("@cant", detalle.Cantidad);
                        command2.ExecuteNonQuery();
                    }
                }
            }
            connection.Close();
        }
        
    }

        public List<Presupuestos> ObtenerPresupuestos()
        {

            List<Presupuestos> listaPresupuestos = new List<Presupuestos>();
            List<PresupuestoDetalle> detalle = new List<PresupuestoDetalle>();

            using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion))
            {
                var query = "SELECT * FROM Presupuestos";
                connection.Open();
                var command = new SqliteCommand(query, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Presupuestos newProd = new Presupuestos(Convert.ToInt32(reader["idPresupuesto"]), Convert.ToString(reader["NombreDestinatario"]) ?? "No tiene Nombre destinatario", Convert.ToString(reader["FechaCreacion"]), detalle);
                        listaPresupuestos.Add(newProd);
                    }
                    connection.Close();
                }
                return listaPresupuestos;
            }
        }

        public Presupuestos ObtenerPresupuestoConDetalles(int idPresupuesto)
        {
            Presupuestos presupuesto = null;
            List<PresupuestoDetalle> listaDetalles = new List<PresupuestoDetalle>();

            var query = @"
                SELECT P.idPresupuesto, P.NombreDestinatario, P.FechaCreacion, 
                PD.idProducto, PD.Cantidad, PR.Descripcion, PR.Precio
                FROM Presupuestos P
                LEFT JOIN PresupuestosDetalle PD ON P.idPresupuesto = PD.IdPresupuesto
                LEFT JOIN Productos PR ON PD.idProducto = PR.idProducto
                WHERE P.idPresupuesto = @idPresupuesto";

            using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion))
            {
                var command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (presupuesto == null)
                        {
                            // Crear el objeto Presupuestos solo la primera vez
                            presupuesto = new Presupuestos(
                                Convert.ToInt32(reader["idPresupuesto"]),
                                Convert.ToString(reader["NombreDestinatario"]) ?? "No tiene destinatario",
                                Convert.ToString(reader["FechaCreacion"]),
                                listaDetalles
                            );
                        }

                        // Crear el objeto Productos
                        var producto = new Productos(
                            Convert.ToInt32(reader["idProducto"]),
                            Convert.ToString(reader["Descripcion"]) ?? "Sin descripción",
                            Convert.ToInt32(reader["Precio"])
                        );

                        // Crear el objeto PresupuestoDetalle
                        var detalle = new PresupuestoDetalle(producto, Convert.ToInt32(reader["Cantidad"]));

                        // Agregar el detalle a la lista de detalles
                        listaDetalles.Add(detalle);
                    }
                }
            }

            return presupuesto;
        }


        public void AgregarProductoAPresupuesto(int idPresupuesto, int idProducto, int cantidad)
        {
            var query = "INSERT INTO PresupuestosDetalle (IdPresupuesto, idProducto, Cantidad) VALUES (@idPresupuesto, @idProducto, @cantidad)";

            using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion))
            {
                connection.Open();
                var command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
                command.Parameters.AddWithValue("@idProducto", idProducto);
                command.Parameters.AddWithValue("@cantidad", cantidad);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void EliminarPresupuestoPorId(int idPresupuesto)
    {


        string query = @"DELETE FROM Presupuestos WHERE idPresupuesto = @IdP;";
        string query2 = @"DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @Id;";
        using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            SqliteCommand command2 = new SqliteCommand(query2, connection);
            command.Parameters.AddWithValue("@IdP", idPresupuesto);
            command2.Parameters.AddWithValue("@Id", idPresupuesto);
            command2.ExecuteNonQuery();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    }

}