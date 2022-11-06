using CapaDeEntidad.Clases;
using CapaDeNegocio.Clases;
using Microsoft.Win32;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

using iTextSharp.text;
using iTextSharp.tool.xml;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Documents;
using System.Windows.Forms;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;
using System.Linq;
using System.Collections.Generic;
using Button = System.Windows.Controls.Button;
using System.Windows.Input;
using System.Globalization;

namespace TurismoReal.Vistas.VistasFuncionario
{
    /// <summary>
    /// Lógica de interacción para Pagos.xaml
    /// </summary>
    public partial class Pagos : UserControl
    {
        readonly CN_Boletas objeto_CN_Boletas = new CN_Boletas();
        readonly CE_Boletas objeto_CE_Boletas = new CE_Boletas();

        readonly CN_DetalleInconveniente objeto_CN_Multa = new CN_DetalleInconveniente();
        readonly CE_DetalleInconveniente objeto_CE_Multa = new CE_DetalleInconveniente();

        readonly CN_DetalleServicio objeto_CN_DServicio = new CN_DetalleServicio();
        readonly CE_DetalleServicio objeto_CE_DServicio = new CE_DetalleServicio();

        public Pagos()
        {
            InitializeComponent();
        }

        #region CARGAR BOLETAS DEL CLIENTE
        void CargarDatos()
        {
            GridDatos.ItemsSource = objeto_CN_Boletas.CargarPorReserva(idReserva).DefaultView;
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
            Mostrar(); CargarCombobox();
        }
        #endregion

        public int idReserva;
        public int idUsuario;
        public int idServicio;

        #region ValidarCamposVacios
        public bool CamposLlenos()
        {
            if (cbMedioPago.Text == ""
                || cbBanco.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region REGRESAR
        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new CheckInOut();
        }
        #endregion

        #region CREAR
        private void Crear(object sender, RoutedEventArgs e)
        {
            if (cbMedioPago.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione un medio de pago", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(cbBanco.SelectedIndex == -1)
            {
                MessageBox.Show("Porfavor seleccione un banco.\nSi el medio de pago es efectivo, indique N/A", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            #region PAGOS NORMALES
            if (chkMulta.IsChecked == false)
            {
                #region CON SERVICIO
                if (idServicio != 0)
                {
                    int valor = int.Parse(tbValorUnitario.Text);
                    int cantidad = int.Parse("0" + tbCantidad.Text);
                    int total = cantidad * valor;

                    objeto_CE_DServicio.Fecha = DateTime.Now;
                    objeto_CE_DServicio.MontoTotal = total;
                    objeto_CE_DServicio.IdServicio = idServicio;
                    objeto_CE_DServicio.IdUsuario = idUsuario;

                    objeto_CN_DServicio.Insertar(objeto_CE_DServicio);

                    if (CamposLlenos() == true)
                    {
                        string comprobante = "C-" + DateTime.Now.ToString("HHmmssddMMyyyy") + "-0" + idReserva;
                        int efectivo = int.Parse("0" + tbEfectivo.Text);
                        int vuelto = efectivo - total;

                        objeto_CE_Boletas.MedioDePago = cbMedioPago.Text;
                        objeto_CE_Boletas.Fecha = DateTime.Now;
                        objeto_CE_Boletas.Banco = cbBanco.Text;
                        objeto_CE_Boletas.Comprobante = comprobante;
                        objeto_CE_Boletas.Monto = total;
                        if (cbMedioPago.Text == "Efectivo")
                        {
                            objeto_CE_Boletas.Efectivo = int.Parse(tbEfectivo.Text);
                            objeto_CE_Boletas.Vuelto = int.Parse(vuelto.ToString());
                        }
                        else if (cbMedioPago.Text == "Credito" || cbMedioPago.Text == "Debito")
                        {
                            objeto_CE_Boletas.Efectivo = 0;
                            objeto_CE_Boletas.Vuelto = 0;
                        }
                        objeto_CE_Boletas.Descripcion = tbDescripcion.Text + " x " + tbCantidad.Text;
                        objeto_CE_Boletas.IdReserva = idReserva;

                        if (tbCantidad.Text == "")
                        {
                            MessageBox.Show("La cantidad no puede quedar en blanco", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                            tbCantidad.Focus();
                        }
                        else if (int.Parse("0" + tbCantidad.Text) == 0)
                        {
                            MessageBox.Show("La cantidad no puede ser 0", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                            tbCantidad.Focus();
                        }
                        else if (cbMedioPago.Text == "Efectivo")
                        {
                            if (int.Parse("0" + tbEfectivo.Text) < int.Parse(tbMonto.Text))
                            {
                                MessageBox.Show("No se puede pagar menos de $" + tbMonto.Text + " en efectivo", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                                tbEfectivo.Focus();
                            }
                            else
                            {
                                if (CamposLlenos() == true)
                                {
                                    objeto_CN_Boletas.InsertarDS(objeto_CE_Boletas);

                                    Imprimir(comprobante, vuelto, total);
                                    MessageBox.Show("Se ingreso exitosamente!!", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);

                                    Content = new CheckInOut();
                                }
                                else
                                {
                                    MessageBox.Show("Asegurese de no dejar campos en blanco!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                        }
                        else if (cbMedioPago.Text != "Efectivo")
                        {
                            if (CamposLlenos() == true)
                            {
                                objeto_CN_Boletas.InsertarDS(objeto_CE_Boletas);

                                Imprimir(comprobante, vuelto, total);
                                MessageBox.Show("Se ingreso exitosamente!!", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);

                                Content = new CheckInOut();
                            }
                            else
                            {
                                MessageBox.Show("Asegurese de que no queden campos vacíos!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }

                        }
                    }
                }
                #endregion

                #region SIN SERVICIOS
                else if (idServicio == 0)
                {
                    if (CamposLlenos() == true)
                    {
                        string comprobante = "C-" + DateTime.Now.ToString("HHmmssddMMyyyy") + "-0" + idReserva;
                        int valor = int.Parse(tbValorUnitario.Text);
                        int cantidad = int.Parse("0" + tbCantidad.Text);
                        int total = cantidad * valor;
                        int efectivo = int.Parse("0" + tbEfectivo.Text);
                        int vuelto = efectivo - total;

                        objeto_CE_Boletas.MedioDePago = cbMedioPago.Text;
                        objeto_CE_Boletas.Fecha = DateTime.Now;
                        objeto_CE_Boletas.Banco = cbBanco.Text;
                        objeto_CE_Boletas.Comprobante = comprobante;
                        objeto_CE_Boletas.Monto = total;
                        if (cbMedioPago.Text == "Efectivo")
                        {
                            objeto_CE_Boletas.Efectivo = int.Parse(tbEfectivo.Text);
                            objeto_CE_Boletas.Vuelto = int.Parse(vuelto.ToString());
                        }
                        else
                        {
                            objeto_CE_Boletas.Efectivo = 0;
                            objeto_CE_Boletas.Vuelto = 0;
                        }
                        objeto_CE_Boletas.Descripcion = tbDescripcion.Text + " x " + tbCantidad.Text;
                        objeto_CE_Boletas.IdReserva = idReserva;

                        if (tbCantidad.Text == "")
                        {
                            MessageBox.Show("La cantidad no puede quedar en blanco", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                            tbCantidad.Focus();
                        }
                        else if (int.Parse("0" + tbCantidad.Text) == 0)
                        {
                            MessageBox.Show("La cantidad no puede ser 0", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                            tbCantidad.Focus();
                        }
                        else if (cbMedioPago.Text == "Efectivo")
                        {
                            if (int.Parse("0" + tbEfectivo.Text) < int.Parse(tbMonto.Text))
                            {
                                MessageBox.Show("No se puede pagar menos de $" + tbMonto.Text + " en efectivo", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                                tbEfectivo.Focus();
                            }
                            else
                            {
                                if (CamposLlenos() == true)
                                {
                                    objeto_CN_Boletas.Insertar(objeto_CE_Boletas);

                                    Imprimir(comprobante, vuelto, total);
                                    MessageBox.Show("Se ingreso exitosamente!!", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);

                                    Content = new CheckInOut();
                                }
                                else
                                {
                                    MessageBox.Show("Asegurese de no dejar campos en blanco!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                        }
                        else if (cbMedioPago.Text != "Efectivo")
                        {
                            if (CamposLlenos() == true)
                            {
                                objeto_CN_Boletas.Insertar(objeto_CE_Boletas);

                                Imprimir(comprobante, vuelto, total);
                                MessageBox.Show("Se ingreso exitosamente!!", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);

                                Content = new CheckInOut();
                            }
                            else
                            {
                                MessageBox.Show("Asegurese de que no queden campos vacíos!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }

                        }
                    }
                    }
            }
            #endregion  
            #endregion
            #region CREAR MULTA
            else
            {
                if (CamposLlenos() == true)
                {
                    string comprobante = "CM-" + DateTime.Now.ToString("HHmmssddMMyyyy") + "-0" + idReserva;
                    int valor = int.Parse("0"+tbValorUnitario.Text);
                    int cantidad = int.Parse("0" + tbCantidad.Text);
                    int total = cantidad * valor;
                    int efectivo = int.Parse("0" + tbEfectivo.Text);
                    int vuelto = efectivo - total;

                    //DETALLE MULTA
                    objeto_CE_Multa.Detalle = tbDescripcion.Text;
                    objeto_CE_Multa.MedioDePago = cbMedioPago.Text;
                    objeto_CE_Multa.Banco = cbBanco.Text;
                    objeto_CE_Multa.Comprobante = comprobante;
                    objeto_CE_Multa.Monto = int.Parse(tbMonto.Text);
                    objeto_CE_Multa.Efectivo = int.Parse(tbEfectivo.Text);
                    objeto_CE_Multa.Vuelto = vuelto.ToString();
                    objeto_CE_Multa.IdReserva = idReserva;

                    //BOLETA
                    objeto_CE_Boletas.MedioDePago = cbMedioPago.Text;
                    objeto_CE_Boletas.Fecha = DateTime.Now;
                    objeto_CE_Boletas.Banco = cbBanco.Text;
                    objeto_CE_Boletas.Comprobante = comprobante ;
                    objeto_CE_Boletas.Monto = total;
                    objeto_CE_Boletas.Efectivo = int.Parse(tbEfectivo.Text);
                    objeto_CE_Boletas.Vuelto = int.Parse(vuelto.ToString());
                    objeto_CE_Boletas.Descripcion = tbDescripcion.Text + " x " + tbCantidad.Text;
                    objeto_CE_Boletas.IdReserva = idReserva;
                    objeto_CE_Boletas.IdDetalleServicio = idServicio;

                    if (int.Parse("0" + tbValorUnitario.Text) == 0)
                    {
                        MessageBox.Show("El valor de la multa no puede ser 0", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                        tbValorUnitario.Clear();
                        tbValorUnitario.Focus();
                    }

                    else if (Regex.IsMatch(tbValorUnitario.Text, @"^[z0-9]+$") == false)
                    {
                        MessageBox.Show("Ingrese solo números en el valor de la multa", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                        tbValorUnitario.Clear();
                        tbValorUnitario.Focus();
                        return;
                    }
                    else if (tbValorUnitario.Text == "")
                    {
                        MessageBox.Show("El valor de la multa no puede quedar en blanco", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                        tbValorUnitario.Focus();
                    }
                    else if (tbValorUnitario.Text.Length < 4)
                    {
                        MessageBox.Show("Resvise el valor de la multa ingresado.\nNo hay montos a pagar menores a mil!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                        tbValorUnitario.Clear();
                        tbValorUnitario.Focus();
                        return;
                    }
                    else if (tbValorUnitario.Text.Length > 6)
                    {
                        MessageBox.Show("El valor de la multa es muy elevado\n(ingrese números desde el 1.000 hasta el 999.999)", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                        tbValorUnitario.Focus();
                        tbValorUnitario.Clear();
                        return;
                    }


                    else if (tbDescripcion.Text == "")
                    {
                        MessageBox.Show("La descripción no puede quedar en blanco", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                        tbDescripcion.Focus();
                    }
                    //valido que se ingresen solo letras
                    else if (Regex.IsMatch(tbDescripcion.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$") == false)
                    {
                        MessageBox.Show("La descripción solo puede tener letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                        tbDescripcion.Clear();
                        tbDescripcion.Focus();
                        return;
                    }

                    else if (cbMedioPago.Text == "Efectivo")
                    {
                        if (int.Parse("0" + tbEfectivo.Text) < int.Parse(tbMonto.Text))
                        {
                            MessageBox.Show("No se puede pagar menos de $" + tbMonto.Text + " en efectivo", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                            tbEfectivo.Focus();
                        }
                        else
                        {
                            if (CamposLlenos() == true)
                            {
                                objeto_CN_Boletas.Insertar(objeto_CE_Boletas);
                                objeto_CN_Multa.Insertar(objeto_CE_Multa);

                                Imprimir(comprobante, vuelto, total);
                                MessageBox.Show("Se ingreso una multa", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);

                                Content = new CheckInOut();
                            }
                            else
                            {
                                MessageBox.Show("Revise que no queden campos en blanco!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            
                        }
                    }

                    else if (cbMedioPago.Text != "Efectivo")
                    {
                        if (CamposLlenos() == true)
                        {
                            tbVuelto.Text = "0";
                            objeto_CN_Boletas.Insertar(objeto_CE_Boletas);
                            objeto_CN_Multa.Insertar(objeto_CE_Multa);
                            
                            Imprimir(comprobante, vuelto, total);
                            MessageBox.Show("Se ingreso una multa", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);

                            Content = new CheckInOut();
                        }
                        else
                        {
                            MessageBox.Show("Asegurese de que no queden campos vacíos", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        
                    }
                }
            }
            #endregion
        }

        #endregion

        #region CALCULOS
        private void Calculo(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
            if (tbCantidad.Text.Length > 2)
            {
                MessageBox.Show("Es demasiado grande la cantidad", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantidad.Clear();
                return;
            }
            else
            {
                int valor = int.Parse(tbValorUnitario.Text);
                int cantidad = int.Parse("0" + tbCantidad.Text);

                tbMonto.Text = "" + valor * cantidad;
            }

        }
        private void Verificar(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void Vuelto(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tbValorUnitario.Text.Length > 6)
            {
                MessageBox.Show("Es demasiado grande el valor, no contamos con precios tan elevados", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbValorUnitario.Clear();
                tbValorUnitario.Focus();
                return;
            }
            else if (tbValorUnitario.Text == "")
            {
                MessageBox.Show("El valor no puede quedar en blanco", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbValorUnitario.Clear();
                tbValorUnitario.Focus();
            }
            else if (int.Parse(tbValorUnitario.Text) == 0)
            {
                MessageBox.Show("El valor no puede ser 0", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbValorUnitario.Clear();
                tbValorUnitario.Focus();
            }
            else
            {
                int valor = int.Parse(tbValorUnitario.Text);
                int cantidad = int.Parse(tbCantidad.Text);
                int total = valor * cantidad;

                int efectivo = int.Parse("0" + tbEfectivo.Text);
                int vuelto = efectivo - total;

                if (tbEfectivo.Text == "0")
                {
                    tbVuelto.Text = "0";
                }
                else if (int.Parse("0" + tbEfectivo.Text) == int.Parse(tbMonto.Text))
                {
                    tbVuelto.Text = "0";
                }
                else if (int.Parse("0" + tbEfectivo.Text) < int.Parse(tbMonto.Text))
                {
                    tbVuelto.Text = "0";
                }
                else if (tbEfectivo.Text == "")
                {
                    tbVuelto.Text = "0";
                }
                else if (int.Parse("0" + tbEfectivo.Text) > int.Parse(tbMonto.Text))
                {
                    tbVuelto.Text = vuelto.ToString();
                }
            }
        }

        private void Multa(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tbValorUnitario.Text.Length > 6)
            {
                MessageBox.Show("Es demasiado grande el valor, no contamos con precios tan elevados", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbValorUnitario.Clear();
                tbValorUnitario.Focus();
                return;
            }
            else
            {
                int valor = int.Parse("0" + tbValorUnitario.Text);
                int cantidad = int.Parse(tbCantidad.Text);

                tbMonto.Text = "" + valor * cantidad;
            }
        }

        void Mostrar()
        {
            int valor = int.Parse("0"+tbValorUnitario.Text);
            int cantidad = int.Parse(tbCantidad.Text);
            int total = cantidad * valor;

            tbMonto.Text = total.ToString();
        }

        #endregion  

        #region IMPRIMIR
        void Imprimir (string comprobante, int vuelto, int total)
        {
            SaveFileDialog savefile = new SaveFileDialog
            {
                FileName = comprobante + ".pdf"
            };

            string Pagina = Properties.Resources.Ticket.ToString();
            Pagina = Pagina.Replace("@Ticket", comprobante);
            Pagina = Pagina.Replace("@Fecha", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            //
            Pagina = Pagina.Replace("@descripcion", tbDescripcion.Text.ToString());
            Pagina = Pagina.Replace("@cantidad", tbCantidad.Text.ToString());
            Pagina = Pagina.Replace("@valorU", int.Parse(tbValorUnitario.Text).ToString("0,0", CultureInfo.InvariantCulture));
            Pagina = Pagina.Replace("@totalItem", total.ToString("0,0", CultureInfo.InvariantCulture));
            //
            Pagina = Pagina.Replace("@TOTAL", total.ToString("0,0", CultureInfo.InvariantCulture));
            //
            Pagina = Pagina.Replace("@metodoPago", cbMedioPago.Text.ToString());
            if (cbMedioPago.Text.ToString() == "Efectivo")
            {
                Pagina = Pagina.Replace("@tipo", "Dinero pagado:");
                Pagina = Pagina.Replace("@efectivo", int.Parse(tbEfectivo.Text).ToString("0,0", CultureInfo.InvariantCulture));
            }
            else
            {
                Pagina = Pagina.Replace("@tipo", "Banco: ");
                Pagina = Pagina.Replace("@efectivo", cbBanco.Text.ToString()); ;
            }
            
            if (cbMedioPago.Text == "Credito" || cbMedioPago.Text == "Debito")
            {
                Pagina = Pagina.Replace("@cambio", 0.ToString());
            }
            else if (tbEfectivo.Text == "0")
            {
                Pagina = Pagina.Replace("@cambio", 0.ToString());
            }
            else
            {
                Pagina = Pagina.Replace("@cambio", vuelto.ToString("0,0", CultureInfo.InvariantCulture));
            }
            

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    Rectangle pagesize = new Rectangle(298, 520);
                    Document document = new Document(pagesize, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(document, stream);
                    document.Open();

                    using (StringReader sr= new StringReader(Pagina))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
                    }
                    document.Close();
                    stream.Close();
                }
            }
        }


        #endregion

        #region Consultar
        private void Consultar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            BtnGuardar.IsEnabled = false;
            cbMedioPago.IsEnabled = false;
            cbBanco.IsEnabled = false;
            tbDescripcion.IsEnabled = false;
            tbMonto.IsEnabled = false;
            tbEfectivo.IsEnabled = false;

            tbValorUnitario.Visibility = Visibility.Hidden;
            tbCantidad.Visibility = Visibility.Hidden;
            txtCantidad.Visibility = Visibility.Hidden;
            txtValor.Visibility = Visibility.Hidden;
            Placeholder.Visibility = Visibility.Hidden;

            var a = objeto_CN_Boletas.Consulta(id);

            tbID.Text = id.ToString();
            cbMedioPago.Text = a.MedioDePago.ToString();
            cFechaPago.Text = a.Fecha.ToString();
            cbBanco.Text = a.Banco.ToString();
            tbMonto.Text = a.Monto.ToString("0,0", CultureInfo.InvariantCulture);
            tbEfectivo.Text = a.Efectivo.ToString("0,0", CultureInfo.InvariantCulture);
            tbVuelto.Text = a.Vuelto.ToString("0,0", CultureInfo.InvariantCulture);
            tbDescripcion.Text = a.Descripcion.ToString();

        }
        #endregion

        #region DATOS COMBOBOX
        public void CargarCombobox()
        {
            List<CB_MedioPago> listMedio = new List<CB_MedioPago>
            {
                new CB_MedioPago { IdMedioPago = 1, MedioPago = "Efectivo" },
                new CB_MedioPago { IdMedioPago = 2, MedioPago = "Debito" },
                new CB_MedioPago { IdMedioPago = 3, MedioPago = "Credito" }
            };

            cbMedioPago.SelectedValuePath = "MedioPago";
            cbMedioPago.DisplayMemberPath = "MedioPago";
            cbMedioPago.ItemsSource = listMedio;

            List<CB_Banco> listBanco = new List<CB_Banco>
            {
                new CB_Banco { IdBanco = 1, BancoName = "N/A" },
                new CB_Banco { IdBanco = 2, BancoName = "CHILE" },
                new CB_Banco { IdBanco = 3, BancoName = "SCOTIABANK" },
                new CB_Banco { IdBanco = 4, BancoName = "CREDITO E INVERSIONES" },
                new CB_Banco { IdBanco = 5, BancoName = "BCI" },
                new CB_Banco { IdBanco = 6, BancoName = "CONSORCIO" },
                new CB_Banco { IdBanco = 7, BancoName = "SANTANDER" },
                new CB_Banco { IdBanco = 8, BancoName = "ITAÚ" },
                new CB_Banco { IdBanco = 9, BancoName = "FALABELLA" },
                new CB_Banco { IdBanco = 10, BancoName = "INTERNACIONAL" }
            };

            cbBanco.SelectedValuePath = "BancoName";
            cbBanco.DisplayMemberPath = "BancoName";
            cbBanco.ItemsSource = listBanco;

        }

        #endregion


    }
}
