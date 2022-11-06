using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_Reportes
    {
        private int _IdReporte;
        private int _Total;
        private int _CantReservas;
        private int _Gastos;
        private DateTime _FechaDesde;
        private DateTime _FechaHasta;
        private int _IdDepartamento;
        private int _IdRegion;
        public int IdReporte { get => _IdReporte; set => _IdReporte = value; }
        public DateTime FechaDesde { get => _FechaDesde; set => _FechaDesde = value; }
        public DateTime FechaHasta { get => _FechaHasta; set => _FechaHasta = value; }
        public int IdDepartamento { get => _IdDepartamento; set => _IdDepartamento = value; }
        public int Total { get => _Total; set => _Total = value; }
        public int CantReservas { get => _CantReservas; set => _CantReservas = value; }
        public int Gastos { get => _Gastos; set => _Gastos = value; }
        public int IdRegion { get => _IdRegion; set => _IdRegion = value; }
    }
}
