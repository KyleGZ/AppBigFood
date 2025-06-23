using System;

namespace BLL
{
    public class Cliente
    {
        public string cedulaLegal { get; set; }

        public string tipoCedula { get; set; }

        public string NombreCompleto { get; set; }

        public string Email { get; set; }

        public DateTime fechaRegistro { get; set; }

        public char estado { get; set; }

        public int Usuario { get; set; }
    }
}
