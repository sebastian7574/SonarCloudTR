using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_Boletas
    {
        private int _IdBoleta;
        private string _MedioDePago;
        private string _Banco;
        private DateTime _Fecha;
        private string _Comprobante;
        private int _Monto;
        private int _Efectivo;
        private int _Vuelto;
        private string _Descripcion;
        private int _IdReserva;
        private int _IdDetalleServicio;


        public int IdBoleta { get => _IdBoleta; set => _IdBoleta = value; }
        public string MedioDePago { get => _MedioDePago; set => _MedioDePago = value; }
        public DateTime Fecha { get => _Fecha; set => _Fecha = value; }
        public string Comprobante { get => _Comprobante; set => _Comprobante = value; }
        public int Monto { get => _Monto; set => _Monto = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public int IdReserva { get => _IdReserva; set => _IdReserva = value; }
        public string Banco { get => _Banco; set => _Banco = value; }
        public int Efectivo { get => _Efectivo; set => _Efectivo = value; }
        public int Vuelto { get => _Vuelto; set => _Vuelto = value; }
        public int IdDetalleServicio { get => _IdDetalleServicio; set => _IdDetalleServicio = value; }
    }
}
