using CapaDeNegocio.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para CheckList.xaml
    /// </summary>
    public partial class CheckList : UserControl
    {
        readonly CN_Departamentos objeto_CN_Departamentos = new CN_Departamentos();

        public CheckList()
        {
            InitializeComponent();
            CargarDatos();
        }

        #region CARGAR Departamentos
        void CargarDatos()
        {
            GridDatos.ItemsSource = objeto_CN_Departamentos.CargarDeptos().DefaultView;
        }
        #endregion

        #region VER INVENTARIO SEGÚN DEPTO
        private void Consultar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDchecklist ventana = new CRUDchecklist();
            ventana.idDepartamento = id;
            FrameInventario.Content = ventana;
            ventana.tbIDdepto.Text = "" + id;
            ventana.Titulo.Text = "Lista de inmuebles Depto. N°" + id;
        }

        #endregion  


        #region FUNCION BUSCAR
        #region Limpiar
        public void LimpiarData()
        {
            tbBuscar.Clear();
        }

        #endregion
        private void Ver(object sender, RoutedEventArgs e)
        {
            if (tbBuscar.Text != "")
            {
                if (tbBuscar.Text.Length > 25)
                {
                    MessageBox.Show("Por favor, no ingrese tantos caracteres", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbBuscar.Clear();
                    return;
                }
                else
                {
                    GridDatos.ItemsSource = objeto_CN_Departamentos.BuscarDepto(tbBuscar.Text).DefaultView;
                    LimpiarData();
                    if (GridDatos.Items.Count == 0)
                    {
                        MessageBox.Show("No se encontraron resultados", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarDatos();
                    }
                }

            }
            else
            {
                MessageBox.Show("Ingrese una dirección para buscar", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                CargarDatos();
            }
        }
            #endregion


        }
}
