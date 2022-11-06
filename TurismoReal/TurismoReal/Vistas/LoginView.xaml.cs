using CapaDeNegocio.Clases;
using System.Windows;
using System.Windows.Input;

namespace TurismoReal.Vistas
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            txtUser.Focus();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUser.Text != "" && txtPass.Password != "")
            {

                Login(txtUser.Text, txtPass.Password);
            }
            else
            {
                MessageBox.Show("No pueden quedar campos vacios");
            }
            
        }

        void Login(string usuario, string contra)
        { 
            CN_Usuarios cn = new CN_Usuarios();
            var a = cn.LogIn(usuario, contra);

            if (a.IdUsuario > 0)
            {
                Properties.Settings.Default.IdUsuario = a.IdUsuario;
                Properties.Settings.Default.IdTipoUsuario = a.IdTipoUsuario;

                if (a.IdTipoUsuario == 1)
                {
                    InicioAdmin inicioAdmin = new InicioAdmin();
                    inicioAdmin.Show();
                    this.Close();
                }
                else
                {
                    InicioFuncionario inicioFuncionario = new InicioFuncionario();
                    inicioFuncionario.Show();
                    this.Close();
                }
            }
            else 
            {
                MessageBox.Show("Datos incorrectos\nIntentalo denuevo");
            }
        }       
    }
}
