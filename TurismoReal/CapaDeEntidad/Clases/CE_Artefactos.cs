using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_Artefactos
    {
        private int _IdArtefactos;
        private string _Descripcion;
        private int _Tamano;
        private string _Color;
        private int _Valor;
        private int _IdUnidadMedida;

        public int IdArtefactos { get => _IdArtefactos; set => _IdArtefactos = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public int Tamano { get => _Tamano; set => _Tamano = value; }
        public string Color { get => _Color; set => _Color = value; }
        public int Valor { get => _Valor; set => _Valor = value; }
        public int IdUnidadMedida { get => _IdUnidadMedida; set => _IdUnidadMedida = value; }
    }
}
