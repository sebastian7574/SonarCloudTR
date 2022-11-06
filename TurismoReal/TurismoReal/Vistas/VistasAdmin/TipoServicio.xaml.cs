using CapaDeEntidad.Clases;
using CapaDeNegocio.Clases;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para TipoServicio.xaml
    /// </summary>
    public partial class TipoServicio : UserControl
    {
        readonly CN_TipoServicio objeto_CN_TipoServicio = new CN_TipoServicio();
        readonly CE_TipoServicio objeto_CE_TipoServicio = new CE_TipoServicio();

        public TipoServicio()
        {
            InitializeComponent();
            CargarDatos();
            BtnActualizar.IsEnabled = false;
        }

        #region CARGAR IMAGENES
        void CargarDatos()
        {
            GridDatos.ItemsSource = objeto_CN_TipoServicio.CargarTipoServicio().DefaultView;
        }
        #endregion

        #region Regresar
        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new Servicios();
        }
        #endregion

        #region ValidarCamposVacios
        public bool CamposLlenos()
        {
            if (tbDescripcion.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region AGREGAR 
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
                if (tbDescripcion.Text.Length > 30)
                {
                    MessageBox.Show("La descripción es demasiado extensa", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
                else if (tbDescripcion.Text.Length < 3)
                {
                    MessageBox.Show("La descripción es muy corta", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
                //valido que se ingresen solo letras
                else if (Regex.IsMatch(tbDescripcion.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$") == false)
                {
                    MessageBox.Show("La descripción solo puede contener letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
            }
            #endregion

            if (CamposLlenos() == true)
            {
                objeto_CE_TipoServicio.TipoServicio = tbDescripcion.Text;

                objeto_CN_TipoServicio.Insertar(objeto_CE_TipoServicio);
                CargarDatos();
                MessageBox.Show("Se ha guardado correctamente", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarData();

            }
            else
            {
                MessageBox.Show("No hay descripción!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        #endregion

        #region CONSULTAR
        public int idTipoServicio;
        private void Consultar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            var a = objeto_CN_TipoServicio.Consulta(id);

            tbDescripcion.IsEnabled = false;
            BtnActualizar.IsEnabled = false;
            BtnCrear.IsEnabled = false;

            tbDescripcion.Text = a.TipoServicio.ToString();
        }
        #endregion

        #region ACTUALIZAR
        public void ActualizarC(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            tbDescripcion.IsEnabled = true;
            BtnCrear.IsEnabled = false;
            BtnActualizar.IsEnabled = true;

            var a = objeto_CN_TipoServicio.Consulta(id);

            tbID.Text = id.ToString();
            tbDescripcion.Text = a.TipoServicio.ToString();

        }
        private void Actualizar(object sender, RoutedEventArgs e)
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
                if (tbDescripcion.Text.Length > 30)
                {
                    MessageBox.Show("La descripción es demasiado extensa", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
                else if (tbDescripcion.Text.Length < 3)
                {
                    MessageBox.Show("La descripción es muy corta", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
                //valido que se ingresen solo letras
                else if (Regex.IsMatch(tbDescripcion.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$") == false)
                {
                    MessageBox.Show("La descripción solo puede contener letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
            }
            #endregion

            if (CamposLlenos() == true)
            {
                objeto_CE_TipoServicio.IdTipoServicio = int.Parse(tbID.Text);
                objeto_CE_TipoServicio.TipoServicio = tbDescripcion.Text;

                objeto_CN_TipoServicio.ActualizarDatos(objeto_CE_TipoServicio);
                CargarDatos();
                MessageBox.Show("Se actualizó exitosamente!!", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarData();
                BtnCrear.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("No se han ingresado datos", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region ELIMINAR
        private void Eliminar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            tbDescripcion.IsEnabled = false;
            if (MessageBox.Show("¿Está seguro de eliminar el tipo de servicio " + tbDescripcion.Text +"?", "Eliminar Tipo Servicio", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                objeto_CE_TipoServicio.IdTipoServicio = id;
                objeto_CN_TipoServicio.Eliminar(objeto_CE_TipoServicio);
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
            tbDescripcion.IsEnabled = true;
            BtnActualizar.IsEnabled = false;
            BtnCrear.IsEnabled = true;
        }
        private void Limpiar(object sender, RoutedEventArgs e)
        {
            LimpiarData();
        }
        #endregion

    }
}


