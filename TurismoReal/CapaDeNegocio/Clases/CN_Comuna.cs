using CapaDeDatos.Clases;
using CapaDeEntidad.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio.Clases
{
    public class CN_Comuna
    {
        CD_Comuna CD_Comuna = new CD_Comuna();

        public CE_Comuna NombreComuna(int IdComuna)
        {
            return CD_Comuna.NombreComuna(IdComuna);
        }

        public int IdComuna(string comuna)
        {
            return CD_Comuna.IdComuna(comuna);
        }

        //listar
        public DataTable ListarComunas(int idRegion)
        {
            return CD_Comuna.listarComunas(idRegion);
        }

        public DataTable MostrarComuna(int idComuna)
        {
            return CD_Comuna.MostrarComuna(idComuna);
        }

        public DataTable MostrarRegion(int idComuna)
        {
            return CD_Comuna.MostrarRegion(idComuna);
        }
    }
}
