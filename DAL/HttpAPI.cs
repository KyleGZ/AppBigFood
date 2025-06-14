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

            client.BaseAddress = new Uri("http://www.puntoventa.somee.com/");

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
