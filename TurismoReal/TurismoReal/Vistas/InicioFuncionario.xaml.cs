using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using TurismoReal.Vistas.VistasAdmin;
using TurismoReal.Vistas.VistasFuncionario;

namespace TurismoReal.Vistas
{
    /// <summary>
    /// Lógica de interacción para InicioFuncionario.xaml
    /// </summary>
    public partial class InicioFuncionario : Window
    {
        public InicioFuncionario()
        {
            InitializeComponent();
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

        private void CheckINOUT_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new CheckInOut();
        }

        private void Inicio_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Inicio();
        }

        private void Checklist_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new CheckList();
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

    }
}
