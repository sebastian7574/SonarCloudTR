using CapaDeDatos.Clases;
using CapaDeEntidad.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio.Clases
{
    public class CN_TipoUsuarioFK
    {
        CD_TipoUsuarioFK CD_TipoUsuarioFK = new CD_TipoUsuarioFK();

        public int idTipoUsuario(string TipoUsuario)
        {
            return CD_TipoUsuarioFK.IdTipoUsuario(TipoUsuario);
        }

        public CE_TipoUsuarioFK nombreTipoUsuario(int IdTipoUsuario)
        {
            return CD_TipoUsuarioFK.NombreTipoUsuario(IdTipoUsuario);
        }

        public List<string> ListarTiposUsuario()
        {
            return CD_TipoUsuarioFK.ObtenerTiposUsuario();
        }
    }
}
