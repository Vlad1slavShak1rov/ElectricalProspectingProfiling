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
using ElectricalProspectingProfiling.Tools;
using ElectricalProspectingProfiling.Windows;

namespace ElectricalProspectingProfiling.Model
{
    /// <summary>
    /// Логика взаимодействия для SapiensApp.xaml
    /// </summary>
    public partial class SapiensApp : Window
    {
        private Square square;
        public SapiensApp()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            using var context = new MyDBContext();
            await LoadGeodesist(context);
            await LoadSquare(context);
            await LoadCustomer(context);
        }

        private async Task LoadCustomer(MyDBContext context)
        {
            RepositoryCustomer repositoryCustomer = new(context);
            dgCustomers.ItemsSource = await repositoryCustomer.GetAll();
        }

        private async Task LoadSquare(MyDBContext context)
        {
            RepositorySquare repositorySquare = new(context);
            dgAreas.ItemsSource = await repositorySquare.GetAll();
        }
        private async Task LoadGeodesist(MyDBContext context)
        {
            RepositoryGeodesist repositoryGeodesist = new(context);
            dgSurveyors.ItemsSource = await repositoryGeodesist.GetAll();
        }

        private async Task LoadPicketProfile(Square square)
        {
            using var context = new MyDBContext();

            RepositoryPicket repositoryPicket = new(context);
            RepositoryProfile repositoryProfile = new(context);

            var profiles = await repositoryProfile.GetBySquareID(square.ID);
            dgProfiles.ItemsSource = profiles;

            var pickets = await repositoryPicket.GetByPicketID(profiles[0].ID);
            dgPickets.ItemsSource = pickets;
        }

        private async void addGeodesistButton_Click(object sender, RoutedEventArgs e)
        {
            AddGeodesistWindow addCustomerWindow = new();
            var result = addCustomerWindow.ShowDialog();
            if(result == true)
            {
                using var context = new MyDBContext();
                await LoadGeodesist(context);
            }
        }

        private async void addSquareButton_Click(object sender, RoutedEventArgs e)
        {
            AddSquareWindow addSquareWindow = new();
            var result = addSquareWindow.ShowDialog();
            if(result == true)
            {
                using var context = new MyDBContext();
                await LoadSquare(context);
            }
        }

        private async void dgAreas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            square = dgAreas.SelectedItem as Square;
            await LoadPicketProfile(square);

            MessageShow.Information("Успешно!");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            string info = $"Данная программа разработана для просмотра результатов изучения методом СЭП";
            MessageShow.Information(info);
        }

        private async void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerWindow addCustomerWindow = new();
            var result = addCustomerWindow.ShowDialog();

            if(result == true)
            {
                using var context = new MyDBContext();
                await LoadCustomer(context);
            }
        }

        private async void dgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = dgCustomers.SelectedItem as Customer;
            if (selectedItem == null) return;

            AddCustomerWindow addCustomerWindow = new(selectedItem);
            var result = addCustomerWindow.ShowDialog();

            if (result == true)
            {
                using var context = new MyDBContext();
                {
                    await LoadCustomer(context);
                }
            }
        }

        private void btDrawUpContract_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
