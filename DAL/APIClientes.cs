using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

//Aquí importamos las bibliotecas
using Newtonsoft.Json;
using BLL;
using System.Net.Http.Headers;

namespace DAL
{
    public class APIClientes
    {
        //Variable para conectar con la clase que se comunica con la api
        private static HttpAPI _api;

        /// <summary>
        /// Método que devuelve todos los clientes
        /// </summary>
        /// <returns></returns>
        public List<Cliente> GetClientes()
        {
            try
            {
                //Creamos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la API
                HttpClient client = _api.Inicial();


                //Utilizamos los métodos para devolver los cliente
                HttpResponseMessage response = client.GetAsync("/Clientes/ListaClientes").Result;

                if (response.IsSuccessStatusCode)
                {
                    //Se leen los datos que se obtienen del objeto JSON
                    var result = response.Content.ReadAsStringAsync().Result;

                    //Se convierte el objeto JSON al objeto del modelo
                    return JsonConvert.DeserializeObject<List<Cliente>>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }//Cierre del método GetClientes

        /// <summary>
        /// Método encargado de devolverme un proyecto en específico
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Cliente GetCliente(int ID)
        {
            try
            {
                //Creamos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la API
                HttpClient client = _api.Inicial();

                HttpResponseMessage response = client.GetAsync($"/Clientes/BuscarClientePorCedula?cedula={ID}").Result;

                if (response.IsSuccessStatusCode)
                {
                    //Leemos los datos que se obtienen del objeto JSON
                    var result = response.Content.ReadAsStringAsync().Result;

                    //Convertimos el objeto JSON al objeto modelo
                    return JsonConvert.DeserializeObject<Cliente>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }//Cierre del método GetCliente

        /// <summary>
        /// Método encargado de crear un cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public bool CrearCliente(Cliente cliente)
        {
            try
            {
                //Hacemos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la Api
                HttpClient client = _api.Inicial();

                //Se serealiza el objeto paquete a json
                var json = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //Utilizamos el método Post del la API
                HttpResponseMessage response = client.PostAsync("/Clientes/AgregarCliente", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    //La edición del cliente se realizó correctamente
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Capturamos la excepción, pero no se realiza ninguna acción
                throw ex;
            }
            return false;

        }//Fin del método crear cliente

        /// <summary>
        /// Método para actualizar cliente
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public bool ActualizarCliente(Cliente cliente)
        {
            try
            {
                //Creamos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la API
                HttpClient client = _api.Inicial();

                //Se serealiza el objeto Cliente a JSON
                var json = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //Se utiliza el método PUT de la API
                HttpResponseMessage response = client.PutAsync("/Clientes/EditarCliente", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    //La edición del cliente se realizó correctamente
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }//Fin del método ActualizarCliente

        public bool EliminarCliente(int ID)
        {
            try
            {
                //Se crea la instancia de la API
                _api = new HttpAPI();

                //Se consume la API
                HttpClient client = _api.Inicial();

                //Utilizamos el método Delete de la API
                HttpResponseMessage response = client.DeleteAsync($"/Clientes/EliminarCliente?id={ID}").Result;

                if (response.IsSuccessStatusCode)
                {
                    //La edición del producto se realizó correctamente
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }
    }//
}//
