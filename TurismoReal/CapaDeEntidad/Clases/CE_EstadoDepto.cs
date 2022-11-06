using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_EstadoDepto
    {
        private int _IdEstadoDepto;
        private string _EstadoDepto;

        public int IdEstadoDepto { get => _IdEstadoDepto; set => _IdEstadoDepto = value; }
        public string EstadoDepto { get => _EstadoDepto; set => _EstadoDepto = value; }
    }
}
