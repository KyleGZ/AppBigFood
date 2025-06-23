using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using BLL;

namespace DAL
{
    public class APIProducto
    {
        private static HttpAPI _api;
        public List<Producto> GetProductos()
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Inicial();

                HttpResponseMessage response = client.GetAsync("/Productos/List").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<List<Producto>>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public Producto GetProducto(int CodInterno)
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Inicial();

                HttpResponseMessage response = client.GetAsync($"/Productos/SearchByCodigoInterno?codigoInterno={CodInterno}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<Producto>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;

        }
        public bool CrearProducto(Producto producto)
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Inicial();

                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("/Productos/Save", content).Result;

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
        public bool ActualizarProducto(Producto producto)
        {
            try
            {
                _api = new HttpAPI();

                HttpClient product = _api.Inicial();

                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = product.PutAsync("/Productos/Update", content).Result;

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
        public bool EliminarProducto(int CodInterno)
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Inicial();

                HttpResponseMessage response = client.DeleteAsync($"/Productos/Delete?codigoInterno={CodInterno}").Result;

                if (response.IsSuccessStatusCode)
                {
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
