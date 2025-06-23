using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.APISecurity
{
    public class RolPantallaPermiso
    {
        public int IdRol { get; set; }
        public int IdPantalla { get; set; }
        public int IdPermiso { get; set; }

        // Propiedades para las relaciones
        public Rol Rol { get; set; }
        public Pantalla Pantalla { get; set; }
        public Permiso Permiso { get; set; }
    }
}
