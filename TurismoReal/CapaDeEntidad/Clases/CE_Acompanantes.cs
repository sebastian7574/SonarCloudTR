using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_Acompanantes
    {
        private int _IdAcompanantes;
        private string _Nombres;
        private string _Apellidos;
        private string _Identificacion;
        private int _IdReserva;

        public int IdAcompanantes { get => _IdAcompanantes; set => _IdAcompanantes = value; }
        public string Nombres { get => _Nombres; set => _Nombres = value; }
        public string Apellidos { get => _Apellidos; set => _Apellidos = value; }
        public string Identificacion { get => _Identificacion; set => _Identificacion = value; }
        public int IdReserva { get => _IdReserva; set => _IdReserva = value; }
    }
}
