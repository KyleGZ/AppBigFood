using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BLL;

namespace DAL
{
    public class APIUsuario
    {
        private static HttpAPI _api;
        public async Task<bool> CrearUsuario(Usuario usuario)
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Seguridad();

                var json = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("/Usuarios/AgregarUsuario", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        public List<Usuario> GetUsuarios()
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Seguridad();

                HttpResponseMessage response = client.GetAsync("/Usuarios/ListaUsuario").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

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
                _api = new HttpAPI();

                HttpClient client = _api.Seguridad();

                HttpResponseMessage response = client.GetAsync($"/Usuarios/BuscarUsuarioPorId?id={ID}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

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
                _api = new HttpAPI();

                HttpClient client = _api.Seguridad();

                HttpResponseMessage response = client.GetAsync($"/Usuarios/BuscarUsuarioPorLogin?login={login}&pass={pass}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

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
                    throw new Exception($"Error: {response.StatusCode}\n{result}");
                }
                return authorized;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Login: " + ex.Message, ex);
            }
        }
        public bool ActualizarUsuario(Usuario usuario)
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Seguridad();

                var json = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync("/Usuarios/EditarUsuario", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }

        public bool EliminarUsuario(int ID)
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Seguridad();

                HttpResponseMessage response = client.DeleteAsync($"/Usuarios/EliminarUsuario?id={ID}").Result;

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }
    }
}
