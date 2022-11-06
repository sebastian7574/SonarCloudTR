using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad.Clases
{
    public class CE_Galeria
    {
        private int _idGaleria;
        private byte[] _imagen;
        private string _descripcionImagen;
        private int _idDepartamento;

        public int idGaleria { get => _idGaleria; set => _idGaleria = value; }
        public byte[] Imagen { get => _imagen; set => _imagen = value; }
        public int IdDepartamento { get => _idDepartamento; set => _idDepartamento = value; }
        public string DescripcionImagen { get => _descripcionImagen; set => _descripcionImagen = value; }
    }
}
