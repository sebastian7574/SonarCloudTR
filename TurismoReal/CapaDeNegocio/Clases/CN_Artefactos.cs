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
    public class CN_Artefactos
    {
        CD_Artefactos CD_Artefactos = new CD_Artefactos();
        private readonly CD_Artefactos objDatos = new CD_Artefactos();

        #region Insertar   

        public void Insertar(CE_Artefactos Artefactos)
        {
            objDatos.CD_Insertar(Artefactos);
        }

        #endregion

        #region Consultar
        public CE_Artefactos Consulta(int idArtefactos)
        {
            return objDatos.CD_Consulta(idArtefactos);
        }
        #endregion

        #region Actualizar Datos   

        public void ActualizarDatos(CE_Artefactos Artefactos)
        {
            objDatos.CD_ActualizarDatos(Artefactos);
        }

        #endregion

        #region Eliminar   

        public void Eliminar(CE_Artefactos Artefactos)
        {
            objDatos.CD_Eliminar(Artefactos);
        }

        #endregion

        public int IdArtefacto(string Descripcion)
        {
            return CD_Artefactos.IdArtefacto(Descripcion);
        }

        public CE_Artefactos NombreArtefacto(int IdArtefacto)
        {
            return CD_Artefactos.NombreArtefacto(IdArtefacto);
        }

        public List<string> ListarArtefacto()
        {
            return CD_Artefactos.ObtenerArtefacto();
        }

        #region CARGAR ARTEFACTOS AL GRID

        public DataTable CargarArtefactos()
        {
            return objDatos.CargarArtefactos();
        }

        #endregion
    }
}
