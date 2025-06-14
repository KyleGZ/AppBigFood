using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

//Aquí importamos las bibliotecas
using Newtonsoft.Json;
using BLL;

namespace DAL
{
    public class APIProducto
    {
        //Variable para conectar con la clase que se comunica con la API
        private static HttpAPI _api;

        /// <summary>
        /// Método encargado de devolver todos los productos
        /// </summary>
        /// <returns></returns>
        public List<Producto> GetProductos()
        {
            try
            {
                //Creamos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la API
                HttpClient client = _api.Inicial();

                //Utilizamos el método para devolver los productos
                HttpResponseMessage response = client.GetAsync("api/Productos").Result;

                if (response.IsSuccessStatusCode)
                {
                    //Se leen los datos que se obtienen del objeto JSON
                    var result = response.Content.ReadAsStringAsync().Result;

                    //Se convierte el objeto Json al objeto del modelo
                    return JsonConvert.DeserializeObject<List<Producto>>(result);
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }//Fin del método GetProductos

        /// <summary>
        /// Método encargado de devolver un producto
        /// </summary>
        /// <param name="CodInterno"></param>
        /// <returns></returns>
        public Producto GetProducto(int? CodInterno)
        {
            try
            {
                //Creamos la instancia de la api
                _api = new HttpAPI();

                //Consumimos la api
                HttpClient client = _api.Inicial();

                //Utilizamos el método get de la api
                HttpResponseMessage response = client.GetAsync("api/Productos/" + CodInterno).Result;

                if (response.IsSuccessStatusCode)
                {
                    //Leemos los datos que se obtienen del objeto JSON
                    var result = response.Content.ReadAsStringAsync().Result;

                    //Convertimos el objeto JSON al objeto del modelo
                    return JsonConvert.DeserializeObject<Producto>(result);
                }
            }
            catch (Exception ex)
            {

            }
            return null;

        }//Fin del método GetProducto

        /// <summary>
        /// Método encargado encargado de crear un producto
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public bool CrearProducto(Producto producto)
        {
            try
            {
                //Creamos una instancia de la API
                _api = new HttpAPI();

                //Consumimos la API
                HttpClient client = _api.Inicial();

                //Se serealiza el objeto paquete a json
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //Utilizamos el método POST de la API
                HttpResponseMessage response = client.PostAsync("api/Productos", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    //La edición del producto se realizó correctamente 
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Capturamos la excepción, pero no se realiza ninguna acción
                Console.WriteLine(ex.Message);
            }
            return false;
        }//Cierre del método CrearProducto

        /// <summary>
        /// Método para actualizar el producto
        /// </summary>
        /// <param name="CodInterno"></param>
        /// <param name="producto"></param>
        /// <returns></returns>
        public bool ActualizarProducto(int CodInterno, Producto producto)
        {
            try
            {
                //Creamos la instancia de la api
                _api = new HttpAPI();

                //Consumimos la API
                HttpClient client = _api.Inicial();

                //Se serealiza el objeto paquete a json
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //Se utiliza el método PUT de la API
                HttpResponseMessage response = client.PostAsync("api/Productos/" + CodInterno, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    //La edicón del producto se realizó correctamente
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return false;
        }

        /// <summary>
        /// Método para eliminar un producto
        /// </summary>
        /// <param name="CodInterno"></param>
        /// <returns></returns>
        public bool EliminarProducto(int CodInterno)
        {
            try
            {
                //Se crea la instancia de la API
                _api = new HttpAPI();

                //Se consume en la API
                HttpClient client = _api.Inicial();

                //Utilizamos el método Delete de la API
                HttpResponseMessage response = client.DeleteAsync("api/Productos/" + CodInterno).Result;

                if (response.IsSuccessStatusCode)
                {
                    //La edición del Producto se realizó correctamente
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }
    }
}
