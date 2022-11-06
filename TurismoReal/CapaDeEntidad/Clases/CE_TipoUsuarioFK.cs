using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_TipoUsuarioFK
    {
        private int _IdTipoUsuario;
        private string _TipoUsuario;

        public int IdTipoUsuario { get => _IdTipoUsuario; set => _IdTipoUsuario = value; }
        public string TipoUsuario { get => _TipoUsuario; set => _TipoUsuario = value; }
    }
}
