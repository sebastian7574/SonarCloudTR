using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_Comuna
    {
        private int _IdComuna;
        private string _Comuna;
        private int _IdRegion;

        public int IdComuna { get => _IdComuna; set => _IdComuna = value; }
        public string Comuna { get => _Comuna; set => _Comuna = value; }
        public int IdRegion { get => _IdRegion; set => _IdRegion = value; }
    }
}
