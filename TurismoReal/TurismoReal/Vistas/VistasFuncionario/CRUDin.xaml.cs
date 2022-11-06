using CapaDeEntidad.Clases;
using CapaDeNegocio.Clases;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

using MessageBox = System.Windows.Forms.MessageBox;
using System.Windows.Media;
using DataGridCell = System.Windows.Controls.DataGridCell;
using System.Windows.Controls.Primitives;
using Org.BouncyCastle.Asn1.Cms;
using System.Linq;
using NPOI.SS.Formula.Functions;
using System.Data;
using System.Globalization;

namespace TurismoReal.Vistas.VistasFuncionario
{
    /// <summary>
    /// Lógica de interacción para CRUDin.xaml
    /// </summary>
    public partial class CRUDin : Page
    {

        readonly CN_Reservas objeto_CN_Reservas = new CN_Reservas();
        readonly CE_Reservas objeto_CE_Reservas = new CE_Reservas();

        readonly CN_Usuarios objeto_CN_Usuarios = new CN_Usuarios();
        readonly CN_Departamentos objeto_CN_Departamentos = new CN_Departamentos();
        readonly CN_Inventario objeto_CN_Inventario = new CN_Inventario();
        readonly CN_Artefactos objeto_CN_Artefactos = new CN_Artefactos();
        readonly CN_Boletas objeto_CN_Boletas = new CN_Boletas();

        readonly CN_Comuna objeto_CN_Comuna = new CN_Comuna();

        public CRUDin()
        {
            InitializeComponent();
        }

        public int idReserva;
        public int idDepartamento;

        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new CheckInOut();
        }

        #region CARGAR inventario
        void CargarDatos()
        {
            GridCheck.ItemsSource = objeto_CN_Inventario.CargarInventario(idDepartamento).DefaultView;
        }
        #endregion

        #region Consultar
        public void Consultar()
        {
            var a = objeto_CN_Reservas.Consulta(idReserva);
            var u = objeto_CN_Usuarios.Consulta(a.IdUsuario);

            tbCliente.Text = u.Nombres.ToString() + " " + u.Apellidos.ToString();
            tbRut.Text = u.Identificacion.ToString();

            tbIDdepto.Text = a.IdDepartamento.ToString();

            cFechaDesde.Text = a.FechaDesde.ToString();
            cFechaHasta.Text = a.FechaHasta.ToString();
            cbEstadoReserva.Text = a.EstadoRerserva.ToString();
            tbPrecioNoche.Text = a.PrecioNocheReserva.ToString("0,0", CultureInfo.InvariantCulture);
            tbSaldo.Text = a.Saldo.ToString("0,0", CultureInfo.InvariantCulture);


           cFechaIngreso.IsEnabled = false;
           cFechaIngreso.Text = a.CheckIN.ToString();
        }
        #endregion

        private void Crear(object sender, RoutedEventArgs e)
        {
            if (cFechaIngreso.Text != "")
            {
                objeto_CE_Reservas.IdReserva = idReserva;
                objeto_CE_Reservas.CheckIN = DateTime.Parse(cFechaIngreso.Text);

                objeto_CN_Reservas.ActualizarIN(objeto_CE_Reservas);
                MessageBox.Show("Se ingreso exitosamente!!", "INFORMACIÓN", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Information);
                BtnCrear.IsEnabled = false;
                cFechaIngreso.IsEnabled = false;

            }
            else
            {
                MessageBox.Show("La fecha de ingreso esta vacía", "ALERTA", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Warning);
            }
        }


        #region IMPRIMIR

        private void Descargar(object sender, RoutedEventArgs e)
        {
            string comprobante = "CheckIN-" + DateTime.Now.ToString("HHmmssddMMyyyy") + "-0" + idReserva;
            Imprimir(comprobante);
        }

        void Imprimir(string comprobante)
        {
            SaveFileDialog savefile = new SaveFileDialog
            {
                FileName = comprobante + ".pdf"
            };
            var a = objeto_CN_Reservas.Consulta(idReserva);
            var u = objeto_CN_Usuarios.Consulta(a.IdUsuario);
            var d = objeto_CN_Departamentos.Consulta(a.IdDepartamento);
            var c = objeto_CN_Comuna.NombreComuna(d.IdComuna);

            string Pagina = Properties.Resources.CheckIN.ToString();
            Pagina = Pagina.Replace("@Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            //TEXTO
            Pagina = Pagina.Replace("@cliente", u.Nombres.ToString() + " " + u.Apellidos.ToString());
            Pagina = Pagina.Replace("@identidad", tbRut.Text = u.Identificacion.ToString());
            Pagina = Pagina.Replace("@nombreDepto", d.Descripcion.ToString() + " de la comuna de " + c.Comuna.ToString());
            Pagina = Pagina.Replace("@direccion", d.Direccion.ToString());
            Pagina = Pagina.Replace("@fechaDesde", a.FechaDesde.ToString("dd/MM/yyyy"));
            Pagina = Pagina.Replace("@fechaTermino", a.FechaHasta.ToString("dd/MM/yyyy"));

            //PARTE DEL PAGO DE LA RESERVA
            var det = objeto_CN_Boletas.Detalle(idReserva);

            //VALIDACION EN CASO DE QUE NO TENGA EL REGISTRO
            if (det.MedioDePago == null || det.Monto == 0)
            {
                MessageBox.Show("Asegurese de realizar el \npago completo de la reserva!!", "INFORMACIÓN", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Information);
                return;
            }
            else
            {
                Pagina = Pagina.Replace("@medioPago", det.MedioDePago.ToString());
                Pagina = Pagina.Replace("@valorReserva", det.Monto.ToString("0,0", CultureInfo.InvariantCulture));
            }
            
            //LISTADO DE ARTEFACTOS
            string filas = string.Empty;
            foreach (DataRowView item in GridCheck.ItemsSource)
            {
                int id = Convert.ToInt32(item[0]);
                int idart = Convert.ToInt32(item[3]);

                var depto = objeto_CN_Reservas.Consulta(idReserva);
                var inv = objeto_CN_Inventario.CargarInventarioIN(depto.IdDepartamento);

                var art = objeto_CN_Artefactos.Consulta(idart);

                var descp = objeto_CN_Inventario.Consulta(id);

                string descripcion = art.Descripcion.ToString();
                int resultado = int.Parse(art.Valor.ToString());

                int cantidad = int.Parse(descp.Cantidad.ToString());

                filas += "<tr>";
                filas += "<td align=\"left\">" + descripcion + "</td>";
                filas += "<td align=\"center\">" + cantidad + "</td>";
                filas += "<td align=\"right\">" + resultado.ToString("0,0", CultureInfo.InvariantCulture) + "</td>";
                filas += "</tr>";
            }
            Pagina = Pagina.Replace("@listado", filas);



            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    int cant = GridCheck.Items.Count;
                    Rectangle pagesize = new Rectangle(590, 620 + (cant * 10));
                    Document document = new Document(pagesize, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(document, stream);

                    document.Open();

                    using (StringReader sr = new StringReader(Pagina))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
                    }
                    document.Close();
                    stream.Close();
                }
            }
        }


        #endregion

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

            ventana.txtValor.Text = "Valor a pagar";
            ventana.tbValorUnitario.IsEnabled = false;
            ventana.chkMulta.IsChecked = false;
            ventana.tbValorUnitario.Text = a.Saldo.ToString();

            ventana.tbCantidad.Text = "1";
            ventana.tbCantidad.Visibility = Visibility.Hidden;
            ventana.txtCantidad.Visibility = Visibility.Hidden;

            ventana.tbDescripcion.IsEnabled = false;
            ventana.tbDescripcion.Text = "Pago reserva completa";

            ventana.cFechaPago.IsEnabled = false;
            ventana.cFechaPago.Text = b.Fecha.ToString();

            ventana.Titulo.Text = "Pago de Reserva #" + idReserva;
        }

        private void Cargar(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }


    }
}
