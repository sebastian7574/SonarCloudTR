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
    public class CN_Gastos
    {
        private readonly CD_Gastos objDatos = new CD_Gastos();

        #region Insertar   

        public void Insertar(CE_Gastos Gastos)
        {
            objDatos.CD_Insertar(Gastos);
        }

        #endregion

        #region Consultar
        public CE_Gastos Consulta(int idGastos)
        {
            return objDatos.CD_Consulta(idGastos);
        }
        #endregion

        #region Actualizar Datos   

        public void ActualizarDatos(CE_Gastos Gastos)
        {
            objDatos.CD_ActualizarDatos(Gastos);
        }

        #endregion

        #region Eliminar   

        public void Eliminar(CE_Gastos Gastos)
        {
            objDatos.CD_Eliminar(Gastos);
        }

        #endregion

        #region CARGAR ARTEFACTOS AL GRID

        public DataTable CargarGastos(int idDepartamento)
        {
            return objDatos.CargarGastos(idDepartamento);
        }

        #endregion
    }
}
