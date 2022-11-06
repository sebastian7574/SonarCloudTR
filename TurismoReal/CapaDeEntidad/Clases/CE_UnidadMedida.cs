using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_UnidadMedida
    {
        private int _IdUnidadMedida;
        private string _tipoUnidad;

        public int IdUnidadMedida { get => _IdUnidadMedida; set => _IdUnidadMedida = value; }
        public string TipoUnidad { get => _tipoUnidad; set => _tipoUnidad = value; }
    }
}
