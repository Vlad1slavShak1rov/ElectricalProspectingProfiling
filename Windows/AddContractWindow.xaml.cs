using ElectricalProspectingProfiling.Database.context;
using ElectricalProspectingProfiling.Database.DAL;
using ElectricalProspectingProfiling.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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

namespace ElectricalProspectingProfiling.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddContractWindow.xaml
    /// </summary>
    public partial class AddContractWindow : Window
    {
        public AddContractWindow()
        {
            InitializeComponent();
            InitData();
        }
        private void InitData()
        {
            using var context = new MyDBContext();
            var customers = context.Customer.ToList();
            var geolociladDatas = context.GeologicalData.ToList();
            var square = context.Squares.ToList();

            if (customers.Count == 0) cbCustomer.Items.Add("Отсутствует");
            else InitComboBox<Customer>(customers, cbCustomer, "Имя");

            if (geolociladDatas.Count == 0) cbGeologicalData.Items.Add("Отсутствует");
            else InitComboBox<GeologicalData>(geolociladDatas, cbGeologicalData, "Info");

            if (square.Count == 0) cbSquare.Items.Add("Отсутствует");
            else InitComboBox<Square>(square, cbSquare, "Название");

            cbSquare.SelectedIndex = cbCustomer.SelectedIndex = cbGeologicalData.SelectedIndex = 0;
        }

        private void InitComboBox<T>(List<T> list, ComboBox comboBox, string displayMember) where T: class
        {
            comboBox.ItemsSource = list;
            comboBox.DisplayMemberPath = displayMember;
        }

        private async void btSave_Click(object sender, RoutedEventArgs e)
        {
            var square = cbSquare.SelectedItem;
            var geolocicalData = cbGeologicalData.SelectedItem;
            var customer = cbCustomer.SelectedItem;

            if(square is not Square)
            {
                MessageBox.Show("Создайте площадь!");
                return;
            }

            if(geolocicalData is not GeologicalData)
            {
                MessageBox.Show("Создайте исследование!");
                return;
            }

            if(customer is not Customer selectedCustomer)
            {
                MessageBox.Show("Заказчик должен быть обязательно выбран!");
                return;
            }

            if(dpStart.SelectedDate == null || dpEnd.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату!");
                return;
            }

            DateTime startDate = dpStart.SelectedDate.Value;
            DateTime endDate = dpEnd.SelectedDate.Value;
            
            if(startDate >= endDate)
            {
                MessageBox.Show("Начальная дата не может быть больше конечной!");
                return;
            }

            using var context = new MyDBContext();

            var contract = context.Contracts.FirstOrDefault(c => c.ПлощадьID == (square as Square).ID || c.ГеологическиеДанныеID == (geolocicalData as GeologicalData).ID);
            if(contract != null)
            {
                MessageBox.Show("На данную площадь и/или исследование уже создан контракт!");
                return;
            }
            contract = new()
            {
                КлиентID = selectedCustomer.ID,
                ГеологическиеДанныеID = geolocicalData is GeologicalData geologicalData ? geologicalData.ID : null,
                ПлощадьID = (square as Square).ID,
                Контакты = tbContacts.Text,
                НачалоДата = startDate.Date,
                КонецДата = endDate.Date
            };
            try
            {
                context.Contracts.Add(contract);
                context.SaveChanges();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            var profile = context.Profile.FirstOrDefault(p => p.ПлощадьID == (square as Square).ID);
            var pickets = await context.Picket.Where(p => p.ПрофильID == profile.ID).ToListAsync();

            RepositoryMeasurement repositoryMeasurement = new(context);
            foreach (var picket in pickets)
            {
                Measurement measurement = new()
                {
                    ГеологическиеДанныеID = contract.ГеологическиеДанныеID.Value,
                    ПикетыID = picket.ID,
                    Дата = DateTime.Now.Date,
                    ТипПрофилирования = "Отсутствует",
                    ДистанцияМеждуЭлектродами = 0,
                    Ток = 0,
                    Вольтаж = 0,
                    Сопротивление = 0,
                };

                await repositoryMeasurement.Add(measurement);
            }

            MessageBox.Show("Успешно!");
            this.DialogResult = true;
            this.Close();
        }

        private void cbCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCustomer = cbCustomer.SelectedItem;
            if(selectedCustomer is Customer customer)
            {
                tbContacts.Text = customer.Контакты;
            }
        }
    }
}
