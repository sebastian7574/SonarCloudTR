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
    public class CN_Region
    {
        CD_Region CD_Region = new CD_Region();
        private readonly CD_Region objDatos = new CD_Region();

        #region Consultar
        public CE_Region Consulta(int idRegion)
        {
            return objDatos.CD_Consulta(idRegion);
        }
        #endregion

        //listar
        public DataTable listaRegiones()
        {
            return CD_Region.listaRegion();
        }


    }
}

