using CapaDeEntidad.Clases;
using CapaDeNegocio.Clases;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace TurismoReal.Vistas.VistasFuncionario
{
    /// <summary>
    /// Lógica de interacción para CRUDout.xaml
    /// </summary>
    public partial class CRUDout : Page
    {
        readonly CN_Reservas objeto_CN_Reservas = new CN_Reservas();
        readonly CE_Reservas objeto_CE_Reservas = new CE_Reservas();

        readonly CN_Usuarios objeto_CN_Usuarios = new CN_Usuarios();
        readonly CN_Boletas objeto_CN_Boletas = new CN_Boletas();

        public CRUDout()
        {
            InitializeComponent();
        }
        public int idReserva;

        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new CheckInOut();
        }

        #region Consultar
        public void Consultar()
        {
            var a = objeto_CN_Reservas.Consulta(idReserva);
            var u = objeto_CN_Usuarios.Consulta(a.IdUsuario);

            
            tbCliente.Text = u.Nombres.ToString() + " " + u.Apellidos.ToString();
            tbRut.Text = u.Identificacion.ToString();
            cFechaDesde.Text = a.FechaDesde.ToString();
            cFechaHasta.Text = a.FechaHasta.ToString();
            cbEstadoReserva.Text = a.EstadoRerserva.ToString();
            tbPrecioNoche.Text = a.PrecioNocheReserva.ToString("0,0", CultureInfo.InvariantCulture);
            tbSaldo.Text = a.Saldo.ToString("0,0", CultureInfo.InvariantCulture);
            cFechaIngreso.Text = a.CheckIN.ToString();


            cFechaSalida.IsEnabled = false;
            cFechaSalida.Text = a.CheckOUT.ToString();
        }
        #endregion

        private void Crear(object sender, RoutedEventArgs e)
        {

            if (cFechaSalida.Text != "")
            {
                objeto_CE_Reservas.IdReserva = idReserva;
                objeto_CE_Reservas.CheckOUT = DateTime.Parse(cFechaSalida.Text);

                objeto_CN_Reservas.ActualizarOUT(objeto_CE_Reservas);
                MessageBox.Show("Se ingreso exitosamente!!", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnCrear.IsEnabled = false;
                cFechaSalida.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("La fecha de salida esta vacía", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Pagar(object sender, RoutedEventArgs e)
        {
            Pagos ventana = new Pagos();
            FramePago.Content = ventana;
            ventana.idReserva = idReserva;
            var a = objeto_CN_Reservas.Consulta(idReserva);
            var u = objeto_CN_Usuarios.Consulta(a.IdUsuario);
            var b = objeto_CN_Boletas.Ver(idReserva);

            ventana.BtnGuardar.Visibility = Visibility.Visible;

            ventana.idUsuario = u.IdUsuario;
            ventana.tbCliente.Text = u.Nombres.ToString() + " " + u.Apellidos.ToString();
            ventana.tbRut.Text = u.Identificacion.ToString();

            ventana.txtValor.Text = "Valor multa";
            ventana.chkMulta.IsChecked = true;

            ventana.tbCantidad.Text = "1";
            ventana.tbCantidad.Visibility = Visibility.Hidden;
            ventana.txtCantidad.Visibility = Visibility.Hidden;

            ventana.cFechaPago.IsEnabled = false;
            ventana.cFechaPago.Text = b.Fecha.ToString();

            ventana.Titulo.Text = "Cobro de Multas Reserva #" + idReserva;
        }
    }
}
