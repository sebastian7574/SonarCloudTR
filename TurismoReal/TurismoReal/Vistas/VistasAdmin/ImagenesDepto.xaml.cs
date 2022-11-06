using CapaDeEntidad.Clases;
using CapaDeNegocio.Clases;
using Microsoft.Win32;
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
    /// Lógica de interacción para ImagenesDepto.xaml
    /// </summary>
    public partial class ImagenesDepto : UserControl
    {
        readonly CN_Galeria objeto_CN_Galeria = new CN_Galeria();
        readonly CE_Galeria objeto_CE_Galeria = new CE_Galeria();

        public ImagenesDepto()
        {
            InitializeComponent();
        }

        #region CARGAR IMAGENES
        void CargarDatos()
        {
            GridDatos.ItemsSource = objeto_CN_Galeria.CargarImagen(idDepartamento).DefaultView;
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
            if (tbDescripcion.Text == ""
                || tbIDdepto.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region AGREGAR IMAGEN
        public int idDepartamento;
        private void Crear(object sender, RoutedEventArgs e)
        {
            #region NOMBRE/DESCRIPCIÓN
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
                    MessageBox.Show("La descripción solo puede tener letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
            }
            #endregion

            

            if (CamposLlenos() == true)
            {
                objeto_CE_Galeria.DescripcionImagen = tbDescripcion.Text;
                objeto_CE_Galeria.IdDepartamento = int.Parse(tbIDdepto.Text);

                #region IMAGEN
                if (imagensubida == true)
                {
                    objeto_CE_Galeria.Imagen = img;

                    objeto_CN_Galeria.Insertar(objeto_CE_Galeria);
                    CargarDatos();
                    MessageBox.Show("Se ha guardado la imagen!", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarData();
                }
                #endregion
                else
                {
                    MessageBox.Show("La imagen no se ha guardado, intentelo denuevo", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            else
            {
                MessageBox.Show("No se ha seleccionado una imagen", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #region SUBIR IMAGEN

        byte[] img;
        private bool imagensubida = false;
        private void Subir(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                img = new byte[fs.Length];
                fs.Read(img, 0, System.Convert.ToInt32(fs.Length));
                fs.Close();

                ImageSourceConverter imgs = new ImageSourceConverter();
                imagen.SetValue(Image.SourceProperty, imgs.ConvertFromString(ofd.FileName.ToString()));
            }
            imagensubida = true;
        }

        #endregion

        #endregion

        #region CONSULTAR
        public int idGaleria;
        private void Consultar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            var a = objeto_CN_Galeria.Consulta(id);

            tbIDdepto.IsEnabled = false;
            tbDescripcion.IsEnabled = false;
            BtnActualizar.IsEnabled = false;
            BtnCrear.IsEnabled = false;
            BtnGuardar.IsEnabled = false;

            ImageSourceConverter imgs = new ImageSourceConverter();
            imagen.Source = (ImageSource)imgs.ConvertFrom(a.Imagen);
            tbDescripcion.Text = a.DescripcionImagen.ToString();
        }
        #endregion

        #region ACTUALIZAR
        public void ActualizarC(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            tbDescripcion.IsEnabled = true;
            BtnGuardar.IsEnabled = false;
            BtnCrear.IsEnabled = true;
            BtnActualizar.IsEnabled = true;

            var a = objeto_CN_Galeria.Consulta(id);

            tbID.Text = id.ToString();
            tbDescripcion.Text = a.DescripcionImagen.ToString();

            ImageSourceConverter imgs = new ImageSourceConverter();
            imagen.Source = (ImageSource)imgs.ConvertFrom(a.Imagen);

        }
        private void Actualizar(object sender, RoutedEventArgs e)
        {
            #region NOMBRE/DESCRIPCIÓN
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
                    MessageBox.Show("La descrición es muy corta", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
                //valido que se ingresen solo letras
                else if (Regex.IsMatch(tbDescripcion.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$") == false)
                {
                    MessageBox.Show("La descripción solo puede tener letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
            }
            #endregion

            if (CamposLlenos() == true)
            {
                objeto_CE_Galeria.idGaleria = int.Parse(tbID.Text);
                objeto_CE_Galeria.DescripcionImagen = tbDescripcion.Text;
                objeto_CE_Galeria.Imagen = img;

                objeto_CN_Galeria.ActualizarIMG(objeto_CE_Galeria);
                CargarDatos();
                MessageBox.Show("Se actualizó exitosamente!!", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarData();
                BtnCrear.IsEnabled = true;
                BtnGuardar.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("No se a cambiado la imagen", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region ELIMINAR
        private void Eliminar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            if (MessageBox.Show("¿Está seguro de eliminar la imagen?", "Eliminar Imagen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                objeto_CE_Galeria.idGaleria = id;
                objeto_CN_Galeria.Eliminar(objeto_CE_Galeria);
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

            var a = objeto_CN_Galeria.Consulta(1);
            ImageSourceConverter imgs = new ImageSourceConverter();
            imagen.Source = (ImageSource)imgs.ConvertFrom(a.Imagen);

            BtnActualizar.IsEnabled = false;
            BtnCrear.IsEnabled = true;
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


