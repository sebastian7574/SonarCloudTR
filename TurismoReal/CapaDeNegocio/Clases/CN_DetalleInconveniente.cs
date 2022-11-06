using CapaDeDatos.Clases;
using CapaDeEntidad.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio.Clases
{
    public class CN_DetalleInconveniente
    {
        CD_DetalleInconveniente CD_DetalleInconveniente = new CD_DetalleInconveniente();
        private readonly CD_DetalleInconveniente objDatos = new CD_DetalleInconveniente();

        #region Insertar   

        public void Insertar(CE_DetalleInconveniente Multas)
        {
            objDatos.CD_Insertar(Multas);
        }

        #endregion
    }
}
