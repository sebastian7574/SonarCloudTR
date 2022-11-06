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
    public class CN_TipoGasto
    {
        CD_TipoGasto CD_TipoGasto = new CD_TipoGasto();
        private readonly CD_TipoGasto objDatos = new CD_TipoGasto();

        #region Insertar   

        public void Insertar(CE_TipoGasto TipoGasto)
        {
            objDatos.CD_Insertar(TipoGasto);
        }

        #endregion

        #region Consultar
        public CE_TipoGasto Consulta(int idTipoGasto)
        {
            return objDatos.CD_Consulta(idTipoGasto);
        }
        #endregion

        #region Actualizar Datos   

        public void ActualizarDatos(CE_TipoGasto TipoGasto)
        {
            objDatos.CD_ActualizarDatos(TipoGasto);
        }

        #endregion

        #region Eliminar   

        public void Eliminar(CE_TipoGasto TipoGasto)
        {
            objDatos.CD_Eliminar(TipoGasto);
        }

        #endregion

        public int IdTipoGasto(string TipoGasto)
        {
            return CD_TipoGasto.IdTipoGasto(TipoGasto);
        }

        public CE_TipoGasto NombreTipoGasto(int IdTipoGasto)
        {
            return CD_TipoGasto.NombreTipoGasto(IdTipoGasto);
        }

        public List<string> ListarTipoGasto()
        {
            return CD_TipoGasto.ObtenerTipoGasto();
        }

        #region CARGAR ARTEFACTOS AL GRID

        public DataTable CargarTipoGasto()
        {
            return objDatos.CargarTipoGastos();
        }

        #endregion
    }
}

