using CapaDeEntidad.Clases;
using CapaDeNegocio.Clases;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TurismoReal.Vistas.VistasAdmin
{
    /// <summary>
    /// Lógica de interacción para CRUDartefacto.xaml
    /// </summary>
    public partial class CRUDartefacto : Page
    {
        readonly CN_UnidadMedida objeto_CN_UnidadMedida = new CN_UnidadMedida();
        readonly CN_Artefactos objeto_CN_Artefactos = new CN_Artefactos();
        readonly CE_Artefactos objeto_CE_Artefactos = new CE_Artefactos();

        public CRUDartefacto()
        {
            InitializeComponent();
            CargarU();
            CargarDatos();
        }

        #region CARGAR Artefactos
        void CargarDatos()
        {
            GridDatos.ItemsSource = objeto_CN_Artefactos.CargarArtefactos().DefaultView;
        }
        #endregion

        #region Cargar FK
        void CargarU()
        {
            List<string> tipoUnidad = objeto_CN_UnidadMedida.ListarUnidad();
            for (int i = 0; i < tipoUnidad.Count; i++)
            {
                cbUnidad.Items.Add(tipoUnidad[i]);
            }
        }
        #endregion

        #region Regresar
        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new Inventario();
        }
        #endregion

        #region Validaciones generales

        #region VALIDAR CAMPOS VACÍOS
        public bool CamposLlenos()
        {
            if (tbDescripcion.Text == ""
                || tbTamaño.Text == ""
                || tbColor.Text == ""
                || tbValor.Text == ""
                || cbUnidad.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region VALIDACIÓN ALFA NÚMERICO
        public bool IsAlphaNumeric(string texto)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-ZñÑáéíóúÁÉÍÓÚ -Z0-9]");
            return !objAlphaNumericPattern.IsMatch(texto);
        }
        #endregion

        #endregion

        public int idArtefactos;
        public int idUnidadMedida;
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
                else if (IsAlphaNumeric(tbDescripcion.Text.ToString()) == false)
                {
                    MessageBox.Show("Solo se pueden ingresar letras\ny números en la descripción", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
            }
            #endregion

            #region COLOR
            if (tbColor.Text == "")
            {
                MessageBox.Show("El color no puede quedar vacío", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbColor.Focus();
                return;
            }
            else if (tbColor.Text != "")
            {
                if (tbColor.Text.Length > 30)
                {
                    MessageBox.Show("El nombre del color es muy extenso", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbColor.Clear();
                    tbColor.Focus();
                    return;
                }
                else if (tbColor.Text.Length < 3)
                {
                    MessageBox.Show("Ups, no existen nombres de colores tan cortos!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbColor.Clear();
                    tbColor.Focus();
                    return;
                }
                //valido que se ingresen solo letras
                else if (Regex.IsMatch(tbColor.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]") == false)
                {
                    MessageBox.Show("Solo se pueden ingresar letras en el color", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbColor.Clear();
                    tbColor.Focus();
                    return;
                }
            }
            #endregion

            #region TAMAÑO
            if (tbTamaño.Text == "")
            {
                MessageBox.Show("Debe ingresar el tamaño (en números)", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbTamaño.Focus();
                return;
            }
            else if (Regex.IsMatch(tbTamaño.Text, @"^[z0-9]+$") == false)
            {
                MessageBox.Show("Ingrese solo números para el tamaño", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbTamaño.Clear();
                tbTamaño.Focus();
                return;
            }
            else if (tbTamaño.Text.Length > 4)
            {
                MessageBox.Show("El tamaño es muy grande, no supere los 4 dígitos", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbTamaño.Focus();
                tbTamaño.Clear();
                return;
            }
            #endregion

            #region UNIDAD DE MEDIDA
            if (cbUnidad.Text == "")
            {
                MessageBox.Show("Debe seleccionar una unidad de medida", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            #endregion

            #region VALOR
            if (tbValor.Text == "")
            {
                MessageBox.Show("Debe ingresar el valor del artefacto", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbValor.Focus();
                return;
            }
            else if (Regex.IsMatch(tbValor.Text, @"^[z0-9]+$") == false)
            {
                MessageBox.Show("Solo se pueden ingresar números en el valor", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbValor.Clear();
                tbValor.Focus();
                return;
            }
            else if (tbValor.Text == "0")
            {
                MessageBox.Show("El valor no puede ser 0", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbValor.Focus();
                return;
            }
            #endregion

            if (CamposLlenos() == true)
            {
                try
                {
                    int unidad = objeto_CN_UnidadMedida.IdUnidad(cbUnidad.Text);

                    objeto_CE_Artefactos.Descripcion = tbDescripcion.Text;
                    objeto_CE_Artefactos.Tamano = int.Parse(tbTamaño.Text);
                    objeto_CE_Artefactos.Color = tbColor.Text; 
                    objeto_CE_Artefactos.Valor = int.Parse(tbValor.Text);
                    objeto_CE_Artefactos.IdUnidadMedida = unidad;

                    objeto_CN_Artefactos.Insertar(objeto_CE_Artefactos);
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
                MessageBox.Show("No se pudo registrar el Artefacto,\n revise los datos e intentelo denuevo", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion
        public void ActualizarC(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            tbDescripcion.IsEnabled = true;
            tbTamaño.IsEnabled = true;
            tbColor.IsEnabled = true;
            tbValor.IsEnabled = true;
            cbUnidad.IsEnabled = true;
            BtnCrear.IsEnabled = false;
            BtnActualizar.IsEnabled = true;

            var a = objeto_CN_Artefactos.Consulta(id);
            var c = objeto_CN_UnidadMedida.NombreUnidad(a.IdUnidadMedida);

            tbID.Text = id.ToString();
            tbDescripcion.Text = a.Descripcion.ToString();
            tbTamaño.Text = a.Tamano.ToString();
            tbColor.Text = a.Color.ToString();
            tbValor.Text = a.Valor.ToString();
            cbUnidad.Text = c.TipoUnidad.ToString();
            
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
                else if (IsAlphaNumeric(tbDescripcion.Text.ToString()) == false)
                {
                    MessageBox.Show("Solo se pueden ingresar letras\ny números en la descripción", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDescripcion.Clear();
                    tbDescripcion.Focus();
                    return;
                }
            }
            #endregion

            #region COLOR
            if (tbColor.Text == "")
            {
                MessageBox.Show("El color no puede quedar vacío", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbColor.Focus();
                return;
            }
            else if (tbColor.Text != "")
            {
                if (tbColor.Text.Length > 30)
                {
                    MessageBox.Show("El nombre del color es muy extenso", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbColor.Clear();
                    tbColor.Focus();
                    return;
                }
                else if (tbColor.Text.Length < 3)
                {
                    MessageBox.Show("Ups, no existen nombres de colores tan cortos!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbColor.Clear();
                    tbColor.Focus();
                    return;
                }
                //valido que se ingresen solo letras
                else if (Regex.IsMatch(tbColor.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]") == false)
                {
                    MessageBox.Show("Solo se pueden ingresar letras en el color", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbColor.Clear();
                    tbColor.Focus();
                    return;
                }
            }
            #endregion

            #region TAMAÑO
            if (tbTamaño.Text == "")
            {
                MessageBox.Show("Debe ingresar el tamaño (en números)", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbTamaño.Focus();
                return;
            }
            else if (Regex.IsMatch(tbTamaño.Text, @"^[z0-9]+$") == false)
            {
                MessageBox.Show("Ingrese solo números para el tamaño", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbTamaño.Clear();
                tbTamaño.Focus();
                return;
            }
            else if (tbTamaño.Text.Length > 4)
            {
                MessageBox.Show("El tamaño es muy grande, no supere los 4 dígitos", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbTamaño.Focus();
                tbTamaño.Clear();
                return;
            }
            #endregion

            #region UNIDAD DE MEDIDA
            if (cbUnidad.Text == "")
            {
                MessageBox.Show("Debe seleccionar una unidad de medida", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            #endregion

            #region VALOR
            if (tbValor.Text == "")
            {
                MessageBox.Show("Debe ingresar el valor del artefacto", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbValor.Focus();
                return;
            }
            else if (Regex.IsMatch(tbValor.Text, @"^[z0-9]+$") == false)
            {
                MessageBox.Show("Solo se pueden ingresar números en el valor", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbValor.Clear();
                tbValor.Focus();
                return;
            }
            else if (tbValor.Text == "0")
            {
                MessageBox.Show("El valor no puede ser 0", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbValor.Focus();
                return;
            }
            #endregion

            if (CamposLlenos() == true)
            {
                int unidad = objeto_CN_UnidadMedida.IdUnidad(cbUnidad.Text);

                objeto_CE_Artefactos.IdArtefactos = int.Parse(tbID.Text);
                objeto_CE_Artefactos.Descripcion = tbDescripcion.Text;
                objeto_CE_Artefactos.Tamano = int.Parse(tbTamaño.Text);
                objeto_CE_Artefactos.Color = tbColor.Text;
                objeto_CE_Artefactos.Valor = int.Parse(tbValor.Text);
                objeto_CE_Artefactos.IdUnidadMedida = unidad;

                objeto_CN_Artefactos.ActualizarDatos(objeto_CE_Artefactos);
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
            tbDescripcion.IsEnabled = false;
            tbTamaño.IsEnabled = false;
            tbColor.IsEnabled = false;
            tbValor.IsEnabled = false;
            cbUnidad.IsEnabled = false;
            BtnActualizar.IsEnabled = false;
            BtnCrear.IsEnabled = false;

            var a = objeto_CN_Artefactos.Consulta(id);
            var c = objeto_CN_UnidadMedida.NombreUnidad(a.IdUnidadMedida);

            tbDescripcion.Text = a.Descripcion.ToString();
            tbTamaño.Text = a.Tamano.ToString();
            tbColor.Text = a.Color.ToString();
            tbValor.Text = a.Valor.ToString("0,0", CultureInfo.InvariantCulture);
            cbUnidad.Text = c.TipoUnidad.ToString();
        }
        #endregion

        #region Eliminar
        private void Eliminar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            if (MessageBox.Show("¿Está seguro de eliminar el artefacto?", "Eliminar Artefacto", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                objeto_CE_Artefactos.IdArtefactos = id;
                objeto_CN_Artefactos.Eliminar(objeto_CE_Artefactos);
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
            tbColor.Clear();
            tbTamaño.Clear();
            tbValor.Clear();
            cbUnidad.SelectedIndex = -1;
            tbDescripcion.IsEnabled = true;
            tbTamaño.IsEnabled = true;
            tbColor.IsEnabled = true;
            tbValor.IsEnabled = true;
            cbUnidad.IsEnabled = true;
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