using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Producto
    {
        public int CodiInterno { get; set; }

        public string CodiBarra { get; set; }

        public string Descripcion { get; set; }

        public double PrecioVenta { get; set; }

        public double Descuento { get; set; }

        public double Impuesto { get; set; }

        public string UnidadMedida { get; set; }

        public double PrecioCompra { get; set; }

        public int Existencia { get; set; }
    }
}
