using System;
using Microsoft.Data.Sqlite;
using productos;

namespace productoReposotory
{
    public class ProductosRepository
    {
        private string CadenaDeConexion = "Data Source=db/Tienda.db;Cache=Shared";

        public void CrearProducto(Productos nuevo)
        {
            using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion))
            {
                var query = "INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio)";
                connection.Open();
                var command = new SqliteCommand(query, connection);
                command.Parameters.Add(new SqliteParameter("@Descripcion", nuevo.Descripcion));
                command.Parameters.Add(new SqliteParameter("@Precio", nuevo.Precio));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void ModificarProductos(int id, Productos produ)
        {
            using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion))
            {
                var query = "UPDATE Productos SET Descripcion = @Descripcion, Precio = @Precio WHERE idProducto = @id";
                connection.Open();
                var command = new SqliteCommand(query, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                command.Parameters.Add(new SqliteParameter("@Descripcion", produ.Descripcion));
                command.Parameters.Add(new SqliteParameter("@Precio", produ.Precio));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Productos> ObtenerProductos()
        {

            List<Productos> listaProductos = new List<Productos>();

            using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion))
            {
                var query = "SELECT * FROM Productos";
                connection.Open();
                var command = new SqliteCommand(query, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Productos newProd = new Productos(Convert.ToInt32(reader["idProducto"]), Convert.ToString(reader["Descripcion"]) ?? "No tiene descripcion", Convert.ToInt32(reader["Precio"]));
                        listaProductos.Add(newProd);
                    }
                    connection.Close();
                }
                return listaProductos;
            }
        }

        public Productos ObtenerDetalleProducto(int id){
            Productos newProd = null;
            var query = "SELECT * FROM Productos WHERE idProducto = @id";
            using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion)){
                var command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using ( var reader = command.ExecuteReader()){
                    if(reader.Read()){
                        newProd = new Productos(Convert.ToInt32(reader["idProducto"]), Convert.ToString(reader["Descripcion"]) ?? "No tiene descripcion", Convert.ToInt32(reader["Precio"]));
                    }
                }
                connection.Close();
            }
            return newProd;
        }


        public void EliminarProducto(int id){
            var query = "DELETE FROM Productos WHERE idProducto = @id";
            using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion)){
                connection.Open();
                var command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }


}