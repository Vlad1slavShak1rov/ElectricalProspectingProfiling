using ElectricalProspectingProfiling.Database.context;
using ElectricalProspectingProfiling.Database.DAL;
using ElectricalProspectingProfiling.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    /// Логика взаимодействия для AddDataWindow.xaml
    /// </summary>
    public partial class AddDataWindow : Window
    {
        public AddDataWindow()
        {
            InitializeComponent();
            InitData();
        }

        private async void InitData()
        {
            using var context = new MyDBContext();
            RepositoryGeodesist repositoryGeodesist = new(context);

            cbGeodesist.ItemsSource = await repositoryGeodesist.GetAll();
            cbGeodesist.DisplayMemberPath = "Имя";
        }
      
        private async void btCreateData_Click(object sender, RoutedEventArgs e)
        {
            var geodesist = cbGeodesist.SelectedItem as Geodesist;
            string typeRock = tbTypeRock.Text;
            string description = tbDescription.Text;
            string pollution = (cbPolution.SelectedItem as ComboBoxItem).Content.ToString();

            GeologicalData geologicalData = new()
            {
                ГеодезистID = geodesist.ID,
                ТипПороды = typeRock,
                ОписаниеСтруктуры = description,
                Загрязнение = pollution,
            };

            using var context = new MyDBContext();
            RepositoryGeologicalData repositoryGeologicalData = new(context);
            await repositoryGeologicalData.Add(geologicalData);

            MessageBox.Show("Геологические данные добавлены!");
            this.DialogResult = true;
            this.Close();
        }
    }
}
