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
    public class APIFacturas
    {
        //Variable para conectar con la clase que se comunica con la api
        private static HttpAPI _api;

        /// <summary>
        /// Método que devuelve todos los clientes
        /// </summary>
        /// <returns></returns>
        public List<Factura> GetFacturas()
        {
            try
            {
                //Creamos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la API
                HttpClient client = _api.Inicial();

                //Utilizamos los métodos para devolver los cliente
                HttpResponseMessage response = client.GetAsync("/Facturas/ListaFacturas").Result;

                if (response.IsSuccessStatusCode)
                {
                    //Se leen los datos que se obtienen del objeto JSON
                    var result = response.Content.ReadAsStringAsync().Result;

                    //Se convierte el objeto JSON al objeto del modelo
                    return JsonConvert.DeserializeObject<List<Factura>>(result);
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
        public Factura GetFactura(int ID)
        {
            try
            {
                //Creamos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la API
                HttpClient client = _api.Inicial();

                HttpResponseMessage response = client.GetAsync($"/Facturas/BuscarFacturaPorNumero?numero={ID}").Result;

                if (response.IsSuccessStatusCode)
                {
                    //Leemos los datos que se obtienen del objeto JSON
                    var result = response.Content.ReadAsStringAsync().Result;

                    //Convertimos el objeto JSON al objeto modelo
                    return JsonConvert.DeserializeObject<Factura>(result);
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
        public bool CrearFactura(Factura factura)
        {
            try
            {
                //Hacemos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la Api
                HttpClient client = _api.Inicial();

                //Se serealiza el objeto paquete a json
                var json = JsonConvert.SerializeObject(factura);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //Utilizamos el método Post del la API
                HttpResponseMessage response = client.PostAsync("/Facturas/AgregarFactura", content).Result;

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
        /// Método encargado de crear un cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public bool CrearFacturaDetalle(DetalleFactura detalle)
        {
            try
            {
                //Hacemos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la Api
                HttpClient client = _api.Inicial();

                //Se serealiza el objeto paquete a json
                var json = JsonConvert.SerializeObject(detalle);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //Utilizamos el método Post del la API
                HttpResponseMessage response = client.PostAsync("/DetalleFacturas/Save", content).Result;

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
        public DetalleFactura GetDetFactura(int numero)
        {
            try
            {
                //Creamos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la API
                HttpClient client = _api.Inicial();

                HttpResponseMessage response = client.GetAsync($"/DetalleFacturas/Search?numFactura={numero}").Result;

                if (response.IsSuccessStatusCode)
                {
                    //Leemos los datos que se obtienen del objeto JSON
                    var result = response.Content.ReadAsStringAsync().Result;

                    //Convertimos el objeto JSON al objeto modelo
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
                //Hacemos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la Api
                HttpClient client = _api.Inicial();

                //Utilizamos el método Post del la API
                HttpResponseMessage response = client.GetAsync("/CuentasPorCobrar/List").Result;

                if (response.IsSuccessStatusCode)
                {
                    //Leemos los datos que se obtienen del objeto JSON
                    var result = response.Content.ReadAsStringAsync().Result;

                    //Convertimos el objeto JSON al objeto modelo
                    return JsonConvert.DeserializeObject<List<CuentasXCobrar>>(result);
                }
            }
            catch (Exception ex)
            {
                //Capturamos la excepción, pero no se realiza ninguna acción
                throw ex;
            }
            return null;

        }//Fin del método crear cliente
        public bool CrearCuentasXCobarar(CuentasXCobrar cuentas)
        {
            try
            {
                //Hacemos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la Api
                HttpClient client = _api.Inicial();

                //Se serealiza el objeto paquete a json
                var json = JsonConvert.SerializeObject(cuentas);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //Utilizamos el método Post del la API
                HttpResponseMessage response = client.PostAsync("/CuentasPorCobrar/Save", content).Result;

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

        public List<Bitacora> GetBitacora()
        {
            try
            {
                //Hacemos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la Api
                HttpClient client = _api.Seguridad();

                //Utilizamos el método Post del la API
                HttpResponseMessage response = client.GetAsync("/Bitacora/ListaBitacora").Result;

                if (response.IsSuccessStatusCode)
                {
                    //Leemos los datos que se obtienen del objeto JSON
                    var result = response.Content.ReadAsStringAsync().Result;

                    //Convertimos el objeto JSON al objeto modelo
                    return JsonConvert.DeserializeObject<List<Bitacora>>(result);
                }
            }
            catch (Exception ex)
            {
                //Capturamos la excepción, pero no se realiza ninguna acción
                throw ex;
            }
            return null;

        }
        public bool CrearBitacora(Bitacora bitacora)
        {
            try
            {
                //Hacemos la instancia de la API
                _api = new HttpAPI();

                //Consumimos la Api
                HttpClient client = _api.Seguridad();

                //Se serealiza el objeto paquete a json
                var json = JsonConvert.SerializeObject(bitacora);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //Utilizamos el método Post del la API
                HttpResponseMessage response = client.PostAsync("/Bitacora/AgregarBitacora", content).Result;

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

        }
    }
}
