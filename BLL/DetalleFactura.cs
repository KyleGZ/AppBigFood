﻿namespace BLL
{
    public class DetalleFactura
    {
        public int numFactura { get; set; }

        public int codInterno { get; set; }

        public int cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }

        public decimal PorImp { get; set; }

        public decimal PorDescuento { get; set; }

    }
}
