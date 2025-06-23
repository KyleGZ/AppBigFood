using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using BLL;

namespace DAL
{
    public class APIClientes
    {
        private static HttpAPI _api;
        public List<Cliente> GetClientes()
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Inicial();

                HttpResponseMessage response = client.GetAsync("/Clientes/ListaClientes").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<List<Cliente>>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public Cliente GetCliente(int ID)
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Inicial();

                HttpResponseMessage response = client.GetAsync($"/Clientes/BuscarClientePorCedula?cedula={ID}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<Cliente>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public bool CrearCliente(Cliente cliente)
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Inicial();

                var json = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("/Clientes/AgregarCliente", content).Result;

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
        public bool ActualizarCliente(Cliente cliente)
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Inicial();

                var json = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync("/Clientes/EditarCliente", content).Result;

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
        public bool EliminarCliente(int ID)
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Inicial();

                HttpResponseMessage response = client.DeleteAsync($"/Clientes/EliminarCliente?id={ID}").Result;

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
