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
        public int Id { get; set; }

        public string login { get; set; }

        public string password { get; set; }

        public DateTime fechaRegistro { get; set; }

        public char estado { get; set; }
    }
}
