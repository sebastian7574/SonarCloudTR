using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_Gastos
    {
        private int _IdGastos;
        private string _Descripcion;
        private int _Monto;
        private DateTime _FechaGastos;
        private int _IdDepartamento;
        private int _IdTipoGastos;

        public int IdGastos { get => _IdGastos; set => _IdGastos = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public int Monto { get => _Monto; set => _Monto = value; }
        public DateTime FechaGastos { get => _FechaGastos; set => _FechaGastos = value; }
        public int IdDepartamento { get => _IdDepartamento; set => _IdDepartamento = value; }
        public int IdTipoGastos { get => _IdTipoGastos; set => _IdTipoGastos = value; }
    }
}
