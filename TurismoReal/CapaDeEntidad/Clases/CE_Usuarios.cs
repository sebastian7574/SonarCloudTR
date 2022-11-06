using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_Usuarios
    {
        private int _idUsuario;
        private string _nombres;
        private string _apellidos;
        private string _usuario;
        private string _correo;
        private string _contrasena;
        private string _patron;
        private string _identificacion;
        private string _celular;
        private string _pais;
        private string _codigoVerificacion;
        private string _habilitada;
        private string _esPasaporte;
        private int _idTipoUsuario;

        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public string Nombres { get => _nombres; set => _nombres = value; }
        public string Apellidos { get => _apellidos; set => _apellidos = value; }
        public string Usuario { get => _usuario; set => _usuario = value; }
        public string Correo { get => _correo; set => _correo = value; }
        public string Contrasena { get => _contrasena; set => _contrasena = value; }
        public string Patron { get => _patron; set => _patron = value; }
        public string Identificacion { get => _identificacion; set => _identificacion = value; }
        public string Celular { get => _celular; set => _celular = value; }
        public string Pais { get => _pais; set => _pais = value; }
        public string CodigoVerificacion { get => _codigoVerificacion; set => _codigoVerificacion = value; }
        public int IdTipoUsuario { get => _idTipoUsuario; set => _idTipoUsuario = value; }
        public string Habilitada { get => _habilitada; set => _habilitada = value; }
        public string EsPasaporte { get => _esPasaporte; set => _esPasaporte = value; }
    }
}
