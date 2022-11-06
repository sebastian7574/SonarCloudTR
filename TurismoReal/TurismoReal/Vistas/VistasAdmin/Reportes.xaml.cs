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

namespace TurismoReal.Vistas.VistasAdmin
{
    /// <summary>
    /// Lógica de interacción para Reportes.xaml
    /// </summary>
    public partial class Reportes : UserControl
    {
        readonly CN_Departamentos objeto_CN_Departamentos = new CN_Departamentos();
        readonly CN_EstadoDepto objeto_CN_EstadoDepto = new CN_EstadoDepto();
        readonly CN_Region objeto_CN_Region = new CN_Region();
        readonly CN_Comuna objeto_CN_Comuna = new CN_Comuna();

        #region INICIAL
        public Reportes()
        {
            InitializeComponent();
            CargarDatos();
        }
        #endregion

        #region CARGAR Departamentos
        void CargarDatos()
        {
            GridDatos.ItemsSource = objeto_CN_Departamentos.CargarDeptos().DefaultView;
        }
        #endregion

        #region CONSULTAR
        private void Consultar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDreportes ventana = new CRUDreportes();
            ventana.idDepartamento = id;
            FrameDepartamentos.Content = ventana;
            ventana.Titulo.Text = "Reporte departamento N° "+ id;
            ventana.BtnGenerar.Visibility = Visibility.Hidden;
            ventana.BtnCrear.Visibility = Visibility.Visible;

        }
        #endregion

        #region REPORTE GENERAL
        private void ReporteGeneral(object sender, RoutedEventArgs e)
        {
            if (cbFiltroRegion.Text != "")
            {
                string regionid = cbFiltroRegion.SelectedValue.ToString();
                int idRegion = Convert.ToInt32(regionid);
                CRUDreportes ventana = new CRUDreportes();
                ventana.idRegion = idRegion;
                FrameDepartamentos.Content = ventana;
                ventana.Titulo.Text = "Reporte General " + idRegion + " Region";
                ventana.txtDetalle.Text = "generar el reporte general de la zona " + idRegion;
                ventana.BtnCrear.Visibility = Visibility.Hidden;
                ventana.BtnGenerar.Visibility = Visibility.Visible;
            }
            
            else
            {
                MessageBox.Show("Para generar este reporte debe indicar que region desea\nen el filtro por region!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region FUNCION FILTRO POR REGION

        private void Regiones(object sender, RoutedEventArgs e)
        {
            CargarRegion();
        }

        public void CargarRegion()
        {
            cbFiltroRegion.DisplayMemberPath = "region";
            cbFiltroRegion.SelectedValuePath = "idRegion";
            cbFiltroRegion.DataContext = objeto_CN_Region.listaRegiones();
        }

        private void Region(object sender, SelectionChangedEventArgs e)
        {
            string regionid = cbFiltroRegion.SelectedValue.ToString();
            int idRegion = Convert.ToInt32(regionid);
            Deptos(idRegion);
        }

        private void Deptos(int idRegion)
        {
            GridDatos.ItemsSource = objeto_CN_Departamentos.Filtro(idRegion.ToString()).DefaultView;
            if (GridDatos.Items.Count == 0)
            {
                MessageBox.Show("No se encontraron resultados", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                CargarDatos();
            }
        }

        #endregion


    }
}
