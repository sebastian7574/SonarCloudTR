using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_TipoGasto
    {
        private int _IdTipoGasto;
        private string _TipoGasto;

        public int IdTipoGasto { get => _IdTipoGasto; set => _IdTipoGasto = value; }
        public string TipoGasto { get => _TipoGasto; set => _TipoGasto = value; }
    }
}
