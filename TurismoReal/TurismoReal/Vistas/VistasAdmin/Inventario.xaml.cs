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

namespace TurismoReal.Vistas.VistasAdmin
{
    /// <summary>
    /// Lógica de interacción para Inventario.xaml
    /// </summary>
    public partial class Inventario : UserControl
    {
        readonly CN_Departamentos objeto_CN_Departamentos = new CN_Departamentos();

        public Inventario()
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

        #region ARTEFACTOS
        private void BtnAgregarArtefacto_Click(object sender, RoutedEventArgs e)
        {
            CRUDartefacto ventana = new CRUDartefacto();
            FrameInventario.Content = ventana;
            ventana.BtnActualizar.IsEnabled = false;
        }
        #endregion

        #region INVENTARIO
        private void Consultar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDinventario ventana = new CRUDinventario();
            ventana.idDepartamento = id;
            FrameInventario.Content = ventana;
            ventana.BtnActualizar.IsEnabled = false;
            ventana.tbIDdepto.Text = ""+id;
            ventana.Titulo.Text = "Inventario Depto. N°"+id;
        }
        #endregion
        #region FUNCION BUSCAR
        #region Limpiar
        public void LimpiarData()
        {
            tbBuscar.Clear();
        }

        #endregion
        public bool IsAlphaNumeric(string texto)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-ZñÑáéíóúÁÉÍÓÚ -Z0-9]");
            return !objAlphaNumericPattern.IsMatch(texto);
        }


        private void Ver(object sender, RoutedEventArgs e)
        {
            if (tbBuscar.Text != "")
            {
                if (tbBuscar.Text.Length > 30)
                {
                    MessageBox.Show("La dirección es demasiado extensa!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbBuscar.Focus();
                    LimpiarData();
                    return;
                }
                else if (IsAlphaNumeric(tbBuscar.Text.ToString()) == false)
                {
                    MessageBox.Show("Para buscar dirección solo ingrese letras y números", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbBuscar.Clear();
                    tbBuscar.Focus();
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
                MessageBox.Show("Se deben ingresar datos para buscar", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                CargarDatos();
            }
        }
        #endregion
    }
}
