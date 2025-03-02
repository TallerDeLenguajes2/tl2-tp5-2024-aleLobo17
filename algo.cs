public void CrearNuevo(Producto prod)
    {
        using ( SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            var query = "INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio)";
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@Descripcion", prod.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@Precio", prod.Precio));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
public List<Producto> ListarProducto()
        {
            List<Producto> listaProd = new List<Producto>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                string query = "SELECT * FROM Productos;";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var prod = new Producto();
                        prod.IdProducto = Convert.ToInt32(reader["idProducto"]);
                        prod.Descripcion = reader["Descripcion"].ToString();
                        prod.Precio = Convert.ToInt32(reader["Precio"]);
                        listaProd.Add(prod);
                    }
                }
                connection.Close();

            }
            return listaProd;
        }