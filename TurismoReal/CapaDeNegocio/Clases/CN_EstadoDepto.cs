using CapaDeDatos.Clases;
using CapaDeEntidad.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio.Clases
{
    public class CN_EstadoDepto
    {
        CD_EstadoDepto CD_EstadoDepto = new CD_EstadoDepto();

        public int IdEstadoDepto(string EstadoDepto)
        {
            return CD_EstadoDepto.IdEstadoDepto(EstadoDepto);
        }

        public CE_EstadoDepto NombreEstado(int IdEstadoDepto)
        {
            return CD_EstadoDepto.NombreEstado(IdEstadoDepto);
        }

        public List<string> ListarEstado()
        {
            return CD_EstadoDepto.ObtenerEstado();
        }
    }
}
