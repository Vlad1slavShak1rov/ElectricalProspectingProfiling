using ElectricalProspectingProfiling.Database.context;
using ElectricalProspectingProfiling.Database.DAL;
using ElectricalProspectingProfiling.Model;
using ElectricalProspectingProfiling.Tools;
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
    /// Логика взаимодействия для AddCustomerWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {
        public AddCustomerWindow()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameBox.Text;
            string contact = contactBox.Text;

            if (Validator.IsNullOrEmptyOrWhiteSpace(name, contact))
            {
                MessageShow.Information("У вас есть незаполненные поля!");
                return;
            }

            using var context = new MyDBContext();
            RepositoryGeodesist repositoryGeodesist = new(context);

            repositoryGeodesist.Add(new Geodesist() { Имя = name, Контакты = contact});
            MessageShow.Information("Успешно!");
            this.DialogResult = true;
            this.Close();
        }
    }
}
