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
    /// Lógica de interacción para Departamentos.xaml
    /// </summary>
    public partial class Departamentos : UserControl
    {
        readonly CN_Departamentos objeto_CN_Departamentos = new CN_Departamentos();
        readonly CN_EstadoDepto objeto_CN_EstadoDepto = new CN_EstadoDepto();
        readonly CN_Region objeto_CN_Region = new CN_Region();
        readonly CN_Comuna objeto_CN_Comuna = new CN_Comuna();

        #region INICIAL
        public Departamentos()
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

        #region AGREGAR
        private void BtnAgregarDepto_Click(object sender, RoutedEventArgs e)
        {
            CRUDdepartamentos ventana = new CRUDdepartamentos();
            FrameDepartamentos.Content = ventana;
            ventana.BtnCrear.Visibility = Visibility.Visible;
        }
        #endregion

        #region CONSULTAR
        private void Consultar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDdepartamentos ventana = new CRUDdepartamentos();
            ventana.idDepartamento = id;
            ventana.Consultar();
            FrameDepartamentos.Content = ventana;
            ventana.Titulo.Text = "Consultar Departamento " + id;
            ventana.tbNombreDepto.IsEnabled = false;
            ventana.cbRegion.IsEnabled = false;
            ventana.cbComuna.IsEnabled = false;
            ventana.tbDireccion.IsEnabled = false;
            ventana.tbCantHabitaciones.IsEnabled = false;
            ventana.tbCantBanos.IsEnabled = false;
            ventana.tbPrecio.IsEnabled = false;
            ventana.cbEstadoDepto.IsEnabled = false;
            ventana.BtnGaleria.IsEnabled = false;
        }


        #endregion

        #region ACTUALIZAR
        private void Actualizar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDdepartamentos ventana = new CRUDdepartamentos();
            ventana.idDepartamento = id;
            ventana.Consultar();
            FrameDepartamentos.Content = ventana;
            ventana.Titulo.Text = "Actualizar Depto. " + id;
            ventana.tbNombreDepto.IsEnabled = true;
            ventana.cbRegion.IsEnabled = false;
            ventana.cbComuna.IsEnabled = false;
            ventana.tbDireccion.IsEnabled = true;
            ventana.tbCantHabitaciones.IsEnabled = true;
            ventana.tbCantBanos.IsEnabled = true;
            ventana.tbPrecio.IsEnabled = true;
            ventana.cbEstadoDepto.IsEnabled = true;
            ventana.BtnGaleria.IsEnabled = true;
            ventana.txtGaleria.Visibility = Visibility.Visible;
            ventana.BtnGaleria.Visibility = Visibility.Visible;
            ventana.txtMantenimiento.Visibility = Visibility.Visible;
            ventana.BtnMantencion.Visibility = Visibility.Visible;
            ventana.BtnActualizar.Visibility = Visibility.Visible;
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
            #endregion
        }
    }
}

