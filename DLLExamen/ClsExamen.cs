using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace APIExamen
{
    public static class ClsExamen
    {
        // Cadena de conexión a la base de datos
        public static string connectionString = "Server=localhost\\SQLEXPRESS;Database=BdiExamen;Trusted_Connection=True;";
        public static async Task<string> ConsultarExamen(string metodo, string nombre, string descripcion)
        {
            if (metodo == "PROCEDURE")
            {


                // Nombre del procedimiento almacenado
                string storedProcedureName = "spConsultar";

                // Lista para almacenar los datos obtenidos del procedimiento almacenado
                List<Examen> datosList = new List<Examen>();

                // Crear y configurar la conexión
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();

                    // Crear un nuevo comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        // Especificar que se ejecutará un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Descripcion", descripcion);

                        // Ejecutar el procedimiento almacenado y obtener los datos
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Leer los datos y agregarlos a la lista
                            while (reader.Read())
                            {
                                Examen datos2 = new Examen();
                                datos2.idexamen = reader.GetInt32(0);
                                datos2.nombre = reader.GetString(1);
                                datos2.descripcion = reader.GetString(2);
                                // Si hay más columnas, agregarlas según sea necesario
                                datosList.Add(datos2);
                            }
                        }
                    }

                }

                // Serializar la lista de datos como JSON
                string jsonData = JsonConvert.SerializeObject(datosList);
                return jsonData;


            }
            if (metodo == "API")
            {
                string apiUrl = "https://localhost:44320/api/TbIExamen";

                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        // Realizar una solicitud GET a la API y obtener la respuesta
                        HttpResponseMessage response = await client.GetAsync(apiUrl);

                        // Verificar si la solicitud fue exitosa (código de estado 200)
                        if (response.IsSuccessStatusCode)
                        {
                            // Leer el contenido de la respuesta como una cadena
                            string responseBody = await response.Content.ReadAsStringAsync();

                            // Procesar el cuerpo de la respuesta

                            return responseBody;
                        }
                        else
                        {
                            // Manejar el caso en que la solicitud no sea exitosa

                            return response.StatusCode.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar cualquier excepción que pueda ocurrir durante la solicitud

                        return $"Error al realizar la solicitud: {ex.Message}";
                    }

                }


            }


            return "";
        }
        public static async Task<string> ConsultarExamen(int id)
        {

            string apiUrl = "https://localhost:44320/api/TbIExamen/" + id;

            // Crear una instancia de HttpClient
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Realizar una solicitud GET a la API y obtener la respuesta
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Verificar si la solicitud fue exitosa (código de estado 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer el contenido de la respuesta como una cadena
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Procesar el cuerpo de la respuesta

                        return responseBody;
                    }
                    else
                    {
                        // Manejar el caso en que la solicitud no sea exitosa

                        return response.StatusCode.ToString();
                    }
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que pueda ocurrir durante la solicitud

                    return $"Error al realizar la solicitud: {ex.Message}";
                }

            }        
    
        }
        public static async Task<string> AgregarExamen(string metodo, int id, string nombre, string descripcion)
        {
            if (metodo == "API")
            {
                string apiUrl = "https://localhost:44320/api/TbIExamen";

                Examen Examen = new Examen(id, nombre, descripcion);
                string json = JsonConvert.SerializeObject(Examen);
              
                using (HttpClient httpClient = new HttpClient())
                {
                    try
                    {
                        // Configurar el encabezado Content-Type para indicar que se está enviando JSON
                        httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");

                        // Crear el contenido de la solicitud con los datos JSON
                        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                        // Realizar la solicitud POST a la API
                        HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                        // Verificar si la solicitud fue exitosa
                        if (response.IsSuccessStatusCode)
                        {
                            // Leer y mostrar la respuesta de la API
                            string responseData = await response.Content.ReadAsStringAsync();
                            
                            Console.WriteLine(responseData);
                        }
                        else
                        {
                            return $"La solicitud no fue exitosa. Código de estado: {response.StatusCode}";
                        }
                    }
                    catch (Exception ex)
                    {
                        return($"Error al realizar la solicitud: {ex.Message}");
                    }
                }
            }

            if (metodo == "PROCEDURE")
            {


                // Nombre del procedimiento almacenado
                string storedProcedureName = "spAgregar";
          

                // Crear y configurar la conexión
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();

                    // Crear un nuevo comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        // Especificar que se ejecutará un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Descripcion", descripcion);
                        try
                        {
                            // Ejecutar el procedimiento almacenado de actualización
                            int rowsAffected = command.ExecuteNonQuery();

                            Console.WriteLine($"Se actualizaron {rowsAffected} filas.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                        }

                    }

                }

                // Serializar la lista de datos como JSON


            }

            return "";
        }
        public static async Task<string> ActualizarExamen(string metodo, int id, string nombre, string descripcion)
        {
            if (metodo == "API")
            {
                string apiUrl = "https://localhost:44320/api/TbIExamen/" + id;

                Examen Examen = new Examen(id, nombre, descripcion);
                string json = JsonConvert.SerializeObject(Examen);

                using (HttpClient httpClient = new HttpClient())
                {
                    try
                    {
                        // Configurar el encabezado Content-Type para indicar que se está enviando JSON
                        httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");

                        // Crear el contenido de la solicitud con los datos JSON
                        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                        // Realizar la solicitud Put a la API
                        HttpResponseMessage response = await httpClient.PutAsync(apiUrl, content);

                        // Verificar si la solicitud fue exitosa
                        if (response.IsSuccessStatusCode)
                        {
                            // Leer y mostrar la respuesta de la API
                            string responseData = await response.Content.ReadAsStringAsync();

                            Console.WriteLine(responseData);
                        }
                        else
                        {
                            return $"La solicitud no fue exitosa. Código de estado: {response.StatusCode}";
                        }
                    }
                    catch (Exception ex)
                    {
                        return ($"Error al realizar la solicitud: {ex.Message}");
                    }
                }
            }

            if (metodo == "PROCEDURE")
            {

                // Nombre del procedimiento almacenado
                string storedProcedureName = "spActualizar";
                // Crear y configurar la conexión
                using (SqlConnection connection = new SqlConnection(connectionString))                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        // Especificar que se ejecutará un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Descripcion", descripcion);
                       
                        try
                        {
                            // Ejecutar el procedimiento almacenado de actualización
                            int rowsAffected = command.ExecuteNonQuery();

                            Console.WriteLine($"Se actualizaron {rowsAffected} filas.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                        }

                    }

                }            

            }

            return "";
        }
        public static async Task<string> EliminarExamen(int id, string metodo)
        {
            if (metodo == "API")
            {
                string apiUrl = "https://localhost:7170/api/TbIExamen/" + id;
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        // Realizar una solicitud GET a la API y obtener la respuesta
                        HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                        // Verificar si la solicitud fue exitosa (código de estado 200)
                        if (response.IsSuccessStatusCode)
                        {
                            // Leer el contenido de la respuesta como una cadena
                            string responseBody = await response.Content.ReadAsStringAsync();

                            // Procesar el cuerpo de la respuesta

                            return responseBody;
                        }
                        else
                        {
                            // Manejar el caso en que la solicitud no sea exitosa

                            return response.StatusCode.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar cualquier excepción que pueda ocurrir durante la solicitud

                        return $"Error al realizar la solicitud: {ex.Message}";
                    }

                }
            }
            if (metodo == "PROCEDURE")
            {
                string connectionString = "Server=localhost\\SQLEXPRESS;Database=BdiExamen;Trusted_Connection=True;";
                // Nombre del procedimiento almacenado
                string storedProcedureName = "spEliminar";
                // Crear y configurar la conexión
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        // Especificar que se ejecutará un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);
                        try
                        {
                            // Ejecutar el procedimiento almacenado de actualización
                            int rowsAffected = command.ExecuteNonQuery();

                            Console.WriteLine($"Se actualizaron {rowsAffected} filas.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                        }
                    }

                }
            }


            return "";
        }

        public static int existeExamen( string nombre, string descripcion )
        {
            // Nombre del procedimiento almacenado
            string storedProcedureName = "spConsultar";

           
            // Crear y configurar la conexión
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();

                // Crear un nuevo comando para ejecutar el procedimiento almacenado
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    // Especificar que se ejecutará un procedimiento almacenado
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Descripcion", descripcion);

                    // Ejecutar el procedimiento almacenado y obtener los datos
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Leer los datos
                      if(reader.Read())
                        {
                            Examen datos2 = new Examen();
                            datos2.idexamen = reader.GetInt32(0);

                            return datos2.idexamen;
                        }
                        else {
                            return 0;
                        }
                            
                    }
                    
                }

            }

        }
    }

}

   

