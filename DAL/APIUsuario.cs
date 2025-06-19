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

        public async Task<AutorizacionResponse> Login(Usuario usuario)
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
                AutorizacionResponse authorized = null;
                //Utilizamos el método POST de la API
                HttpResponseMessage response = await client.PostAsync("/AutorizacionUsuario/Autenticar", content);

                if (response.IsSuccessStatusCode)
                {
                    //La edición del producto se realizó correctamente 
                    var result = response.Content.ReadAsStringAsync().Result;
                    authorized = JsonConvert.DeserializeObject<AutorizacionResponse>(result);
                }
                return authorized;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//Cierre del método Login

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
