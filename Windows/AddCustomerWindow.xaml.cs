using ElectricalProspectingProfiling.Database.context;
using ElectricalProspectingProfiling.Database.DAL;
using ElectricalProspectingProfiling.Model;
using ElectricalProspectingProfiling.Tools;
using Microsoft.Data.SqlClient;
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
        private Customer _customer;
        public AddCustomerWindow()
        {
            InitializeComponent();
        }
        public AddCustomerWindow(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
            addButton.Content = "Изменить";
            nameBox.Text = _customer.Имя;
            contactBox.Text = _customer.Контакты;
            brDeleteCustomer.Visibility = Visibility.Visible;
        }

        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameBox.Text;
            string contact = contactBox.Text;

            if(Validator.IsNullOrEmptyOrWhiteSpace(name, contact))
            {
                MessageShow.Information("У вас есть незаполненные поля!");
                return;
            }

            using (var context = new MyDBContext())
            {
                RepositoryCustomer repositoryCustomer = new(context);
                try
                {
                    if (_customer == null)
                    {
                        Customer customer = new()
                        {
                            Имя = name,
                            Контакты = contact
                        };
                        await repositoryCustomer.Add(customer);
                    }
                    else
                    {
                        _customer.Контакты = contact;
                        _customer.Имя = name;
                        await repositoryCustomer.Update(_customer);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
           

            MessageShow.Information("Успешно!");
            this.DialogResult = true;
            this.Close();
        }

        private async void brDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageShow.Question($"Вы уверены, что хотите удалить {_customer.Имя}?");
            if(result == MessageBoxResult.Yes)
            {
                using var context = new MyDBContext();
                RepositoryCustomer repositoryCustomer = new(context);
                await repositoryCustomer.Remove(_customer);

                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
