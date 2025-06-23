using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using BLL;

namespace DAL
{
    public class APIFacturas
    {
        private static HttpAPI _api;
        public List<Factura> GetFacturas()
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Inicial();

                HttpResponseMessage response = client.GetAsync("/Facturas/ListaFacturas").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<List<Factura>>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public Factura GetFactura(int ID)
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Inicial();

                HttpResponseMessage response = client.GetAsync($"/Facturas/BuscarFacturaPorNumero?numero={ID}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<Factura>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public bool CrearFactura(Factura factura)
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Inicial();

                var json = JsonConvert.SerializeObject(factura);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("/Facturas/AgregarFactura", content).Result;

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
        public bool CrearFacturaDetalle(DetalleFactura detalle)
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Inicial();

                var json = JsonConvert.SerializeObject(detalle);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("/DetalleFacturas/Save", content).Result;

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
        public DetalleFactura GetDetFactura(int numero)
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Inicial();

                HttpResponseMessage response = client.GetAsync($"/DetalleFacturas/Search?numFactura={numero}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<DetalleFactura>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public List<CuentasXCobrar> CuentasPorCobrar()
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Inicial();

                HttpResponseMessage response = client.GetAsync("/CuentasPorCobrar/List").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<List<CuentasXCobrar>>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;

        }
        public bool CrearCuentasXCobarar(CuentasXCobrar cuentas)
        {
            try
            {
                _api = new HttpAPI();

                HttpClient client = _api.Inicial();

                var json = JsonConvert.SerializeObject(cuentas);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("/CuentasPorCobrar/Save", content).Result;

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
