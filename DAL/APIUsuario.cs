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
    public class APIUsuario
    {
        //Variable para conectar con la clase que se comunica con la API
        private static HttpAPI _api;


        /// <summary>
        /// Método encargado encargado de crear un producto
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public async Task<bool> CrearUsuario(Usuario usuario)
        {
            try
            {
                //Creamos una instancia de la API
                _api = new HttpAPI();

                //Consumimos la API
                HttpClient client = _api.Seguridad();

                //Se serealiza el objeto paquete a json
                var json = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //Utilizamos el método POST de la API
                HttpResponseMessage response = await client.PostAsync("/Usuarios/AgregarUsuario", content);

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
        }//Cierre del método CrearUsuario
        public List<Usuario> GetUsuarios()
        {
            try
            {
                //Creamos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la API
                HttpClient client = _api.Seguridad();


                //Utilizamos los métodos para devolver los cliente
                HttpResponseMessage response = client.GetAsync("/Usuarios/ListaUsuario").Result;

                if (response.IsSuccessStatusCode)
                {
                    //Se leen los datos que se obtienen del objeto JSON
                    var result = response.Content.ReadAsStringAsync().Result;

                    //Se convierte el objeto JSON al objeto del modelo
                    return JsonConvert.DeserializeObject<List<Usuario>>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<Usuario> GetUsuario(int ID)
        {
            try
            {
                //Creamos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la API
                HttpClient client = _api.Seguridad();

                HttpResponseMessage response = client.GetAsync($"/Usuarios/BuscarUsuarioPorId?id={ID}").Result;

                if (response.IsSuccessStatusCode)
                {
                    //Leemos los datos que se obtienen del objeto JSON
                    var result = response.Content.ReadAsStringAsync().Result;

                    //Convertimos el objeto JSON al objeto modelo
                    return JsonConvert.DeserializeObject<Usuario>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public async Task<Usuario> GetUsuarioLogin(string login, string pass)
        {
            try
            {
                //Creamos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la API
                HttpClient client = _api.Seguridad();

                HttpResponseMessage response = client.GetAsync($"/Usuarios/BuscarUsuarioPorLogin?login={login}&pass={pass}").Result;

                if (response.IsSuccessStatusCode)
                {
                    //Leemos los datos que se obtienen del objeto JSON
                    var result = response.Content.ReadAsStringAsync().Result;

                    //Convertimos el objeto JSON al objeto modelo
                    return JsonConvert.DeserializeObject<Usuario>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        //public async Task<AutorizacionResponse> Login(Usuario usuario)
        //{
        //    try
        //    {
        //        //Creamos una instancia de la API
        //        _api = new HttpAPI();

        //        //Consumimos la API
        //        HttpClient client = _api.Seguridad();

        //        //Se serealiza el objeto paquete a json
        //        var json = JsonConvert.SerializeObject(usuario);
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //        AutorizacionResponse authorized = null;
        //        //Utilizamos el método POST de la API
        //        HttpResponseMessage response = await client.PostAsync("/AutorizacionUsuario/Autenticar", content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            //La edición del producto se realizó correctamente 
        //            var result = response.Content.ReadAsStringAsync().Result;
        //            authorized = JsonConvert.DeserializeObject<AutorizacionResponse>(result);
        //        }
        //        return authorized;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}//Cierre del Login

        public async Task<AutorizacionResponse> Login(Usuario usuario)
        {
            try
            {
                _api = new HttpAPI();
                HttpClient client = _api.Seguridad();

                var json = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                AutorizacionResponse authorized = null;

                HttpResponseMessage response = await client.PostAsync("/Usuarios/Autenticar", content);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    authorized = JsonConvert.DeserializeObject<AutorizacionResponse>(result);
                }
                else
                {
                    // En vez de mostrar MessageBox, lanza una excepción con el error
                    throw new Exception($"Error: {response.StatusCode}\n{result}");
                }
                return authorized;
            }
            catch (Exception ex)
            {
                // Re-lanza el error para que la UI lo capture y lo muestre
                throw new Exception("Error en Login: " + ex.Message, ex);
            }
        }


        public bool ActualizarUsuario(Usuario usuario)
        {
            try
            {
                //Creamos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la API
                HttpClient client = _api.Seguridad();

                //Se serealiza el objeto Cliente a JSON
                var json = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //Se utiliza el método PUT de la API
                HttpResponseMessage response = client.PutAsync("/Usuarios/EditarUsuario", content).Result;

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

        public bool EliminarUsuario(int ID)
        {
            try
            {
                //Se crea la instancia de la API
                _api = new HttpAPI();

                //Se consume la API
                HttpClient client = _api.Seguridad();

                //Utilizamos el método Delete de la API
                HttpResponseMessage response = client.DeleteAsync($"/Usuarios/EliminarUsuario?id={ID}").Result;

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

        //    public bool Logout()
        //    {
        //        try
        //        {
        //            //Creamos la instancia de la API
        //            _api = new HttpAPI();

        //            //Consumimos la API
        //            HttpClient client = _api.Inicial();

        //            //Utilizamos el método para devolver los productos
        //            HttpResponseMessage response = client.PostAsync("api/Usuarios/logout", null).Result;

        //            if (response.IsSuccessStatusCode)
        //            {
        //                //Se leen los datos que se obtienen del objeto JSON
        //                var result = response.Content.ReadAsStringAsync().Result;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //Capturamos la excepción, pero no se realiza ninguna acción
        //            Console.WriteLine(ex.Message);
        //        }
        //        return false;
        //    }//Cierre del método CrearUsuario
    }
}
