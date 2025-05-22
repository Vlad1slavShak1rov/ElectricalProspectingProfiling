using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Microsoft.EntityFrameworkCore;
using ScottPlot;


namespace ElectricalProspectingProfiling.Model
{
    /// <summary>
    /// Логика взаимодействия для SapiensApp.xaml
    /// </summary>
    public partial class SapiensApp : Window
    {
        private Square square;
        private List<Contract> contracts = new();
        public SapiensApp()
        {
            InitializeComponent();
            LoadData();
        }
    
        private async Task InitPlotSquare()
        {
            using var context = new MyDBContext();
            var coordinatsSquare = await context.CoordinatsSquare.Where(c => c.SquareID == square.ID).ToListAsync();
            
            Coordinates[] points = new Coordinates[coordinatsSquare.Count + 1];

            for (int i = 0; i < coordinatsSquare.Count; i++)
            {
                var coord = coordinatsSquare[i];
                points[i] = new Coordinates() { X = (double)coord.XКоордината, Y = (double)coord.YКоордината };
            }

            points[coordinatsSquare.Count] = points[0];

            squarePlot.Plot.Clear();
            squarePlot.Plot.Add.Polygon(points);

            squarePlot.Plot.Title($"Рисунок площади {square.Название}");
            squarePlot.Plot.XLabel("Х");
            squarePlot.Plot.YLabel("Y");

            squarePlot.Refresh();
        }

        private async Task InitPlotProfile()
        {
            pltProfile.Plot.Clear();
            using var context = new MyDBContext();
            var profiles = await context.Profile.Where(p=>p.ПлощадьID == square.ID).ToListAsync();

            double[] xc = new double[profiles.Count];
            double[] yc = new double[profiles.Count];

            int i = 0;
            foreach (var profile in profiles)
            {
                var profileCoordinats = await context.CoordinatsProfile.FirstOrDefaultAsync(p => p.ID == profile.КоординатыID);

                xc[i] = profileCoordinats.XКоордината;
                yc[i] = profileCoordinats.YКоордината;

                i++;
            }
          

            pltProfile.Plot.Title($"Профиль площади {square.Название}");
            pltProfile.Plot.Add.Scatter(xc,yc,ScottPlot.Colors.Black);
        }
        private async Task LoadData()
        {
            using var context = new MyDBContext();
            await LoadGeodesist(context);
            await LoadSquare(context);
            await LoadCustomer(context);
            await LoadContracts(context);
            await LoadGeologicalData(context);
        }

        private async Task LoadGeologicalData(MyDBContext context)
        {
            RepositoryGeologicalData repositoryGeologicalData = new(context);
            var geolocicalDatas = await context.GeologicalData.Include(g => g.Geodesist).ToListAsync();
            dgGeologicalData.ItemsSource = geolocicalDatas;
        }

        private async Task LoadContracts(MyDBContext context)
        {
            contracts = await context.Contracts.Include(c => c.Customer).Include(s => s.Square).ToListAsync();
            dgContracts.ItemsSource = contracts;
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
            if (square == null) return;
            await LoadPicketProfile(square);
            await InitPlotSquare();
            await InitPlotProfile();

            dgAreas.SelectedItem = null;
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
            dgCustomers.SelectedItem = null;
        }

        private async void btDrawUpContract_Click(object sender, RoutedEventArgs e)
        {
            AddContractWindow addContractWindow = new();
            var result = addContractWindow.ShowDialog();
            if(result == true)
            {
                using var context = new MyDBContext();
                {
                    await LoadContracts(context);
                }
              
            }
        }

        private async void btAddData_Click(object sender, RoutedEventArgs e)
        {
            AddDataWindow addDataWindow = new();
            var result = addDataWindow.ShowDialog();
            if(result == true)
            {
                using var context = new MyDBContext();
                await LoadGeologicalData(context);
            }
        }

        private void dgGeologicalData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var selectedItem = dgGeologicalData.SelectedItem;
            if (selectedItem == null) return;

            var geologicalData = selectedItem as GeologicalData;
            MeasurmentWindow measurement = new(geologicalData);
            measurement.ShowDialog();
            dgGeologicalData.SelectedItem = null;
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgContracts == null) return;
            int selectedIndex = cbSort.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    dgContracts.ItemsSource = contracts.OrderBy(c => c.НачалоДата);
                    break;

                case 1:
                    dgContracts.ItemsSource = contracts.OrderByDescending(c => c.НачалоДата);
                    break;
            }
        }

        private async void btCreateSintData_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new();
            using var context = new MyDBContext();
            RepositoryGeodesist repositoryGeodesist = new(context);

            Geodesist geodesist = new()
            {
                Имя = "Васильев Петр Иванович",
                Контакты = "383-12-34"
            };

            await repositoryGeodesist.Add(geodesist);

            RepositoryCustomer repositoryCustomer = new(context);

            Customer customer = new()
            {
                Имя = $"ООО ГорноДобПром {rnd.Next(0, 99999)}",
                Контакты = "432-23-21"
            };

            await repositoryCustomer.Add(customer);

            RepositorySquare repositorySquare = new(context);

            Square square = new()
            {
                Название = $"ПромЗона {rnd.Next(0, 99999)}",
                Высота = 208
            };

            await repositorySquare.Add(square);

            RepositoryGeologicalData repositoryGeologicalData = new(context);

            GeologicalData geologicalData = new()
            {
                ГеодезистID = geodesist.ID,
                ТипПороды = "Грунт",
                ОписаниеСтруктуры = "Твердый грунт",
                Загрязнение = "Чистое"
            };

            await repositoryGeologicalData.Add(geologicalData);

            RepositoryCoordinatsProfile repositoryCoordinatsProfile = new(context);
            RepositoryProfile repositoryProfile = new(context);
            RepositoryPicket repositoryPicket = new(context);
            RepositoryMeasurement repositoryMeasurement = new(context);
            RepositoryCoordinatsSquare repositoryCoordinatsSquare = new(context);

            Profile profile = new()
            {
                ПлощадьID = square.ID,
                МетодПрофилирования = "Поляризационный",
            };

            int count = rnd.Next(4, 10);

            for (int i = 0; i < count; i++)
            {
                CoordinatsSquare coordinatsSquare = new()
                {
                    XКоордината = rnd.Next(0, 100),
                    YКоордината = rnd.Next(0, 100),
                    SquareID = square.ID,
                };

                await repositoryCoordinatsSquare.Add(coordinatsSquare);
                CoordinatsProfile coordinatsProfile = new()
                {
                    XКоордината = rnd.Next(0, 100),
                    YКоордината = rnd.Next(0, 100),
                };

                await repositoryCoordinatsProfile.Add(coordinatsProfile);

                profile.КоординатыID = coordinatsProfile.ID;

                await repositoryProfile.Update(profile);
                Picket picket = new()
                {
                    ПрофильID = profile.ID,
                    Координаты = $"{rnd.Next(0,10)} {rnd.Next(0, 10)} ",
                    Номер = i+1,
                    Дистанция = rnd.Next(0, 100)
                };

                await repositoryPicket.Add(picket);

                Measurement measurement = new()
                {
                    ГеологическиеДанныеID = geologicalData.ID,
                    ПикетыID = picket.ID,
                    Дата = DateTime.Now.Date,
                    ТипПрофилирования = "Электротомография",
                    ДистанцияМеждуЭлектродами = rnd.Next(0, 32),
                    Ток = rnd.Next(1, 128),
                    Вольтаж = rnd.Next(128,256),
                };

                measurement.Сопротивление = measurement.Вольтаж / measurement.Ток;

                await repositoryMeasurement.Add(measurement);
            }

            RepositoryContractL repositoryContract = new(context);

            Contract contract = new()
            {
                КлиентID = customer.ID,
                ГеологическиеДанныеID = geologicalData.ID,
                ПлощадьID = square.ID,
                Контакты = customer.Контакты,
                НачалоДата = DateTime.Parse($"{rnd.Next(0, 30)}.{rnd.Next(1, 5)}.2025"),
                КонецДата = DateTime.Parse($"{rnd.Next(0, 30)}.{rnd.Next(5, 12)}.2025"),
            };

            repositoryContract.Add(contract);

            MessageBox.Show("Синтетические данные добавлены!");
            await LoadData();
        }
    }
}
