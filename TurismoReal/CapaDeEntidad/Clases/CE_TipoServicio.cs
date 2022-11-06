using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_TipoServicio
    {
        private int _IdTipoServicio;
        private string _TipoServicio;

        public int IdTipoServicio { get => _IdTipoServicio; set => _IdTipoServicio = value; }
        public string TipoServicio { get => _TipoServicio; set => _TipoServicio = value; }
    }
    
}
