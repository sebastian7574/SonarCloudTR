using CapaDeEntidad.Clases;
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
    /// Lógica de interacción para CheckInOut.xaml
    /// </summary>
    public partial class CheckInOut : UserControl
    {
        readonly CN_Reservas objeto_CN_Reservas = new CN_Reservas();
        readonly CE_Reservas objeto_CE_Reservas = new CE_Reservas();

        readonly CN_Boletas objeto_CN_Boletas = new CN_Boletas();
        readonly CN_Departamentos objeto_CN_Departamentos = new CN_Departamentos();
        readonly CN_Usuarios objeto_CN_Usuarios = new CN_Usuarios();

        public CheckInOut()
        {
            InitializeComponent();
            CargarDatos();
        }

        #region CARGAR RESERVAS
        void CargarDatos()
        {
            GridDatos.ItemsSource = objeto_CN_Reservas.CargarReservas().DefaultView;
        }
        #endregion

        private void IN_click(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDin ventana = new CRUDin();
            ventana.idReserva = id;
            ventana.Consultar();
            var a = objeto_CN_Reservas.Consulta(id);
            FrameCheckINOUT.Content = ventana;
            ventana.Titulo.Text = "CHECK IN Reserva N°" + id;
            ventana.idDepartamento = a.IdDepartamento;
            
        }

        private void ContratarServicio(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            ListadoServicios ventana = new ListadoServicios();
            FrameCheckINOUT.Content = ventana;

            ventana.idReserva = id;
            var a = objeto_CN_Reservas.Consulta(id);
            ventana.idUsuario = a.IdUsuario;
        }

        private void OUT_click(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDout ventana = new CRUDout();
            ventana.idReserva = id;
            ventana.Consultar();
            FrameCheckINOUT.Content = ventana;
            ventana.Titulo.Text = "CHECK OUT Reserva N°" + id;
        }

        #region FUNCION BUSCAR
        #region Limpiar
        public void LimpiarData()
        {
            tbBuscar.Clear();
            tbRut.Clear();
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
                    tbBuscar.Focus();
                    tbBuscar.Clear();
                    return;
                }
                else
                {
                    GridDatos.ItemsSource = objeto_CN_Reservas.BuscarN(tbBuscar.Text).DefaultView;
                    LimpiarData();
                    if (GridDatos.Items.Count == 0)
                    {
                        MessageBox.Show("No se encontraron resultados", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarDatos();
                    }
                }

            }
            else if (tbRut.Text != "")
            {

                if (tbRut.Text.Length < 9)
                {
                    MessageBox.Show("Para buscar Pasaporte/Rut se deben ingresar 9 caracteres\nsin guiones ni puntos según el tipo de identificación", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbRut.Clear();
                    tbRut.Focus();
                    return;
                }
                else if (tbRut.Text.Length > 9)
                {
                    MessageBox.Show("Por favor, no ingrese más de 9 caracteres", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbRut.Clear();
                    tbRut.Focus();
                    return;
                }
                else
                {
                    GridDatos.ItemsSource = objeto_CN_Reservas.BuscarR(tbRut.Text).DefaultView;
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

        private void Acompañantes(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            Acompañantes ventana = new Acompañantes();
            FrameCheckINOUT.Content = ventana;

            ventana.idReserva = id;
            ventana.Titulo.Text = "Listado de Acompañantes Reserva N° " + id;
        }
    }
}