using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_Reservas
    {
        private int _IdReserva;
        private DateTime _FechaDesde;
        private DateTime _FechaHasta;
        private string _EstadoRerserva;
        private int _Abono;
        private DateTime _CheckIN;
        private DateTime _CheckOUT;
        private int _PrecioNocheReserva;
        private int _Saldo;
        private int _IdDepartamento;
        private int _IdUsuario;

        public int IdReserva { get => _IdReserva; set => _IdReserva = value; }
        public DateTime FechaDesde { get => _FechaDesde; set => _FechaDesde = value; }
        public DateTime FechaHasta { get => _FechaHasta; set => _FechaHasta = value; }
        public int Abono { get => _Abono; set => _Abono = value; }
        public DateTime CheckIN { get => _CheckIN; set => _CheckIN = value; }
        public DateTime CheckOUT { get => _CheckOUT; set => _CheckOUT = value; }
        public int PrecioNocheReserva { get => _PrecioNocheReserva; set => _PrecioNocheReserva = value; }
        public int Saldo { get => _Saldo; set => _Saldo = value; }
        public int IdDepartamento { get => _IdDepartamento; set => _IdDepartamento = value; }
        public int IdUsuario { get => _IdUsuario; set => _IdUsuario = value; }
        public string EstadoRerserva { get => _EstadoRerserva; set => _EstadoRerserva = value; }
    }
}
