using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        public string cedula { get; set; }

        public string TipoCedula { get; set; }

        public string NombreCompleto { get; set; }

        public string Email { get; set; }

        public string Estado { get; set; }
    }
}
