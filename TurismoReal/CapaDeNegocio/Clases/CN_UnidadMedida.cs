using CapaDeDatos.Clases;
using CapaDeEntidad.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio.Clases
{
    public class CN_UnidadMedida
    {
        CD_UnidadMedida CD_UnidadMedida = new CD_UnidadMedida();

        public int IdUnidad(string TipoUnidad)
        {
            return CD_UnidadMedida.IdUnidad(TipoUnidad);
        }

        public CE_UnidadMedida NombreUnidad(int IdUnidadMedida)
        {
            return CD_UnidadMedida.NombreUnidad(IdUnidadMedida);
        }

        public List<string> ListarUnidad()
        {
            return CD_UnidadMedida.ObtenerUnidad();
        }
    }
}
