using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_Inventario
    {
        private int _IdInventario;
        private int _Cantidad;
        private int _IdArtefactos;
        private int _IdDepartamento;

        public int IdInventario { get => _IdInventario; set => _IdInventario = value; }
        public int Cantidad { get => _Cantidad; set => _Cantidad = value; }
        public int IdArtefactos { get => _IdArtefactos; set => _IdArtefactos = value; }
        public int IdDepartamento { get => _IdDepartamento; set => _IdDepartamento = value; }
    }
}
