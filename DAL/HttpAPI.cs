using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using BLL; 

namespace DAL
{
    public class HttpAPI
    {
        public HttpClient Inicial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://www.ApiBigFoodSA.somee.com");
            return client;
        }

        public HttpClient Seguridad()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7157/");
            return client;
        }

        public HttpClient Gometa()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://apis.gometa.org");
            return client;
        }

        public async Task<List<PermisoDTO>> ObtenerPermisos(string login, string token)
        {
            var client = Seguridad();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"Permisos/ListarPorUsuario?login={login}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<List<PermisoDTO>>(json);
            }

            return new List<PermisoDTO>();
        }
    }
}
