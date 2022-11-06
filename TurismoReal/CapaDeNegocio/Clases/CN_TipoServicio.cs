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
    public class CN_TipoServicio
    {
        CD_TipoServicio CD_TipoServicio = new CD_TipoServicio();

        private readonly CD_TipoServicio objDatos = new CD_TipoServicio();

        #region Insertar   

        public void Insertar(CE_TipoServicio TipoServicios)
        {
            objDatos.CD_Insertar(TipoServicios);
        }

        #endregion

        #region Consultar
        public CE_TipoServicio Consulta(int idTipoServicio)
        {
            return objDatos.CD_Consulta(idTipoServicio);
        }
        #endregion

        #region Actualizar Datos   

        public void ActualizarDatos(CE_TipoServicio TipoServicios)
        {
            objDatos.CD_ActualizarDatos(TipoServicios);
        }

        #endregion

        #region Eliminar   

        public void Eliminar(CE_TipoServicio TipoServicios)
        {
            objDatos.CD_Eliminar(TipoServicios);
        }

        #endregion



        #region CARGAR ARTEFACTOS AL GRID

        public DataTable CargarTipoServicio()
        {
            return objDatos.CargarTipoServicio();
        }

        #endregion

        public int IdTipoServicio(string Descripcion)
        {
            return CD_TipoServicio.IdTipoServicio(Descripcion);
        }

        public CE_TipoServicio NombreTipoServicio(int IdTipoServicio)
        {
            return CD_TipoServicio.NombreTipoServicio(IdTipoServicio);
        }

        public List<string> ListarTipoServicio()
        {
            return CD_TipoServicio.ObtenerTipoServicio();
        }
    }
}