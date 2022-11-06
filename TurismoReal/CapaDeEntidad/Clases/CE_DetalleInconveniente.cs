using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_DetalleInconveniente
    {
        private int _IdDetalleInconveniente;
        private string _Detalle;
        private string _MedioDePago;
        private string _Banco;
        private string _Comprobante;
        private int _Monto;
        private int _Efectivo;
        private string _Vuelto;
        private int _IdReserva;

        public int IdDetalleInconveniente { get => _IdDetalleInconveniente; set => _IdDetalleInconveniente = value; }
        public string Detalle { get => _Detalle; set => _Detalle = value; }
        public int Monto { get => _Monto; set => _Monto = value; }
        public int IdReserva { get => _IdReserva; set => _IdReserva = value; }
        public string MedioDePago { get => _MedioDePago; set => _MedioDePago = value; }
        public string Banco { get => _Banco; set => _Banco = value; }
        public string Comprobante { get => _Comprobante; set => _Comprobante = value; }
        public int Efectivo { get => _Efectivo; set => _Efectivo = value; }
        public string Vuelto { get => _Vuelto; set => _Vuelto = value; }
    }
}
