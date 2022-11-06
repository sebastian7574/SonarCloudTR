using CapaDeDatos.Clases;
using CapaDeEntidad.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio.Clases
{
    public class CN_Reportes
    {
        private readonly CD_Reportes objDatos = new CD_Reportes();

        #region Insertar   

        public void Insertar(CE_Reportes Reportes)
        {
            objDatos.CD_Insertar(Reportes);
        }

        public void InsertarR(CE_Reportes Reportes)
        {
            objDatos.CD_InsertarR(Reportes);
        }

        #endregion

        

        #region Consultar
        public CE_Reportes Consulta()
        {
            return objDatos.CD_Consulta();
        }
        #endregion
    }
}
