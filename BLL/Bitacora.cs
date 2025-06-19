using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Bitacora
    {
        public string Tabla { get; set; }

        public int Usuario { get; set; }

        public string Maquina { get; set; }

        public DateTime Fecha { get; set; }

        public string TipoMov { get; set; }

        public string Registro { get; set; }
    }
}
