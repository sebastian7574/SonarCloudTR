using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_Region
    {
        private int _IdRegion;
        private string _Region;

        public int IdRegion { get => _IdRegion; set => _IdRegion = value; }
        public string Region { get => _Region; set => _Region = value; }
    }
}
