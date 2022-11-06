using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CB_MedioPago
    {
        private int _IdMedioPago;
        private string _MedioPago;

        public int IdMedioPago { get => _IdMedioPago; set => _IdMedioPago = value; }
        public string MedioPago { get => _MedioPago; set => _MedioPago = value; }
    }
}
