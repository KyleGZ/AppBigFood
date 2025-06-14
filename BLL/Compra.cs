using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Compra
    {
        //public int Id { get; set; }

        public int ClienteId { get; set; }

        public string TipoPa { get; set; }

        public string Condicion { get; set; }

        public DateTime Fecha { get; set; }
    }
}
