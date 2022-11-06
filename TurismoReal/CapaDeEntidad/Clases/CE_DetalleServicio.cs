using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_DetalleServicio
    {
        private int _IdDetalleServicio;
        private DateTime _Fecha;
        private int _MontoTotal;
        private int _IdServicio;
        private int _IdUsuario;

        public int IdDetalleServicio { get => _IdDetalleServicio; set => _IdDetalleServicio = value; }
        public DateTime Fecha { get => _Fecha; set => _Fecha = value; }
        public int MontoTotal { get => _MontoTotal; set => _MontoTotal = value; }
        public int IdServicio { get => _IdServicio; set => _IdServicio = value; }
        public int IdUsuario { get => _IdUsuario; set => _IdUsuario = value; }
    }
}
