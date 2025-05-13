using ElectricalProspectingProfiling.Database.context;
using ElectricalProspectingProfiling.Database.DAL;
using ElectricalProspectingProfiling.Model;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для MeasurmentWindow.xaml
    /// </summary>
    public partial class MeasurmentWindow : Window
    {
        private GeologicalData GeologicalData { get; set; }
        private Measurement CurrentMeasurement { get; set; }
        public MeasurmentWindow(GeologicalData geologicalData)
        {
            InitializeComponent();
            GeologicalData = geologicalData;
            tbAmperaget.TextInput += tbAmperaget_TextInput;
            tbVoltage.TextInput += tbVoltage_TextInput;
            InitData();
        }

        private async Task InitData()
        {
            using var context = new MyDBContext();
            var measurements = await context.Measurement.Where(m=>m.ГеологическиеДанныеID == GeologicalData.ID).ToListAsync();
            List<Picket> pickets = new();

            foreach(var measurement in measurements)
            {
                var picket = await context.Picket.FirstOrDefaultAsync(p => p.ID == measurement.ПикетыID);
                pickets.Add(picket);
            }

            cbPickets.ItemsSource = pickets;
            cbPickets.DisplayMemberPath = "ShowInfo";

        }

        private void cbPickets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var picket = cbPickets.SelectedItem as Picket;
            using var context = new MyDBContext();
            CurrentMeasurement = context.Measurement.FirstOrDefault(m=>m.ПикетыID == picket.ID);

            dpStartMeasurement.SelectedDate = CurrentMeasurement.Дата;
            cbTypeProfile.SelectedItem = CurrentMeasurement.ТипПрофилирования;
            tbDistance.Text = CurrentMeasurement.ДистанцияМеждуЭлектродами.ToString();
            tbAmperaget.Text = CurrentMeasurement.Ток.ToString();
            tbVoltage.Text = CurrentMeasurement.Вольтаж.ToString();
            tbResistance.Text = CurrentMeasurement.Сопротивление.ToString();
        }

        private void tbAmperaget_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumber(e.Text))
            {
                e.Handled = true;
                return;
            }
            
            if (tbVoltage.Text != string.Empty)
            {
                tbResistance.Text = (double.Parse(tbVoltage.Text) / double.Parse(e.Text)).ToString();
            }
        }

        private void tbVoltage_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumber(e.Text))
            {
                e.Handled = true;
                return;
            }
            
            if (tbAmperaget.Text != string.Empty)
            {
                tbResistance.Text = (double.Parse(e.Text) / double.Parse(tbAmperaget.Text)).ToString("F2");
            }
        }

        private bool IsNumber(string num) => int.TryParse(num, out _);

        private void tbAmperaget_TextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text == "0")
            {
                tb.Text = "1";
                MessageBox.Show("Деление на ноль невозможно!");
                e.Handled = true; 
                return;
            }

            if (string.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "1";
                e.Handled = true;
            }

        }

        private void tbVoltage_TextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text.Count() == 0)
            {
                tb.Text = "1";
            }
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            var selectedPicket = cbPickets.SelectedItem;
            if(selectedPicket is not Picket)
            {
                MessageBox.Show("Выберите пикет!");
                return;
            }

            selectedPicket = selectedPicket as Picket;
            string typeProfile = (cbTypeProfile.SelectedItem as ComboBoxItem).Content.ToString();
            int picketDistance;
            if(!int.TryParse(tbDistance.Text, out picketDistance))
            {
                MessageBox.Show("Введите корректное число!");
                return;
            }

            int amper = int.Parse(tbAmperaget.Text);
            int voltage = int.Parse(tbVoltage.Text);
            double res = double.Parse(tbResistance.Text);
            if(dpStartMeasurement.SelectedDate == null)
            {
                MessageBox.Show("Введите дату!");
                return;
            }
            DateTime date = dpStartMeasurement.SelectedDate.Value;
            using var context = new MyDBContext();

            CurrentMeasurement.Дата = date;
            CurrentMeasurement.ТипПрофилирования = typeProfile;
            CurrentMeasurement.ДистанцияМеждуЭлектродами = picketDistance;
            CurrentMeasurement.Ток = amper;
            CurrentMeasurement.Вольтаж = voltage;
            CurrentMeasurement.Сопротивление = res;

            context.Measurement.Update(CurrentMeasurement);
            context.SaveChanges();

            MessageBox.Show("Успешно!");

            this.DialogResult = true;
            this.Close();
        }
    }
}
