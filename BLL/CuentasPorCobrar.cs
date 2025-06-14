using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CuentasPorCobrar
    {
        public int Id { get; set; }

        public int codCliente { get; set; }

        public DateTime FechaFactura { get; set; }

        public string FechaRegistro { get; set; }

        public double MontoFactura { get; set; }

        public string usuario { get; set; }

        public string Estado { get; set; }
    }
}
