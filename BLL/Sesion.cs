using System.Collections.Generic;

namespace BLL
{
    public static class Sesion
    {
        public static string UsuarioLogin { get; set; }
        public static string Token { get; set; }

        public static List<PermisoDTO> Permisos { get; set; } = new List<PermisoDTO>();

        public static bool TienePermiso(string pantalla, string accion)
        {
            return Permisos.Exists(p =>
                p.Pantalla.ToLower() == pantalla.ToLower() &&
                p.Accion.ToLower() == accion.ToLower());
        }
    }
    public class PermisoDTO
    {
        public string Pantalla { get; set; }
        public string Accion { get; set; }
    }
}
