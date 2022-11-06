using CapaDeEntidad.Clases;
using CapaDeNegocio.Clases;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Lógica de interacción para CRUDgastos.xaml
    /// </summary>
    public partial class CRUDgastos : Page
    {
        readonly CN_TipoGasto objeto_CN_TipoGasto = new CN_TipoGasto();
        readonly CN_Gastos objeto_CN_Gastos = new CN_Gastos();
        readonly CE_Gastos objeto_CE_Gastos = new CE_Gastos();

        public CRUDgastos()
        {
            InitializeComponent();
            CargarA();
        }

        #region Cargar FK
        void CargarA()
        {
            List<string> tipogasto = objeto_CN_TipoGasto.ListarTipoGasto();
            for (int i = 0; i < tipogasto.Count; i++)
            {
                cbTipoGasto.Items.Add(tipogasto[i]);
            }
        }
        #endregion

        #region CARGAR inventario
        void CargarDatos()
        {
            GridDatos.ItemsSource = objeto_CN_Gastos.CargarGastos(idDepartamento).DefaultView;
        }
        #endregion

        #region Regresar
        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new Gastos();
        }
        #endregion

        #region ValidarCamposVacios
        public bool CamposLlenos()
        {
            if (tbIDdepto.Text == ""
                || tbDescripcion.Text == ""
                || tbMonto.Text == ""
                || cFecha.Text == ""
                || cbTipoGasto.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion


        public int idTipoGastos;
        public int idGastos;
        public int idDepartamento;
        #region Crear
        private void Crear(object sender, RoutedEventArgs e)
        {
            #region DESCRIPCIÓN
            if (tbDescripcion.Text == "")
            {
                MessageBox.Show("La descripción no puede quedar vacía", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbDescripcion.Focus();
                return;
            }
            else if (tbDescripcion.Text != "")
            {
                if (tbDescripcion.Text.Length > 35)
                {
                    MessageBox.Show("Es demasiado extensa la descripción", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
                else if (tbDescripcion.Text.Length < 3)
                {
                    MessageBox.Show("Es muy corta la descripción", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
                //valido que se ingresen solo letras
                else if (Regex.IsMatch(tbDescripcion.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$") == false)
                {
                    MessageBox.Show("Solo se pueden ingresar letras en la descripción", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
            }
            #endregion

            #region MONTO
            if (tbMonto.Text == "")
            {
                MessageBox.Show("El monto no puede quedar en blanco", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbMonto.Focus();
            }
            else if (int.Parse(tbMonto.Text) == 0)
            {
                MessageBox.Show("El monto no puede ser 0", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbMonto.Clear();
                tbMonto.Focus();
            }

            else if (Regex.IsMatch(tbMonto.Text, @"^[z0-9]+$") == false)
            {
                MessageBox.Show("Ingrese solo números en el monto", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbMonto.Clear();
                tbMonto.Focus();
                return;
            }
            else if (tbMonto.Text.Length >= 7)
            {
                MessageBox.Show("El monto es demasiado grande!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbMonto.Focus();
                tbMonto.Clear();
                return;
            }
            #endregion

            #region FECHA
            else if (cFecha.Text == "")
            {
                MessageBox.Show("Por favor, indique la fecha que se pago", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                cFecha.Focus();
            }
            #endregion

            #region TIPO GASTO
            else if (cbTipoGasto.Text == "")
            {
                MessageBox.Show("Indique el tipo de gasto!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            #endregion

            else if (CamposLlenos() == true)
            {
                try
                {
                    int tipogasto = objeto_CN_TipoGasto.IdTipoGasto(cbTipoGasto.Text);

                    objeto_CE_Gastos.Descripcion = tbDescripcion.Text;
                    objeto_CE_Gastos.Monto = int.Parse(tbMonto.Text);
                    objeto_CE_Gastos.FechaGastos = DateTime.Parse(cFecha.Text);
                    objeto_CE_Gastos.IdDepartamento = int.Parse(tbIDdepto.Text);
                    objeto_CE_Gastos.IdTipoGastos = tipogasto;

                    objeto_CN_Gastos.Insertar(objeto_CE_Gastos);
                    CargarDatos();
                    MessageBox.Show("Se registro exitosamente", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarData();
                }
                catch
                {
                    MessageBox.Show("No pueden quedar campos vacíos!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("No se pudo registrar el Gasto,\n revise los datos e intentelo denuevo", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion
        public void ActualizarC(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            tbIDdepto.IsEnabled = false;
            tbDescripcion.IsEnabled = true;
            tbMonto.IsEnabled = true;
            cFecha.IsEnabled = true;
            cbTipoGasto.IsEnabled = true;
            BtnCrear.IsEnabled = false;
            BtnActualizar.IsEnabled = true;

            var a = objeto_CN_Gastos.Consulta(id);
            var c = objeto_CN_TipoGasto.NombreTipoGasto(a.IdTipoGastos);

            tbID.Text = id.ToString();
            tbDescripcion.Text = a.Descripcion.ToString();
            tbMonto.Text = a.Monto.ToString();
            cFecha.Text = a.FechaGastos.ToString();
            cbTipoGasto.Text = c.TipoGasto.ToString();

        }

        #region Actualizar
        public void Actualizar(object sender, RoutedEventArgs e)
        {
            //Validaciones
            #region DESCRIPCIÓN
            if (tbDescripcion.Text == "")
            {
                MessageBox.Show("La descripción no puede quedar vacía", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbDescripcion.Focus();
                return;
            }
            else if (tbDescripcion.Text != "")
            {
                if (tbDescripcion.Text.Length > 35)
                {
                    MessageBox.Show("Es demasiado extensa la descripción", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
                else if (tbDescripcion.Text.Length < 3)
                {
                    MessageBox.Show("Es muy corta la descripción", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
                //valido que se ingresen solo letras
                else if (Regex.IsMatch(tbDescripcion.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$") == false)
                {
                    MessageBox.Show("Solo se pueden ingresar letras en la descripción", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
            }
            #endregion

            #region MONTO
            if (tbMonto.Text == "")
            {
                MessageBox.Show("El monto no puede quedar en blanco", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbMonto.Focus();
            }
            else if (int.Parse(tbMonto.Text) == 0)
            {
                MessageBox.Show("El monto no puede ser 0", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbMonto.Clear();
                tbMonto.Focus();
            }

            else if (Regex.IsMatch(tbMonto.Text, @"^[z0-9]+$") == false)
            {
                MessageBox.Show("Ingrese solo números en el monto", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbMonto.Clear();
                tbMonto.Focus();
                return;
            }
            else if (tbMonto.Text.Length >= 7)
            {
                MessageBox.Show("El monto es demasiado grande!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbMonto.Focus();
                tbMonto.Clear();
                return;
            }
            #endregion

            #region FECHA
            else if (cFecha.Text == "")
            {
                MessageBox.Show("Por favor, indique la fecha que se pago", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                cFecha.Focus();
            }
            #endregion

            #region TIPO GASTO
            else if (cbTipoGasto.Text == "")
            {
                MessageBox.Show("Indique el tipo de gasto!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            #endregion

            else if (CamposLlenos() == true)
            {
                int tipogasto = objeto_CN_TipoGasto.IdTipoGasto(cbTipoGasto.Text);

                objeto_CE_Gastos.IdGastos = int.Parse(tbID.Text);
                objeto_CE_Gastos.Descripcion = tbDescripcion.Text;
                objeto_CE_Gastos.Monto = int.Parse(tbMonto.Text);
                objeto_CE_Gastos.FechaGastos = DateTime.Parse(cFecha.Text);
                objeto_CE_Gastos.IdDepartamento = int.Parse(tbIDdepto.Text);
                objeto_CE_Gastos.IdTipoGastos = tipogasto;

                objeto_CN_Gastos.ActualizarDatos(objeto_CE_Gastos);

                CargarDatos();
                MessageBox.Show("Se actualizó exitosamente!!", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarData();
                BtnCrear.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Por favor, no dejar campos vacios", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region Consultar
        private void Consultar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            tbIDdepto.IsEnabled = false;
            tbDescripcion.IsEnabled = false;
            tbMonto.IsEnabled = false;
            cFecha.IsEnabled = false;
            cbTipoGasto.IsEnabled = false;
            BtnActualizar.IsEnabled = false;
            BtnCrear.IsEnabled = false;

            var a = objeto_CN_Gastos.Consulta(id);
            var c = objeto_CN_TipoGasto.NombreTipoGasto(a.IdTipoGastos);

            tbID.Text = id.ToString();
            tbDescripcion.Text = a.Descripcion.ToString();
            tbMonto.Text = a.Monto.ToString("0,0", CultureInfo.InvariantCulture);
            cFecha.Text = a.FechaGastos.ToString();
            cbTipoGasto.Text = c.TipoGasto.ToString();
        }
        #endregion

        #region Eliminar
        private void Eliminar(object sender, RoutedEventArgs e)
        {

            int id = (int)((Button)sender).CommandParameter;
            if (MessageBox.Show("¿Está seguro de eliminar el gasto?", "Eliminar Gasto", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                objeto_CE_Gastos.IdGastos = id;
                objeto_CN_Gastos.Eliminar(objeto_CE_Gastos);
                CargarDatos();
                LimpiarData();
            }
            else
            {
                CargarDatos();
                LimpiarData();
            }
        }
        #endregion


        #region Limpiar Campos

        public void LimpiarData()
        {
            tbDescripcion.Clear();
            tbMonto.Clear();
            tbID.Clear();
            cbTipoGasto.SelectedIndex = -1;
            cFecha.Text = "";
            cbTipoGasto.IsEnabled = true;
            BtnActualizar.IsEnabled = false;
            BtnCrear.IsEnabled = true;
        }
        private void Limpiar(object sender, RoutedEventArgs e)
        {
            LimpiarData();
        }
        #endregion

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }
    }
}