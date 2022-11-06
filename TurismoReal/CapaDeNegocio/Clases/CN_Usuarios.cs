using CapaDeDatos.Clases;
using CapaDeEntidad.Clases;
using System.Data;

namespace CapaDeNegocio.Clases
{
    public class CN_Usuarios
    {
        private readonly CD_Usuarios objDatos = new CD_Usuarios();
        
        #region Consultar
        public CE_Usuarios Consulta(int idUsuario)
        {
            return objDatos.CD_Consulta(idUsuario);
        }
        #endregion

        #region Insertar   

        public void Insertar(CE_Usuarios Usuarios)
        {
            objDatos.CD_Insertar(Usuarios);
        }

        #endregion

        #region Actualizar Datos   

        public void ActualizarDatos(CE_Usuarios Usuarios)
        {
            objDatos.CD_ActualizarDatos(Usuarios);
        }

        #endregion

        #region Actualizar Pass   

        public void ActualizarPass(CE_Usuarios Usuarios)
        {
            objDatos.CD_ActualizarPass(Usuarios);
        }

        #endregion

        #region CARGAR USUARIOS A LA VISTA

        public DataTable CargarUsuarios()
        {
            return objDatos.CargarUsuarios();
        }

        public DataTable CargarClientes()
        {
            return objDatos.CargarClientes();
        }

        #endregion

        #region BUSCAR USUARIOS

        public DataTable Buscar(string buscar)
        {
            return objDatos.Buscar(buscar);
        }

        #endregion

        #region FILTRAR TIPO DE  USUARIOS

        public DataTable Filtro(string filtro)
        {
            return objDatos.Filtro(filtro);
        }

        #endregion

        #region BUSCAR RUT

        public DataTable BuscarRut(string buscarRut)
        {
            return objDatos.BuscarRut(buscarRut);
        }

        #endregion

        #region LOGIN
        public CE_Usuarios LogIn(string usuario, string contra)
        {
            return objDatos.Login(usuario, contra);
        }
        #endregion


    }
}
