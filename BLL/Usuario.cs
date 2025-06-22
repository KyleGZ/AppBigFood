using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Usuario
    {
        public int IdUsuario { get; set; }                // Cambia el nombre a coincidir con la tabla

        public string NombreUsuario { get; set; }         // nombreUsuario VARCHAR2(100) NOT NULL

        public string Contrasena { get; set; }            // contrasena VARCHAR2(255) NOT NULL

        public string Estado { get; set; }                // estado VARCHAR2(20) DEFAULT 'Activo'
    }
}
