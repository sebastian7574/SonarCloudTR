using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_Servicios
    {
        private int _IdServicio;
        private string _Descripcion;
        private string _Disponibilidad;
        private int _Precio;
        private int _IdTipoServicio;

        public int IdServicio { get => _IdServicio; set => _IdServicio = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public string Disponibilidad { get => _Disponibilidad; set => _Disponibilidad = value; }
        public int Precio { get => _Precio; set => _Precio = value; }
        public int IdTipoServicio { get => _IdTipoServicio; set => _IdTipoServicio = value; }
    }
}
