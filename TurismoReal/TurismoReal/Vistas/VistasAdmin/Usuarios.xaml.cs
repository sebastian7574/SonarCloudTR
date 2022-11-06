using CapaDeNegocio.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace TurismoReal.Vistas.VistasAdmin
{

    public partial class Usuarios : UserControl
    {
        readonly CN_Usuarios objeto_CN_Usuarios = new CN_Usuarios();
        readonly CN_TipoUsuarioFK objeto_CN_TipoUsuarioFK = new CN_TipoUsuarioFK();

        #region INICIAL
        public Usuarios()
        {
            InitializeComponent();
            CargarDatos();
            CargarCB();
        }
        #endregion

        #region Cargar FK
        void CargarCB()
        {
            List<string> tipousuario = objeto_CN_TipoUsuarioFK.ListarTiposUsuario();
            for (int i = 0; i < tipousuario.Count; i++)
            {
                cbFiltroTipo.Items.Add(tipousuario[i]);
            }
        }
        #endregion

        #region CARGAR USUARIOS
        void CargarDatos()
        {
            GridDatos.ItemsSource = objeto_CN_Usuarios.CargarUsuarios().DefaultView;
        }
        #endregion

        #region AGREGAR
        private void BtnAgregarUser_Click(object sender, RoutedEventArgs e)
        {
            CRUDusuarios ventana = new CRUDusuarios();
            FrameUsuarios.Content = ventana;
            ventana.BtnCrear.Visibility = Visibility.Visible;
            ventana.ChangePassword.Visibility = Visibility.Hidden;
            ventana.tbContrasena.Visibility = Visibility.Visible;
            ventana.txtContra.Visibility = Visibility.Visible;
        }
        #endregion

        #region ACTUALIZAR
        private void Actualizar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDusuarios ventana = new CRUDusuarios();
            ventana.idUsuario = id;
            ventana.Consultar();
            FrameUsuarios.Content = ventana;
            ventana.Titulo.Text = "Información del Usuario";
            ventana.tbNombre.IsEnabled = true;
            ventana.tbApellido.IsEnabled = true;
            ventana.tbCel.IsEnabled = true;
            ventana.tbPais.IsEnabled = true;
            ventana.tbCorreo.IsEnabled = true;
            ventana.tbRut.IsEnabled = true;
            ventana.tbUser.IsEnabled = true;
            ventana.tbContrasena.IsEnabled = true;
            ventana.cbTipoUsuario.IsEnabled = true;
            ventana.BtnActualizar.Visibility = Visibility.Visible;
            ventana.ChangePassword.Visibility = Visibility.Visible;
        }
        #endregion

        #region FUNCION BUSCAR
        #region Limpiar
        public void LimpiarData()
        {
            tbBuscar.Clear();
            cbFiltroTipo.SelectedIndex = -1;
        }

        #endregion
        private void Ver(object sender, RoutedEventArgs e)
        {

            if (tbBuscar.Text != "")
            {
                if (Regex.IsMatch(tbBuscar.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]") == false)
                {
                    MessageBox.Show("Para buscar por Nombre o Apellido\nsolo se deben ingresar letras!\n(evite espacios)", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbBuscar.Clear();
                    tbBuscar.Focus();
                    return;
                }
                else if (tbBuscar.Text.Length > 25)
                {
                    MessageBox.Show("Por favor, no ingrese tantas letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbBuscar.Focus();
                    LimpiarData();
                    return;
                }
                else
                {
                    GridDatos.ItemsSource = objeto_CN_Usuarios.Buscar(tbBuscar.Text).DefaultView;
                    LimpiarData();
                    if (GridDatos.Items.Count == 0)
                    {
                        MessageBox.Show("No se encontraron resultados", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarDatos();
                    }
                }

            }
            else if (cbFiltroTipo.Text != "")
            {
                GridDatos.ItemsSource = objeto_CN_Usuarios.Filtro(cbFiltroTipo.Text).DefaultView;
                LimpiarData();
                if (GridDatos.Items.Count == 0)
                {
                    MessageBox.Show("No se encontraron resultados", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarDatos();
                }

            }
            else
            {
                MessageBox.Show("Se deben ingresar datos para buscar usuario", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                CargarDatos();
            }
        }
        #endregion
    }
}
