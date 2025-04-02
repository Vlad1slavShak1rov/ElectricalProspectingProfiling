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
using ElectricalProspectingProfiling.Database.context;
using ElectricalProspectingProfiling.Database.DAL;
using ElectricalProspectingProfiling.Windows;

namespace ElectricalProspectingProfiling.Model
{
    /// <summary>
    /// Логика взаимодействия для SapiensApp.xaml
    /// </summary>
    public partial class SapiensApp : Window
    {
        public SapiensApp()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            using var context = new MyDBContext();
            await LoadGeodesist(context);
            await LoadPicket(context);
        }

        private async Task LoadGeodesist(MyDBContext context)
        {
            RepositoryGeodesist repositoryGeodesist = new(context);
            dgPickets.ItemsSource = await repositoryGeodesist.GetAll();
        }

        private async Task LoadPicket(MyDBContext context)
        {
            RepositoryPicket repository = new(context);
            dgPickets.ItemsSource = await repository.GetAll();
        }
        private async void addGeodesistButton_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerWindow addCustomerWindow = new();
            var result = addCustomerWindow.ShowDialog();
            if(result == true)
            {
                using var context = new MyDBContext();
                await LoadGeodesist(context);
            }
        }
    }
}
