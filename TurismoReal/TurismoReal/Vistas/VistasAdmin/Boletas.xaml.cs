using CapaDeNegocio.Clases;
using NPOI.SS.Formula.Functions;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TurismoReal.Vistas.VistasAdmin
{
    /// <summary>
    /// Lógica de interacción para Boletas.xaml
    /// </summary>
    public partial class Boletas : UserControl
    {
        readonly CN_Boletas objeto_CN_Boletas = new CN_Boletas();

        public Boletas()
        {
            InitializeComponent();
            CargarDatos();
        }

        #region CARGAR BOLETAS
        void CargarDatos()
        {
            GridDatos.ItemsSource = objeto_CN_Boletas.CargarBoletas().DefaultView;
        }
        #endregion

        #region Limpiar
        public void LimpiarData()
        {
            tbBuscar.Clear();
        }

        #endregion
        private void Ver(object sender, RoutedEventArgs e)
        {
            if (tbBuscar.Text != "")
            {
                GridDatos.ItemsSource = objeto_CN_Boletas.Buscar(tbBuscar.Text).DefaultView;
                if (GridDatos.Items.Count == 0)
                {
                    MessageBox.Show("No se encontraron resultados", "INFORMACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarDatos();
                }
            }
            else
            {
                MessageBox.Show("Ingrese N° de comprobante para buscar", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                CargarDatos();
            }
            LimpiarData();
        }
    }

}
