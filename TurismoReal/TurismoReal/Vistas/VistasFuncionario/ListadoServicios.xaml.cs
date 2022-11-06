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
    public partial class ListadoServicios : UserControl
    {
        readonly CN_Servicios objeto_CN_Servicios = new CN_Servicios();
        readonly CN_TipoServicio objeto_CN_TipoServicio = new CN_TipoServicio();
        readonly CN_Reservas objeto_CN_Reservas = new CN_Reservas();

        readonly CN_Boletas objeto_CN_Boletas = new CN_Boletas();
        readonly CN_Usuarios objeto_CN_Usuarios = new CN_Usuarios();

        public ListadoServicios()
        {
            InitializeComponent();
            CargarDatos();
        }


        #region CARGAR Servicios
        void CargarDatos()
        {
            GridDatos.ItemsSource = objeto_CN_Servicios.CargarListado().DefaultView;
        }
        #endregion
        public int idReserva;
        public int idServicio;
        public int idUsuario;
        private void ContratarServicio(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            Pagos ventana = new Pagos();
            FrameServicios.Content = ventana;
            ventana.idServicio = id;
            ventana.idReserva = idReserva;
            ventana.idUsuario = idUsuario;

            var a = objeto_CN_Reservas.Consulta(idReserva);
            var u = objeto_CN_Usuarios.Consulta(a.IdUsuario);
            var b = objeto_CN_Boletas.Ver(idReserva);
            var s = objeto_CN_Servicios.Consulta(id);
            var ds = objeto_CN_TipoServicio.Consulta(s.IdTipoServicio);

            ventana.tbCliente.Text = u.Nombres.ToString() + " " + u.Apellidos.ToString();
            ventana.tbRut.Text = u.Identificacion.ToString();
            ventana.tbDescripcion.IsEnabled = false;
            ventana.cFechaPago.IsEnabled = false;
            ventana.cFechaPago.Text = b.Fecha.ToString();
            ventana.tbMonto.IsEnabled = false;
            ventana.tbValorUnitario.IsEnabled = false;
            ventana.tbValorUnitario.Text = s.Precio.ToString();
            ventana.tbDescripcion.Text = ds.TipoServicio.ToString() + " " + s.Descripcion.ToString();
            ventana.Titulo.Text = "Pago de servicio Reserva #" + idReserva;
        }

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
                    LimpiarData();
                    tbBuscar.Focus();
                    return;
                }
                else if (Regex.IsMatch(tbBuscar.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]+$") == false)
                {
                    MessageBox.Show("La búsqueda se realiza solo con letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    LimpiarData();
                    tbBuscar.Focus();
                    return;
                }
                else
                {
                    GridDatos.ItemsSource = objeto_CN_Servicios.BuscarServDispo(tbBuscar.Text).DefaultView;
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
                MessageBox.Show("Ingrese una descripción para buscar", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion
    }
}

