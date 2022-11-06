using CapaDeEntidad.Clases;
using CapaDeNegocio.Clases;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TurismoReal.Vistas.VistasAdmin
{
    /// <summary>
    /// Lógica de interacción para CRUDservicios.xaml
    /// </summary>
    public partial class CRUDservicios : Page
    {
        readonly CN_Servicios objeto_CN_Servicios = new CN_Servicios();
        readonly CE_Servicios objeto_CE_Servicios = new CE_Servicios();
        readonly CN_TipoServicio objeto_CN_TipoServicio = new CN_TipoServicio();

        public CRUDservicios()
        {
            InitializeComponent();
            CargarTP();
        }

        #region Cargar FK
        void CargarTP()
        {
            List<string> tiposervicio = objeto_CN_TipoServicio.ListarTipoServicio();
            for (int i = 0; i < tiposervicio.Count; i++)
            {
                cbTipoServicio.Items.Add(tiposervicio[i]);
            }
        }
        #endregion

        #region Resgresar
        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new Servicios();
        }
        #endregion

        #region ValidarCamposVacios
        public bool CamposLlenos()
        {
            if (tbDescripcion.Text == ""
                || tbPrecio.Text == ""
                || cbTipoServicio.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion


        #region CREAR
        public int idServicio;
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
                    MessageBox.Show("La descripción solo puede contener letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
            }
            #endregion

            #region PRECIO
            if (tbPrecio.Text == "")
            {
                MessageBox.Show("Debe ingresar precio del servicio", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Focus();
                return;
            }
            else if (int.Parse(tbPrecio.Text) == 0)
            {
                MessageBox.Show("El precio no puede ser 0", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Clear();
                tbPrecio.Focus();
                return;
            }
            else if (Regex.IsMatch(tbPrecio.Text, @"^[z0-9]+$") == false)
            {
                MessageBox.Show("Ingrese solo números en el precio", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Clear();
                tbPrecio.Focus();
                return;
            }
            else if (tbPrecio.Text.Length < 4)
            {
                MessageBox.Show("El precio es muy bajo, no contamos con valores\nmenores a mil!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Focus();
                tbPrecio.Clear();
                return;
            }
            else if (tbPrecio.Text.Length > 5)
            {
                MessageBox.Show("El precio es muy elevado, no contamos con servicios\ntan caros!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Focus();
                tbPrecio.Clear();
                return;
            }

            #endregion

            #region ESTADO
            else if (cbTipoServicio.Text == "")
            {
                MessageBox.Show("Debe seleccionar un Tipo de servicio", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            #endregion

            #region ESTADO DE CUENTA
            else if (ckbDisponible.IsChecked == false)
            {
                MessageBox.Show("Se ingreso un servicio que no se encuentra disponible", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            #endregion

            if (CamposLlenos() == true)
            {
                try
                {
                    int tiposervicio = objeto_CN_TipoServicio.IdTipoServicio(cbTipoServicio.Text);

                    objeto_CE_Servicios.Descripcion = tbDescripcion.Text;
                    if (ckbDisponible.IsChecked == true)
                    {
                        objeto_CE_Servicios.Disponibilidad = "Disponible";
                    }
                    else
                    {
                        objeto_CE_Servicios.Disponibilidad = "No Disponible";
                    }
                    objeto_CE_Servicios.Precio = int.Parse(tbPrecio.Text);
                    objeto_CE_Servicios.IdTipoServicio = tiposervicio;

                    objeto_CN_Servicios.Insertar(objeto_CE_Servicios);

                    Content = new Servicios();
                }
                catch
                {
                    MessageBox.Show("No pueden quedar campos vacíos!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("No se pudo ingresar el servicio,\n revise los datos e intentelo denuevo", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region Actualizar
        private void Actualizar(object sender, RoutedEventArgs e)
        {
            //Validaciones
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
                    MessageBox.Show("La descripción solo puede contener letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
            }
            #endregion

            #region PRECIO
            if (tbPrecio.Text == "")
            {
                MessageBox.Show("Debe ingresar precio del servicio", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Focus();
                return;
            }
            else if (int.Parse(tbPrecio.Text) == 0)
            {
                MessageBox.Show("El precio no puede ser 0", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Clear();
                tbPrecio.Focus();
                return;
            }
            else if (Regex.IsMatch(tbPrecio.Text, @"^[z0-9]+$") == false)
            {
                MessageBox.Show("Ingrese solo números en el precio", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Clear();
                tbPrecio.Focus();
                return;
            }
            else if (tbPrecio.Text.Length < 4)
            {
                MessageBox.Show("El precio es muy bajo, no contamos con valores\nmenores a mil!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Focus();
                tbPrecio.Clear();
                return;
            }
            else if (tbPrecio.Text.Length > 5)
            {
                MessageBox.Show("El precio es muy elevado, no contamos con servicios\ntan caros!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Focus();
                tbPrecio.Clear();
                return;
            }

            #endregion

            #region ESTADO
            else if (cbTipoServicio.Text == "")
            {
                MessageBox.Show("Debe seleccionar un Tipo de servicio", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            #endregion

            #region ESTADO DE CUENTA
            else if (ckbDisponible.IsChecked == false)
            {
                MessageBox.Show("Se ingreso un servicio que no se encuentra disponible", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            #endregion

            if (CamposLlenos() == true)
            {
                int tiposervicio = objeto_CN_TipoServicio.IdTipoServicio(cbTipoServicio.Text);

                objeto_CE_Servicios.IdServicio = idServicio;
                objeto_CE_Servicios.Descripcion = tbDescripcion.Text;
                if (ckbDisponible.IsChecked == true)
                {
                    objeto_CE_Servicios.Disponibilidad = "Disponible";
                }
                else
                {
                    objeto_CE_Servicios.Disponibilidad = "No Disponible";
                }
                objeto_CE_Servicios.Precio = int.Parse(tbPrecio.Text);
                objeto_CE_Servicios.IdTipoServicio = tiposervicio;

                objeto_CN_Servicios.ActualizarDatos(objeto_CE_Servicios);
                Content = new Servicios();
            }
            else
            {
                MessageBox.Show("Por favor, no dejar campos vacios", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region Consultar
        public void Consultar()
        {
            var a = objeto_CN_Servicios.Consulta(idServicio);
            var ts = objeto_CN_TipoServicio.NombreTipoServicio(a.IdTipoServicio);

            BtnTipoServicio.IsEnabled = false;
            tbDescripcion.Text = a.Descripcion.ToString();
            tbPrecio.Text = a.Precio.ToString();
            cbTipoServicio.Text = ts.TipoServicio.ToString();
            if (a.Disponibilidad == "Disponible")
            {
                ckbDisponible.IsChecked = true;
            }
            else
            {
                ckbDisponible.IsChecked = false;
            }
        }
        #endregion

        #region Eliminar
        private void Eliminar(object sender, RoutedEventArgs e)
        {
            objeto_CE_Servicios.IdServicio = idServicio;

            objeto_CN_Servicios.Eliminar(objeto_CE_Servicios);

            Content = new Servicios();
        }
        #endregion

        private void BtnTipoServicio_Click(object sender, RoutedEventArgs e)
        {
            Content = new TipoServicio();
        }
    }
}