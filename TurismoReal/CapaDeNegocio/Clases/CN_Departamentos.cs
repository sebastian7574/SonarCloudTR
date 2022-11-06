using CapaDeDatos.Clases;
using CapaDeEntidad.Clases;
using System.Data;

namespace CapaDeNegocio.Clases
{
    public class CN_Departamentos
    {
        private readonly CD_Departamentos objDatos = new CD_Departamentos();

        #region Consultar
        public CE_Departamentos Consulta(int idDepartamento)
        {
            return objDatos.CD_Consulta(idDepartamento);
        }
        #endregion

        #region Insertar   

        public void Insertar(CE_Departamentos Departamentos)
        {
            objDatos.CD_Insertar(Departamentos);
        }

        #endregion

        #region Actualizar Datos   

        public void ActualizarDatos(CE_Departamentos Departamentos)
        {
            objDatos.CD_ActualizarDatos(Departamentos);
        }

        #endregion

        #region Mantencion   

        public void Mantencion(CE_Departamentos Departamentos)
        {
            objDatos.CD_Mantencion(Departamentos);
        }
        #region Cargar mantencion

        public DataTable CargarMantencion(int idDepartamento)
        {
            return objDatos.CargarMantencion(idDepartamento);
        }

        #endregion

        #endregion


        #region CARGAR DEPTOS A LA VISTA

        public DataTable CargarDeptos()
        {
            return objDatos.CargarDepartamentos();
        }

        #endregion

        #region BUSCAR DEPARTAMENTOS

        public DataTable BuscarDepto(string buscarDepto)
        {
            return objDatos.BuscarDepto(buscarDepto);
        }

        #endregion

        #region FILTRAR REGION

        public DataTable Filtro(string filtro)
        {
            return objDatos.Filtro(filtro);
        }

        #endregion


    }
}