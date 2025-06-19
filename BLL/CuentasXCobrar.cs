using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CuentasXCobrar
    {
        public int numFactura { get; set; }

        public string codCliente { get; set; }

        public DateTime FechaFactura { get; set; }

        public DateTime FechaRegistro { get; set; }

        public decimal montoFactura { get; set; }

        public int usuario { get; set; }

        public char estado { get; set; }
    }
}
