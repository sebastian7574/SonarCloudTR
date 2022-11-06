using CapaDeEntidad.Clases;
using CapaDeNegocio.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TurismoReal.Vistas.VistasAdmin
{

    public partial class CRUDusuarios : Page
    {
        readonly CN_Usuarios objeto_CN_Usuarios = new CN_Usuarios();
        readonly CE_Usuarios objeto_CE_Usuarios = new CE_Usuarios();
        readonly CN_TipoUsuarioFK objeto_CN_TipoUsuarioFK = new CN_TipoUsuarioFK();

        #region Inicial
        public CRUDusuarios()
        {
            InitializeComponent();
            CargarCB();
        }
        #endregion

        #region Regresar
        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new Usuarios();
        }
        #endregion

        #region Cargar FK
        void CargarCB()
        {
            List<string> tipousuario = objeto_CN_TipoUsuarioFK.ListarTiposUsuario();
            for (int i = 0; i < tipousuario.Count; i++)
            {
                cbTipoUsuario.Items.Add(tipousuario[i]);
            }
        }
        #endregion

        #region Validaciones generales

        #region VALIDAR CAMPOS VACÍOS
        public bool CamposLlenos()
        {
            if (tbNombre.Text == ""
                || tbApellido.Text == ""
                || tbCel.Text == ""
                || tbCorreo.Text == ""
                || tbPais.Text == ""
                || tbRut.Text == ""
                || tbUser.Text == ""
                || cbTipoUsuario.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region VALIDAR RUT
        public bool ValidarRut(string rut)
        {
            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {

            }
            return validacion;

        }

        #endregion

        #region VALIDAR CORREO
        public static bool ValidarCorreo(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                //Normalizar el dominio
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                //Examina la parte del dominio del correo electrónico y lo normaliza
                string DomainMapper(Match match)
                {
                    //Utilice la clase IdnMapping para convertir los nombres de dominio Unicode
                    var idn = new IdnMapping();

                    // Extraer y procesar el nombre de dominio (lanza ArgumentException en caso de no ser válido)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        #endregion


        #endregion

        #region CRUD

        public int idUsuario;
        public string Patron = "Portafolio";
        
        #region CREAR
        private void Crear(object sender, RoutedEventArgs e)
        {
            //Validaciones
            #region NOMBRE Y APELLIDO
            //NOMBRE
            if (tbNombre.Text == "")
            {
                MessageBox.Show("El nombre no puede quedar vacio", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbNombre.Focus();
                return;
            }
            else if (tbNombre.Text != "")
            {
                if (tbNombre.Text.Length > 25)
                {
                    MessageBox.Show("El nombre es demasiado extenso", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbNombre.Clear();
                    tbNombre.Focus();
                    return;
                }
                else if (tbNombre.Text.Length < 3)
                {
                    MessageBox.Show("El nombre es muy corto, asegurese de ingresarlo bien", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbNombre.Clear();
                    tbNombre.Focus();
                    return;
                }
                //valido que se ingresen solo letras
                else if (Regex.IsMatch(tbNombre.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]+$") == false)
                {
                    MessageBox.Show("El nombre solo puede contener letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbNombre.Clear();
                    tbNombre.Focus();
                    return;
                }
            }

            //APELLIDO
            if (tbApellido.Text == "")
            {
                MessageBox.Show("El Apellido no puede quedar vacio", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbApellido.Focus();
                return;
            }
            else if (tbApellido.Text != "")
            {
                if (tbApellido.Text.Length > 25)
                {
                    MessageBox.Show("El apellido es demasiado extenso", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbApellido.Clear();
                    tbApellido.Focus();
                    return;
                }
                else if (tbApellido.Text.Length < 3)
                {
                    MessageBox.Show("El apellido es muy corto, asegurese de que este bien", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbApellido.Clear();
                    tbApellido.Focus();
                    return;
                }
                //valido que se ingresen solo letras
                else if (Regex.IsMatch(tbApellido.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]+$") == false)
                {
                    MessageBox.Show("El Apellido solo puede contener letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbApellido.Clear();
                    tbApellido.Focus();
                    return;
                }
            }
            #endregion

            #region RUT O PASAPORTE
            if (chkPasaporte.IsChecked == false && tbRut.Text == "")
            {
                MessageBox.Show("El rut no puede quedar vacío", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbRut.Focus();

                if (ValidarRut(tbRut.Text.ToString()) == false)
                {
                    MessageBox.Show("Resvise que el rut este bien ingresado", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbRut.Clear();
                    tbRut.Focus();
                    return;
                }
                else if (tbRut.Text.Length < 8)
                {
                    MessageBox.Show("Ingrese todos los digitos de su rut", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbRut.Clear();
                    tbRut.Focus();
                }
                else if (tbRut.Text.Length > 9)
                {
                    MessageBox.Show("Ingrese solo los digitos de su rut, sin puntos ni guiones", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbRut.Clear();
                    tbRut.Focus();
                }
            }
            else
            {
                if (tbRut.Text == "")
                {
                    MessageBox.Show("El N° pasaporte no puede quedar vacío", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbRut.Focus();
                }
                else if (tbRut.Text.Length > 9 || tbRut.Text.Length < 9)
                {
                    MessageBox.Show("Asegurese de ingresar bien el N° Pasaporte", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbRut.Clear();
                    tbRut.Focus();
                }
            }
            #endregion

            #region CORREO
             if (tbCorreo.Text == "")
             {
                 MessageBox.Show("El Correo no puede quedar vacio", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                 tbCorreo.Focus();
                 return;
             }
            else if (ValidarCorreo(tbCorreo.Text.ToString()) == false)
            {
                MessageBox.Show("Ingresar formato de correo", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCorreo.Clear();
                tbCorreo.Focus();
                return;
            }
            #endregion

            #region CELULAR
            else if (tbCel.Text == "")
            {
                MessageBox.Show("Ingrese número de celular", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCel.Focus();
                return;
            }
            else if (Regex.IsMatch(tbCel.Text, @"^[z0-9]+$") == false )
            {
                MessageBox.Show("Ingrese solo números para el celular", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCel.Clear();
                tbCel.Focus();
                return;
            }
            else if (tbCel.Text.Length < 9 || tbCel.Text.Length > 9)
            {
                MessageBox.Show("Ingrese los 9 dígitos de su celular", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCel.Clear();
                tbCel.Focus();
                return;
            }
            #endregion

            #region PAIS
            else if (tbPais.Text == "")
            {
                MessageBox.Show("Ingrese un País", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPais.Focus();
                return;
            }

            else if (Regex.IsMatch(tbPais.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$") == false)
            {
                MessageBox.Show("El País solo puede contener letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPais.Clear();
                tbPais.Focus();
                return;
            }

            else if (tbPais.Text.Length < 4)
            {
                MessageBox.Show("Asegurese que el nombre del País este correcto", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPais.Focus();
                return;
            }
            #endregion

            #region USER NAME
            else if (tbUser.Text == "")
            {
                MessageBox.Show("El usuario no puede quedar vacio", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbUser.Focus();
                return;
            }
            else if (tbUser.Text.Length < 1 || tbUser.Text.Length < 2)
            {
                MessageBox.Show("El nombre de usuario no puede ser tan corto", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbUser.Clear();
                tbUser.Focus();
                return;
            }
            else if (tbUser.Text.Length > 25)
            {
                MessageBox.Show("El nombre de usuario es demasiado extenso", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbUser.Clear();
                tbUser.Focus();
                return;
            }
            else if (Regex.IsMatch(tbUser.Text, @"^[A-Za-z0-9_.]+$") == false)
            {
                MessageBox.Show("El usuario puede contener letras, números, puntos y guiones bajos", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbUser.Clear();
                tbUser.Focus();
                return;
            }
            #endregion

            #region TIPO USUARIO
            else if (cbTipoUsuario.Text == "")
            {
                MessageBox.Show("Debe seleccionar un tipo de usuario!!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            #endregion

            #region CONTRASEÑA
            else if (tbContrasena.Password.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener más de 6 caracteres", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (tbContrasena.Password == "")
            {
                MessageBox.Show("La contraseña debe tener más de 6 caracteres", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            #endregion

            #region ESTADO DE CUENTA
            else if (chkHabilitar.IsChecked == false)
            {
                MessageBox.Show("Se ingreso un usuario con su cuenta deshabilitada", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            #endregion


            if (CamposLlenos() == true)
            {
                try
                {
                    int tipousuario = objeto_CN_TipoUsuarioFK.idTipoUsuario(cbTipoUsuario.Text);

                    objeto_CE_Usuarios.Nombres = tbNombre.Text;
                    objeto_CE_Usuarios.Apellidos = tbApellido.Text;
                    objeto_CE_Usuarios.Usuario = tbUser.Text;
                    objeto_CE_Usuarios.Correo = tbCorreo.Text;
                    objeto_CE_Usuarios.Contrasena = tbContrasena.Password;
                    objeto_CE_Usuarios.Patron = Patron;
                    objeto_CE_Usuarios.Identificacion = tbRut.Text;
                    objeto_CE_Usuarios.Celular = tbCel.Text;
                    objeto_CE_Usuarios.Pais = tbPais.Text;
                    objeto_CE_Usuarios.CodigoVerificacion = " ";
                    if (chkHabilitar.IsChecked == true)
                    {
                        objeto_CE_Usuarios.Habilitada = "Habilitado";
                    }
                    else
                    {
                        objeto_CE_Usuarios.Habilitada = "Deshabilitado";
                    }

                    if (chkPasaporte.IsChecked == true)
                    {
                        objeto_CE_Usuarios.EsPasaporte = "Pasaporte";
                    }
                    else
                    {
                        objeto_CE_Usuarios.EsPasaporte = "Rut";
                    }


                    objeto_CE_Usuarios.IdTipoUsuario = tipousuario;

                    objeto_CN_Usuarios.Insertar(objeto_CE_Usuarios);
                    MessageBox.Show("Se ha ingresado un nuevo usuario!", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                    Content = new Usuarios();
                }
                catch
                {
                    MessageBox.Show("Revise bien sus datos", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("No se pudo ingresar usuario, revise que ningun dato quede vacío!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region READ
        public void Consultar()
        {
            var a = objeto_CN_Usuarios.Consulta(idUsuario);
            tbNombre.Text = a.Nombres.ToString();
            tbApellido.Text = a.Apellidos.ToString();
            tbUser.Text = a.Usuario.ToString();
            tbCorreo.Text = a.Correo.ToString();
            tbRut.Text = a.Identificacion.ToString();
            tbCel.Text = a.Celular.ToString(); 
            tbPais.Text = a.Pais.ToString();

            var b = objeto_CN_TipoUsuarioFK.nombreTipoUsuario(a.IdTipoUsuario);
            cbTipoUsuario.Text = b.TipoUsuario;

            if (a.Habilitada == "Habilitado")
            {
                chkHabilitar.IsChecked = true;
            }
            else
            {
                chkHabilitar.IsChecked = false;
            }

            if (a.EsPasaporte == "Pasaporte")
            {
                chkPasaporte.IsChecked = true;
            }
            else
            {
                chkPasaporte.IsChecked = false;
            }

        }
        #endregion

        #region UPDATE
        private void Actualizar(object sender, RoutedEventArgs e)
        {
            //Validaciones
            #region NOMBRE Y APELLIDO
            //NOMBRE
            if (tbNombre.Text == "")
            {
                MessageBox.Show("El nombre no puede quedar vacio", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbNombre.Focus();
                return;
            }
            else if (tbNombre.Text != "")
            {
                if (tbNombre.Text.Length > 25)
                {
                    MessageBox.Show("El nombre es demasiado extenso", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbNombre.Clear();
                    tbNombre.Focus();
                    return;
                }
                else if (tbNombre.Text.Length < 3)
                {
                    MessageBox.Show("El nombre es muy corto, asegurese de ingresarlo bien", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbNombre.Clear();
                    tbNombre.Focus();
                    return;
                }
                //valido que se ingresen solo letras
                else if (Regex.IsMatch(tbNombre.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]+$") == false)
                {
                    MessageBox.Show("El nombre solo puede contener letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbNombre.Clear();
                    tbNombre.Focus();
                    return;
                }
            }

            //APELLIDO
            if (tbApellido.Text == "")
            {
                MessageBox.Show("El Apellido no puede quedar vacio", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbApellido.Focus();
                return;
            }
            else if (tbApellido.Text != "")
            {
                if (tbApellido.Text.Length > 25)
                {
                    MessageBox.Show("El apellido es demasiado extenso", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbApellido.Clear();
                    tbApellido.Focus();
                    return;
                }
                else if (tbApellido.Text.Length < 3)
                {
                    MessageBox.Show("El apellido es muy corto, asegurese de que este bien", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbApellido.Clear();
                    tbApellido.Focus();
                    return;
                }
                //valido que se ingresen solo letras
                else if (Regex.IsMatch(tbApellido.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]+$") == false)
                {
                    MessageBox.Show("El Apellido solo puede contener letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbApellido.Clear();
                    tbApellido.Focus();
                    return;
                }
            }
            #endregion

            #region RUT O PASAPORTE
            if (chkPasaporte.IsChecked == false && tbRut.Text == "")
            {
                MessageBox.Show("El rut no puede quedar vacío", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbRut.Focus();

                if (ValidarRut(tbRut.Text.ToString()) == false)
                {
                    MessageBox.Show("Resvise que el rut este bien ingresado", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbRut.Clear();
                    tbRut.Focus();
                    return;
                }
                else if (tbRut.Text.Length < 8)
                {
                    MessageBox.Show("Ingrese todos los digitos de su rut", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbRut.Clear();
                    tbRut.Focus();
                }
                else if (tbRut.Text.Length > 9)
                {
                    MessageBox.Show("Ingrese solo los digitos de su rut, sin puntos ni guiones", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbRut.Clear();
                    tbRut.Focus();
                }
            }
            else
            {
                if (tbRut.Text == "")
                {
                    MessageBox.Show("El N° pasaporte no puede quedar vacío", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbRut.Focus();
                }
                else if (tbRut.Text.Length > 9 || tbRut.Text.Length < 9)
                {
                    MessageBox.Show("Asegurese de ingresar bien el N° Pasaporte", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbRut.Clear();
                    tbRut.Focus();
                }
            }
            #endregion

            #region CORREO
            if (tbCorreo.Text == "")
            {
                MessageBox.Show("El Correo no puede quedar vacio", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCorreo.Focus();
                return;
            }
            else if (ValidarCorreo(tbCorreo.Text.ToString()) == false)
            {
                MessageBox.Show("Ingresar formato de correo", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCorreo.Clear();
                tbCorreo.Focus();
                return;
            }
            #endregion

            #region CELULAR
            else if (tbCel.Text == "")
            {
                MessageBox.Show("Ingrese número de celular", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCel.Focus();
                return;
            }
            else if (Regex.IsMatch(tbCel.Text, @"^[z0-9]+$") == false)
            {
                MessageBox.Show("Ingrese solo números para el celular", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCel.Clear();
                tbCel.Focus();
                return;
            }
            else if (tbCel.Text.Length < 9 || tbCel.Text.Length > 9)
            {
                MessageBox.Show("Ingrese los 9 dígitos de su celular", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbCel.Clear();
                tbCel.Focus();
                return;
            }
            #endregion

            #region PAIS
            else if (tbPais.Text == "")
            {
                MessageBox.Show("Ingrese un País", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPais.Focus();
                return;
            }

            else if (Regex.IsMatch(tbPais.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$") == false)
            {
                MessageBox.Show("El País solo puede contener letras", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPais.Clear();
                tbPais.Focus();
                return;
            }

            else if (tbPais.Text.Length < 4)
            {
                MessageBox.Show("Asegurese que el nombre del País este correcto", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPais.Focus();
                return;
            }
            #endregion

            #region USER NAME
            else if (tbUser.Text == "")
            {
                MessageBox.Show("El usuario no puede quedar vacio", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbUser.Focus();
                return;
            }
            else if (tbUser.Text.Length < 1 || tbUser.Text.Length < 2)
            {
                MessageBox.Show("El nombre de usuario no puede ser tan corto", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbUser.Clear();
                tbUser.Focus();
                return;
            }
            else if (tbUser.Text.Length > 25)
            {
                MessageBox.Show("El nombre de usuario es demasiado extenso", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbUser.Clear();
                tbUser.Focus();
                return;
            }
            else if (Regex.IsMatch(tbUser.Text, @"^[A-Za-z0-9_.]+$") == false)
            {
                MessageBox.Show("El usuario puede contener letras, números, puntos y guiones bajos", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbUser.Clear();
                tbUser.Focus();
                return;
            }
            #endregion

            #region TIPO USUARIO
            else if (cbTipoUsuario.Text == "")
            {
                MessageBox.Show("Debe seleccionar un tipo de usuario!!", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            #endregion

            #region ESTADO DE CUENTA
            else if (chkHabilitar.IsChecked == false)
            {
                MessageBox.Show("Se ingreso un usuario con su cuenta deshabilitada", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            #endregion

            if (CamposLlenos() == true)
            {
                int tipousuario = objeto_CN_TipoUsuarioFK.idTipoUsuario(cbTipoUsuario.Text);

                objeto_CE_Usuarios.IdUsuario = idUsuario;
                objeto_CE_Usuarios.Nombres = tbNombre.Text;
                objeto_CE_Usuarios.Apellidos = tbApellido.Text;
                objeto_CE_Usuarios.Usuario = tbUser.Text;
                objeto_CE_Usuarios.Correo = tbCorreo.Text;
                objeto_CE_Usuarios.Contrasena = tbContrasena.Password;
                objeto_CE_Usuarios.Patron = Patron;
                objeto_CE_Usuarios.Identificacion = tbRut.Text;
                objeto_CE_Usuarios.Celular = tbCel.Text;
                objeto_CE_Usuarios.Pais = tbPais.Text;
                objeto_CE_Usuarios.CodigoVerificacion = " ";
                if (chkHabilitar.IsChecked == true)
                {
                    objeto_CE_Usuarios.Habilitada = "Habilitado";
                }
                else
                {
                    objeto_CE_Usuarios.Habilitada = "Deshabilitado";
                }

                if (chkPasaporte.IsChecked == true)
                {
                    objeto_CE_Usuarios.EsPasaporte = "Pasaporte";
                }
                else
                {
                    objeto_CE_Usuarios.EsPasaporte = "Rut";
                    if (ValidarRut(tbRut.Text.ToString()) == false)
                    {
                        MessageBox.Show("Ingrese correctamente el rut", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                objeto_CE_Usuarios.IdTipoUsuario = tipousuario;

                objeto_CN_Usuarios.ActualizarDatos(objeto_CE_Usuarios);
                MessageBox.Show("Se han actualizado los datos exitosamente!", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                Content = new Usuarios();
            }
            else
            {
                MessageBox.Show("Por favor, no dejar campos vacios", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if(tbContrasena.Password.Length > 6)
            {
                objeto_CE_Usuarios.IdUsuario = idUsuario;
                objeto_CE_Usuarios.Contrasena = tbContrasena.Password;
                objeto_CE_Usuarios.Patron = Patron;

                objeto_CN_Usuarios.ActualizarPass(objeto_CE_Usuarios);
                Content = new Usuarios();
            }

        }
        #endregion

        #endregion

        #region ACTUALIZAR CONTRASEÑA

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            tbContrasena.Visibility = Visibility.Visible;
            txtContra.Visibility = Visibility.Visible;
            MessageBox.Show("Ingrese una contraseña de 6 caracteres");
            
        }
        #endregion

    }
}
