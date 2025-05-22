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
            InitData();
        }

        private async Task InitData()
        {
            using var context = new MyDBContext();
            var measurements = await context.Measurement.Where(m=>m.ГеологическиеДанныеID == GeologicalData.ID).ToListAsync();
            List<Picket> pickets = new();
            await PlotMeasurement(measurements);

            foreach (var measurement in measurements)
            {
                var picket = await context.Picket.FirstOrDefaultAsync(p => p.ID == measurement.ПикетыID);
                pickets.Add(picket);
            }

            cbPickets.ItemsSource = pickets;
            cbPickets.DisplayMemberPath = "ShowInfo";

        }

        private async Task PlotMeasurement(List<Measurement> measurements )
        {
            pltMeasurement.Plot.Clear();
            double[] coordinatesX = new double[measurements.Count];
            double[] coordinatesY = new double[measurements.Count];

            int i = -1;
            foreach(var measurement in measurements)
            {
                ++i;
                if(measurement.Сопротивление == 0.0)
                    continue;

                coordinatesX[i] = measurement.Вольтаж;
                coordinatesY[i] = measurement.Сопротивление;
            }
                

            var poly = pltMeasurement.Plot.Add.ScatterLine(coordinatesX, coordinatesY);

            poly.FillY = true;
            poly.ColorPositions.Add(new(ScottPlot.Colors.Red, 0));
            poly.ColorPositions.Add(new(ScottPlot.Colors.Orange, 10));
            poly.ColorPositions.Add(new(ScottPlot.Colors.Yellow, 20));
            poly.ColorPositions.Add(new(ScottPlot.Colors.Green, 30));
            poly.ColorPositions.Add(new(ScottPlot.Colors.Blue, 40));
            poly.ColorPositions.Add(new(ScottPlot.Colors.Violet, 50));

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

        private async void btSave_Click(object sender, RoutedEventArgs e)
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
            double res;
            if(!double.TryParse(tbResistance.Text, out res))
            {
                MessageBox.Show("Некорректное значение, скорее всего вы поделили на ноль и поле с сопротивление пустое.");
                return;
            }
          
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

            var measurements = context.Measurement.Where(m => m.ГеологическиеДанныеID == GeologicalData.ID).ToList();
            await PlotMeasurement(measurements);
        }

        private void tbVoltage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(tbVoltage.Text, out double voltage) &&
               double.TryParse(tbAmperaget.Text, out double amperage) &&
               amperage != 0)
            {
                double resistance = voltage / amperage;
                tbResistance.Text = resistance.ToString("F2");
            }
            else
            {
                tbResistance.Text = "";
            }
        }

        private void tbAmperaget_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(tbAmperaget.Text, out double amperage) &&
                   double.TryParse(tbVoltage.Text, out double voltage) &&
                   amperage != 0)
            {
                double resistance = voltage / amperage;
                tbResistance.Text = resistance.ToString("F2");
            }
            else
            {
                tbResistance.Text = "";
            }
        }
    }
}
