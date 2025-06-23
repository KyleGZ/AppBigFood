using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.APISecurity
{
    public class UsuarioPantallaPermiso
    {
        public int IdUsuario { get; set; }

        public int IdPantalla { get; set; }

        public int IdPermiso { get; set; }

        public Usuario Usuario { get; set; }

        public Pantalla Pantalla { get; set; }

        public Permiso Permiso { get; set; }
    }
}
