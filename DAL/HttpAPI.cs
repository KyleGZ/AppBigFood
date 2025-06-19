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

            client.BaseAddress = new Uri("https://localhost:7141");

            return client;
        }

        public HttpClient Seguridad()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:7284");

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
