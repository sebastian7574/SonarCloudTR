using CapaDeEntidad.Clases;
using CapaDeNegocio.Clases;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace TurismoReal.Vistas.VistasAdmin
{
    /// <summary>
    /// Lógica de interacción para Mantencion.xaml
    /// </summary>
    public partial class Mantencion : UserControl
    {
        readonly CN_Departamentos objeto_CN_Departamentos = new CN_Departamentos();
        readonly CE_Departamentos objeto_CE_Departamentos = new CE_Departamentos();
        public Mantencion()
        {
            InitializeComponent();
        }

        #region CARGAR IMAGENES
        void CargarDatos()
        {
            GridDatos.ItemsSource = objeto_CN_Departamentos.CargarMantencion(idDepartamento).DefaultView;
        }
        #endregion

        #region Regresar
        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new Departamentos();
        }
        #endregion

        #region ValidarCamposVacios
        public bool CamposLlenos()
        {
            if (tbIDdepto.Text == ""
                || cMantencionT.Text == ""
                || cMantencionI.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region MANTENCION
        public int idDepartamento;
        private void Crear(object sender, RoutedEventArgs e)
        {
            if (DateTime.Parse(cMantencionI.Text) > DateTime.Parse(cMantencionT.Text))
            {
                MessageBox.Show("La fecha de inicio no puede ser\nmayor a la fecha de termino!!", "AVISO", MessageBoxButton.OK, MessageBoxImage.Stop);
                LimpiarData();
                return;
            }

            else if (CamposLlenos() == true)
            {
                objeto_CE_Departamentos.IdDepartamento = idDepartamento;
                objeto_CE_Departamentos.MantInicio = DateTime.Parse(cMantencionI.Text);
                objeto_CE_Departamentos.MantTermino = DateTime.Parse(cMantencionT.Text);

                objeto_CN_Departamentos.Mantencion(objeto_CE_Departamentos);
                CargarDatos();
                MessageBox.Show("Se ha registrado el mantenimiento!", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarData();
            }
            else
            {
                MessageBox.Show("Seleccione ambos rangos de fechas!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion


        #region Limpiar Campos

        public void LimpiarData()
        {
            cMantencionT.Text = ""; 
            cMantencionI.Text = "";
            BtnGuardar.IsEnabled = true;
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


