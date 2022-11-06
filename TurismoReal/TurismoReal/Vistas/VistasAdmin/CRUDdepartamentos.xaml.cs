using CapaDeEntidad.Clases;
using CapaDeNegocio.Clases;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace TurismoReal.Vistas.VistasAdmin
{
    /// <summary>
    /// Lógica de interacción para CRUDdepartamentos.xaml
    /// </summary>
    public partial class CRUDdepartamentos : Page
    {
        readonly CN_Departamentos objeto_CN_Departamentos = new CN_Departamentos();
        readonly CE_Departamentos objeto_CE_Departamentos = new CE_Departamentos();

        readonly CN_Comuna objeto_CN_Comuna = new CN_Comuna();
        readonly CN_Region objeto_CN_Region = new CN_Region();
        readonly CN_EstadoDepto objeto_CN_EstadoDepto = new CN_EstadoDepto();

        public CRUDdepartamentos()
        {
            InitializeComponent();
            CargarE();
        }

        #region Cargar FK
        void CargarE()
        {
            List<string> estadoDepto = objeto_CN_EstadoDepto.ListarEstado();
            for (int i = 0; i < estadoDepto.Count; i++)
            {
                cbEstadoDepto.Items.Add(estadoDepto[i]);
            }
        }
        #endregion

        #region Regresar
        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new Departamentos();
        }
        #endregion

        #region Valdiaciones generales
        #region VALIDAR CAMPOS VACÍOS
        public bool CamposLlenos()
        {
            if (tbNombreDepto.Text == ""
                || tbDireccion.Text == ""
                || tbCantHabitaciones.Text == ""
                || tbCantBanos.Text == ""
                || tbPrecio.Text == ""
                || cbComuna.Text == ""
                || cbRegion.Text == ""
                || cbEstadoDepto.Text == "")
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

        public int idDepartamento;
        public int idRegion;
        public int idComuna;
        #region Crear
        private void Crear(object sender, RoutedEventArgs e)
        {
            //Validaciones 
            #region NOMBRE/DESCRIPCIÓN
            if (tbNombreDepto.Text == "")
            {
                MessageBox.Show("El nombre no puede quedar vacío", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbNombreDepto.Focus();
                return;
            }
            else if (tbNombreDepto.Text != "")
            {
                if (tbNombreDepto.Text.Length > 30)
                {
                    MessageBox.Show("El nombre es demasiado extenso", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbNombreDepto.Clear();
                    tbNombreDepto.Focus();
                    return;
                }
                else if (tbNombreDepto.Text.Length < 3)
                {
                    MessageBox.Show("El nombre es muy corto", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbNombreDepto.Clear();
                    tbNombreDepto.Focus();
                    return;
                }
                //valido que se ingresen solo letras
                else if (Regex.IsMatch(tbNombreDepto.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$") == false)
                {
                    MessageBox.Show("El nombre solo puede contener letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbNombreDepto.Clear();
                    tbNombreDepto.Focus();
                    return;
                }
            }
            #endregion

            #region COMBOBOX REGION COMUNA
            if (cbRegion.Text == "")
            {
                MessageBox.Show("Debe seleccionar una Region", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (cbComuna.Text == "")
            {
                MessageBox.Show("Debe seleccionar una Comuna", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            #endregion

            #region DIRECCIÓN
            if (tbDireccion.Text == "")
            {
                MessageBox.Show("La dirección no puede quedar vacía!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbDireccion.Focus();
                return;
            }
            else if (tbDireccion.Text.Length < 6)
            {
                MessageBox.Show("La dirección es muy corta, ¿Está bien escrita?", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbDireccion.Clear();
                tbDireccion.Focus();
                return;
            }
            else if (tbDireccion.Text.Length > 35)
            {
                MessageBox.Show("La dirección es muy extensa", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbDireccion.Clear();
                tbDireccion.Focus();
                return;
            }
            else if (IsAlphaNumeric(tbDireccion.Text.ToString()) == false)
            {
                MessageBox.Show("La dirección solo debe contener letras y números", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbDireccion.Clear();
                tbDireccion.Focus();
                return;
            }
            #endregion

            #region HABITACIONES, BAÑOS Y PRECIO
            //HABITACIONES
            if (tbCantHabitaciones.Text == "")
            {
                MessageBox.Show("Debe ingresar la cantidad de habitaciones", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantHabitaciones.Focus();
                return;
            }
            else if (Regex.IsMatch(tbCantHabitaciones.Text, @"^[z0-9]+$") == false)
            {
                MessageBox.Show("Ingrese solo números para especificar\nla cantidad de habitaciones!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantHabitaciones.Clear();
                tbCantHabitaciones.Focus();
                return;
            }
            else if (tbCantHabitaciones.Text == "0")
            {
                MessageBox.Show("La cantidad de habitaciones no puede ser 0", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantHabitaciones.Clear();
                tbCantHabitaciones.Focus();
                return;
            }
            else if (tbCantHabitaciones.Text.Length > 1)
            {
                MessageBox.Show("Ingrese un número no mayor a 10", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantHabitaciones.Focus();
                tbCantHabitaciones.Clear();
                return;
            }

            //BAÑOS
            else if (tbCantBanos.Text == "")
            {
                MessageBox.Show("Debe ingresar la cantidad de baños", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantBanos.Focus();
                return;
            }
            else if (Regex.IsMatch(tbCantBanos.Text, @"^[z0-9]+$") == false)
            {
                MessageBox.Show("Ingrese solo números para especificar\nla cantidad de baños!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantBanos.Clear();
                tbCantBanos.Focus();
                return;
            }
            else if (tbCantBanos.Text == "0")
            {
                MessageBox.Show("La cantidad de baños no puede ser 0", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantBanos.Clear();
                tbCantBanos.Focus();
                return;
            }
            else if (tbCantBanos.Text.Length > 1)
            {
                MessageBox.Show("Ingrese un número no mayor a 10", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantBanos.Clear();
                tbCantBanos.Focus();
                return;
            }

            //PRECIO
            else if (tbPrecio.Text == "")
            {
                MessageBox.Show("Debe ingresar el precio por noche", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Focus();
                return;
            }
            else if (Regex.IsMatch(tbPrecio.Text, @"^[z0-9]+$") == false)
            {
                MessageBox.Show("El precio solo puede tener números", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Clear();
                tbPrecio.Focus();
                return;
            }
            else if (tbPrecio.Text == "0")
            {
                MessageBox.Show("El precio no puede ser 0", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Clear();
                tbPrecio.Focus();
                return;
            }
            else if (tbPrecio.Text.Length > 6 || tbPrecio.Text.Length < 5)
            {
                MessageBox.Show("El precio puede tener un largo de hasta 6 dígitos\n(10.000 - 999.999)", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Clear();
                tbPrecio.Focus();
                return;
            }
            #endregion

            #region ESTADO
            if (cbEstadoDepto.Text == "")
            {
                MessageBox.Show("Debe seleccionar un Estado del Depto.", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            #endregion

            if (CamposLlenos() == true)
            {
                int estadoDepto = objeto_CN_EstadoDepto.IdEstadoDepto(cbEstadoDepto.Text);
                int comuna = objeto_CN_Comuna.IdComuna(cbComuna.Text);

                objeto_CE_Departamentos.Descripcion = tbNombreDepto.Text;
                objeto_CE_Departamentos.Direccion = tbDireccion.Text;
                objeto_CE_Departamentos.CantHabitaciones = int.Parse(tbCantHabitaciones.Text);
                objeto_CE_Departamentos.CantBanos = int.Parse(tbCantBanos.Text);
                objeto_CE_Departamentos.PrecioNoche = int.Parse(tbPrecio.Text);
                objeto_CE_Departamentos.IdComuna = comuna; 
                objeto_CE_Departamentos.IdEstadoDepto = estadoDepto;

                objeto_CN_Departamentos.Insertar(objeto_CE_Departamentos);
                MessageBox.Show("Se ha ingresado un nuevo departamento!", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                Content = new Departamentos();
            }

            else
            {
                MessageBox.Show("No se pudo ingresar el depto,\n revise los datos e intentelo denuevo", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region Actualizar
        private void Actualizar(object sender, RoutedEventArgs e)
        {
            //Validaciones 
            #region NOMBRE/DESCRIPCIÓN
            if (tbNombreDepto.Text == "")
            {
                MessageBox.Show("El nombre no puede quedar vacío", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbNombreDepto.Focus();
                return;
            }
            else if (tbNombreDepto.Text != "")
            {
                if (tbNombreDepto.Text.Length > 30)
                {
                    MessageBox.Show("El nombre es demasiado extenso", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbNombreDepto.Clear();
                    tbNombreDepto.Focus();
                    return;
                }
                else if (tbNombreDepto.Text.Length < 3)
                {
                    MessageBox.Show("El nombre es muy corto", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbNombreDepto.Clear();
                    tbNombreDepto.Focus();
                    return;
                }
                //valido que se ingresen solo letras
                else if (Regex.IsMatch(tbNombreDepto.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$") == false)
                {
                    MessageBox.Show("El nombre solo puede contener letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbNombreDepto.Clear();
                    tbNombreDepto.Focus();
                    return;
                }
            }
            #endregion

            #region COMBOBOX REGION COMUNA
            if (cbRegion.Text == "")
            {
                MessageBox.Show("Debe seleccionar una Region", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (cbComuna.Text == "")
            {
                MessageBox.Show("Debe seleccionar una Comuna", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            #endregion

            #region DIRECCIÓN
            if (tbDireccion.Text == "")
            {
                MessageBox.Show("La dirección no puede quedar vacía!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbDireccion.Focus();
                return;
            }
            else if (tbDireccion.Text.Length < 6)
            {
                MessageBox.Show("La dirección es muy corta, ¿Está bien escrita?", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbDireccion.Clear();
                tbDireccion.Focus();
                return;
            }
            else if (tbDireccion.Text.Length > 35)
            {
                MessageBox.Show("La dirección es muy extensa", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbDireccion.Clear();
                tbDireccion.Focus();
                return;
            }
            else if (IsAlphaNumeric(tbDireccion.Text.ToString()) == false)
            {
                MessageBox.Show("La dirección solo debe contener letras y números", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbDireccion.Clear();
                tbDireccion.Focus();
                return;
            }
            #endregion

            #region HABITACIONES, BAÑOS Y PRECIO
            //HABITACIONES
            if (tbCantHabitaciones.Text == "")
            {
                MessageBox.Show("Debe ingresar la cantidad de habitaciones", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantHabitaciones.Focus();
                return;
            }
            else if (Regex.IsMatch(tbCantHabitaciones.Text, @"^[z0-9]+$") == false)
            {
                MessageBox.Show("Ingrese solo números para especificar\nla cantidad de habitaciones!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantHabitaciones.Clear();
                tbCantHabitaciones.Focus();
                return;
            }
            else if (tbCantHabitaciones.Text == "0")
            {
                MessageBox.Show("La cantidad de habitaciones no puede ser 0", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantHabitaciones.Clear();
                tbCantHabitaciones.Focus();
                return;
            }
            else if (tbCantHabitaciones.Text.Length > 1)
            {
                MessageBox.Show("Ingrese un número no mayor a 10", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantHabitaciones.Focus();
                tbCantHabitaciones.Clear();
                return;
            }

            //BAÑOS
            else if (tbCantBanos.Text == "")
            {
                MessageBox.Show("Debe ingresar la cantidad de baños", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantBanos.Focus();
                return;
            }
            else if (Regex.IsMatch(tbCantBanos.Text, @"^[z0-9]+$") == false)
            {
                MessageBox.Show("Ingrese solo números para especificar\nla cantidad de baños!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantBanos.Clear();
                tbCantBanos.Focus();
                return;
            }
            else if (tbCantBanos.Text == "0")
            {
                MessageBox.Show("La cantidad de baños no puede ser 0", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantBanos.Clear();
                tbCantBanos.Focus();
                return;
            }
            else if (tbCantBanos.Text.Length > 1)
            {
                MessageBox.Show("Ingrese un número no mayor a 10", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCantBanos.Clear();
                tbCantBanos.Focus();
                return;
            }

            //PRECIO
            else if (tbPrecio.Text == "")
            {
                MessageBox.Show("Debe ingresar el precio por noche", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Focus();
                return;
            }
            else if (Regex.IsMatch(tbPrecio.Text, @"^[z0-9]+$") == false)
            {
                MessageBox.Show("El precio solo puede tener números", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Clear();
                tbPrecio.Focus();
                return;
            }
            else if (tbPrecio.Text == "0")
            {
                MessageBox.Show("El precio no puede ser 0", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Clear();
                tbPrecio.Focus();
                return;
            }
            else if (tbPrecio.Text.Length > 6 || tbPrecio.Text.Length < 5)
            {
                MessageBox.Show("El precio puede tener un largo de hasta 6 dígitos\n(10.000 - 999.999)", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPrecio.Clear();
                tbPrecio.Focus();
                return;
            }
            #endregion

            #region ESTADO
            if (cbEstadoDepto.Text == "")
            {
                MessageBox.Show("Debe seleccionar un Estado del Depto.", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            #endregion

            if (CamposLlenos() == true)
            {
                int estadoDepto = objeto_CN_EstadoDepto.IdEstadoDepto(cbEstadoDepto.Text);
                int comuna = objeto_CN_Comuna.IdComuna(cbComuna.Text);

                objeto_CE_Departamentos.IdDepartamento = idDepartamento;
                objeto_CE_Departamentos.Descripcion = tbNombreDepto.Text;
                objeto_CE_Departamentos.IdComuna = comuna; 
                objeto_CE_Departamentos.Direccion = tbDireccion.Text;
                objeto_CE_Departamentos.CantHabitaciones = int.Parse(tbCantHabitaciones.Text);
                objeto_CE_Departamentos.CantBanos = int.Parse(tbCantBanos.Text);
                objeto_CE_Departamentos.PrecioNoche = int.Parse(tbPrecio.Text);
                objeto_CE_Departamentos.IdEstadoDepto = estadoDepto;

                objeto_CN_Departamentos.ActualizarDatos(objeto_CE_Departamentos);
                MessageBox.Show("Se ha actualizado correctamente!", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                Content = new Departamentos();
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
            var a = objeto_CN_Departamentos.Consulta(idDepartamento);
            var d = objeto_CN_EstadoDepto.NombreEstado(a.IdEstadoDepto);

            tbNombreDepto.Text = a.Descripcion.ToString();
            MostrarComuna(a.IdComuna);
            tbDireccion.Text = a.Direccion.ToString();
            tbCantHabitaciones.Text = a.CantHabitaciones.ToString();
            tbCantBanos.Text = a.CantBanos.ToString();
            tbPrecio.Text = a.PrecioNoche.ToString();
            cbEstadoDepto.Text = d.EstadoDepto.ToString();
        }
        #endregion

        #region Galeria
        public void BtnGaleria_Click(object sender, RoutedEventArgs e)
        {
            ImagenesDepto ventana = new ImagenesDepto();
            ventana.idDepartamento = idDepartamento;
            FrameGaleria.Content = ventana;
            ventana.BtnActualizar.IsEnabled = false;
            ventana.tbIDdepto.Text = " " + idDepartamento;
            ventana.Titulo.Text = "Galeria Depto. N°" + idDepartamento;
        }
        #endregion

        #region Combobox Anidado
        private void Anidado(object sender, RoutedEventArgs e)
        {
            if (cbComuna.Text == "")
            {
                CargarRegion();
            }
            else
            {
                var a = objeto_CN_Departamentos.Consulta(idDepartamento);
                var c = objeto_CN_Comuna.NombreComuna(a.IdComuna);
                MostrarComuna(a.IdComuna);
                cbRegion.DataContext = objeto_CN_Comuna.MostrarRegion(a.IdComuna);
                cbRegion.SelectedIndex = c.IdRegion - 1;
                CargarRegion();
            }
        }

        public void CargarRegion()
        {
            cbRegion.DisplayMemberPath = "region";
            cbRegion.SelectedValuePath = "idRegion";
            cbRegion.DataContext = objeto_CN_Region.listaRegiones();
        }

        private void Region(object sender, SelectionChangedEventArgs e)
        {
            if (cbComuna.Text == "")
            {
                string regionid = cbRegion.SelectedValue.ToString();
                int idRegion = Convert.ToInt32(regionid);
                CargarComuna(idRegion);
            }
        }

        public void CargarComuna(int idRegion)
        {
            cbComuna.DisplayMemberPath = "comuna";
            cbComuna.SelectedValuePath = "idComuna";
            cbComuna.DataContext = objeto_CN_Comuna.ListarComunas(idRegion);
        }

        public void MostrarComuna(int idComuna)
        {
            cbComuna.DataContext = objeto_CN_Comuna.MostrarComuna(idComuna);
            cbComuna.SelectedIndex = 0;
        }

        #endregion

        private void BtnMantencion_Click(object sender, RoutedEventArgs e)
        {
            Mantencion ventana = new Mantencion();
            ventana.idDepartamento = idDepartamento;
            FrameGaleria.Content = ventana;
            ventana.tbIDdepto.Text = " " + idDepartamento;
            ventana.Titulo.Text = "Mantención Depto. N°" + idDepartamento;
        }
    }
}
