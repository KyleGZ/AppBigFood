using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

            client.BaseAddress = new Uri("http://www.ApiSecurity.somee.com");

            return client;
        }

        public HttpClient Gometa()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("https://apis.gometa.org");

            return client;
        }
    }
}
