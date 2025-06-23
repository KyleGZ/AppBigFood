using System;

namespace BLL
{
    public class Factura
    {
        public int numero { get; set; }

        public DateTime Fecha { get; set; }

        public string codCliente { get; set; }

        public decimal SubTotal { get; set; }

        public decimal MontoDescuento { get; set; }

        public decimal MontoImpuesto { get; set; }

        public decimal Total { get; set; }

        public char estado { get; set; }

        public int Usuario { get; set; }

        public string TipoPago { get; set; }

        public string Condicion { get; set; }
    }
}
