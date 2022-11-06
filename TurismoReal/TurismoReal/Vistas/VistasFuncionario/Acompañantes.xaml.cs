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

namespace TurismoReal.Vistas.VistasFuncionario
{
    /// <summary>
    /// Lógica de interacción para Acompañantes.xaml
    /// </summary>
    public partial class Acompañantes : UserControl
    {
        readonly CN_Reservas objeto_CN_Reserva = new CN_Reservas();
        readonly CN_Acompanantes objeto_CN_Acompanantes = new CN_Acompanantes();

        public Acompañantes()
        {
            InitializeComponent();
            
        }
        public int idReserva;

        #region CARGAR Servicios
        void CargarDatos()
        {
            GridDatos.ItemsSource = objeto_CN_Reserva.CargarA(idReserva).DefaultView;
        }
        #endregion


        #region FUNCION BUSCAR
        #region Limpiar
        public void LimpiarData()
        {
            tbBuscar.Clear();
            tbBuscarRut.Clear();
        }

        #endregion
        private void Ver(object sender, RoutedEventArgs e)
        {
            if (tbBuscar.Text != "")
            {
                if (Regex.IsMatch(tbBuscar.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]") == false)
                {
                    MessageBox.Show("Para buscar por Nombre o Apellido\nsolo se deben ingresar letras!\n(evite espacios)", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbBuscar.Clear();
                    tbBuscar.Focus();
                    return;
                }
                else if (tbBuscar.Text.Length > 25)
                {
                    MessageBox.Show("Por favor, no ingrese tantas letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbBuscar.Clear();
                    tbBuscar.Focus();
                    return;
                }
                else
                {
                    GridDatos.ItemsSource = objeto_CN_Acompanantes.Buscar(tbBuscar.Text).DefaultView;
                    LimpiarData(); 
                    if (GridDatos.Items.Count == 0)
                    {
                        MessageBox.Show("No se encontraron resultados", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarDatos();
                    }
                }
                
            }
            else if(tbBuscarRut.Text != "")
            {
                
                if (tbBuscarRut.Text.Length < 9)
                {
                    MessageBox.Show("Para buscar Pasaporte/Rut se deben ingresar 9 caracteres\nsin guiones ni puntos según el tipo de identificación", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbBuscarRut.Clear();
                    tbBuscarRut.Focus();
                    return;
                }
                else if (tbBuscarRut.Text.Length > 9)
                {
                    MessageBox.Show("Por favor, no ingrese más de 9 caracteres", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbBuscarRut.Clear();
                    tbBuscarRut.Focus();
                    return;
                }
                else
                {
                    GridDatos.ItemsSource = objeto_CN_Acompanantes.BuscarRut(tbBuscarRut.Text).DefaultView;
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

        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new CheckInOut();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }
    }
}
