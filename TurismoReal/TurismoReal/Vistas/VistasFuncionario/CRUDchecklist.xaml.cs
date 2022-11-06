using CapaDeEntidad.Clases;
using CapaDeNegocio.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TurismoReal.Vistas.VistasAdmin;

namespace TurismoReal.Vistas.VistasFuncionario
{
    /// <summary>
    /// Lógica de interacción para CRUDchecklist.xaml
    /// </summary>
    public partial class CRUDchecklist : Page
    {
        readonly CN_Artefactos objeto_CN_Artefactos = new CN_Artefactos();
        readonly CN_Inventario objeto_CN_Inventario = new CN_Inventario();
        readonly CE_Inventario objeto_CE_Inventario = new CE_Inventario();

        public CRUDchecklist()
        {
            InitializeComponent();

        }

        #region CARGAR inventario
        void CargarDatos()
        {
            GridCheck.ItemsSource = objeto_CN_Inventario.CargarInventario(idDepartamento).DefaultView;
        }
        #endregion

        #region Regresar
        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new CheckList();
        }
        #endregion

        public int idArtefactos;
        public int idInventario;
        public int idDepartamento;
        

        #region Consultar
        private void Consultar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            tbIDdepto.IsEnabled = false;

            var a = objeto_CN_Inventario.Consulta(id);
            var c = objeto_CN_Artefactos.NombreArtefacto(a.IdArtefactos);

        }
        #endregion


        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }
    }
}