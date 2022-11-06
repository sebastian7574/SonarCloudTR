using CapaDeDatos.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio.Clases
{
    public class CN_Acompanantes
    {
        private readonly CD_Acompanantes objDatos = new CD_Acompanantes();
        #region BUSCAR NOMBRE APELLIDO

        public DataTable Buscar(string buscar)
        {
            return objDatos.Buscar(buscar);
        }

        #endregion

        #region BUSCAR RUT

        public DataTable BuscarRut(string buscarRut)
        {
            return objDatos.BuscarRut(buscarRut);
        }

        #endregion
    }
}
