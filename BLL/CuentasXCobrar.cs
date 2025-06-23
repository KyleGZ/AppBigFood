using System;

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
