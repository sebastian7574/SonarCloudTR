using System.Windows;
using System.Windows.Input;
using TurismoReal.Vistas.VistasAdmin;

namespace TurismoReal.Vistas
{
    /// <summary>
    /// Lógica de interacción para InicioAdmin.xaml
    /// </summary>
    public partial class InicioAdmin : Window
    {
        public InicioAdmin()
        {
            InitializeComponent();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de cerrar la aplicación?", "AVISO", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void TBShow(object sender, RoutedEventArgs e)
        {
            GridContent.Opacity = 0.5;
        }

        private void TBHide(object sender, RoutedEventArgs e)
        {
            GridContent.Opacity = 1;
        }

        private void PreviewMLBD(object sender, MouseButtonEventArgs e)
        {
            BtnShowHide.IsChecked = false;
        }

        private void Usuarios_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Usuarios();
        }

        private void Departamentos_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Departamentos();
        }

        private void Inventario_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Inventario();
        }

        private void Gasto_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Gastos();
        }

        private void Cliente_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Clientes();
        }

        private void Servicio_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Servicios();
        }

        private void Boletas_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Boletas();
        }
        private void Reservas_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Reservas();
        }

        private void Inicio_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Inicio();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void BtnCerrarSesion(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cerrar sesión?", "AVISO", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LoginView lg = new LoginView();
                lg.Show();
                this.Close();
            }
        }

        private void Reportes(object sender, RoutedEventArgs e)
        {
            DataContext = new Reportes();
        }
    }
}
