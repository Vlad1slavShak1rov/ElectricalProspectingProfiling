using ElectricalProspectingProfiling.Model;
using ElectricalProspectingProfiling.Tools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ElectricalProspectingProfiling.Database.context;
using ElectricalProspectingProfiling.Database.DAL;
using Microsoft.IdentityModel.Tokens;

namespace ElectricalProspectingProfiling.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddSquareWindow.xaml
    /// </summary>
    public partial class AddSquareWindow : Window
    {
        private List<CoordinatsSquare> coordinatsSquares = new();
        private List<CoordinatsProfile> coordinatsProfiles = new();
        private List<string> coordinatsPicket = new();

        public AddSquareWindow()
        {
            InitializeComponent();
        }

        // Универсальный метод для добавления координат
        private void AddCoordinate<T>(TextBox xCoordinateTextBox, TextBox yCoordinateTextBox, List<T> coordinateList, ListBox coordinateListBox, Func<int, int, T> createCoordinate)
        {
            string x = xCoordinateTextBox.Text;
            string y = yCoordinateTextBox.Text;

            if (Validator.IsNullOrEmptyOrWhiteSpace(x, y))
            {
                MessageShow.Information("У вас есть незаполненные координаты!");
                return;
            }

            int xCoordinate;            
            int yCoordinate;

            if(!int.TryParse(x,out xCoordinate))
            {
                MessageShow.Information("У вас есть незаполненные координаты!");
                return;
            }

            if (!int.TryParse(x, out yCoordinate))
            {
                MessageShow.Information("У вас есть незаполненные координаты!");
                return;
            }

            T coordinate = createCoordinate(xCoordinate, yCoordinate);

            coordinateList.Add(coordinate);

            string coordinateText = $"X: {x} Y: {y}";

            if (!coordinateListBox.Items.Contains(coordinateText))
            {
                coordinateListBox.Items.Add(coordinateText);
                xCoordinateTextBox.Text = "";
                yCoordinateTextBox.Text = "";
            }
            else
            {
                MessageShow.Information("Данные координаты уже добавлены!");
            }
        }
        private void addSquareCoordinateButton_Click(object sender, RoutedEventArgs e)
        {
            AddCoordinate(
                xCoordinateSquare,
                yCoordinateSquare,
                coordinatsSquares,
                squareCoordinateListBox,
                (x, y) => new CoordinatsSquare { XКоордината = x, YКоордината = y }
            );
        }

        private void removeSquareCoordinateButton_Click(object sender, RoutedEventArgs e)
        {
            if (squareCoordinateListBox.Items.Count > 0)
            {
                var item = squareCoordinateListBox.Items[squareCoordinateListBox.Items.Count - 1];
                squareCoordinateListBox.Items.Remove(item);
            }
        }

        private void addProfileCoordinateButton_Click(object sender, RoutedEventArgs e)
        {
            AddCoordinate(
                xCoordinateProfile,
                yCoordinateProfile,
                coordinatsProfiles,
                ProfileCoordinateListBox,
                (x, y) => new CoordinatsProfile { XКоордината = x, YКоордината = y }
            );
        }

        private void removeProfileCoordinateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProfileCoordinateListBox.Items.Count > 0)
            {
                var item = ProfileCoordinateListBox.Items[ProfileCoordinateListBox.Items.Count - 1];
                ProfileCoordinateListBox.Items.Remove(item);
            }
        }

        private void addpicketCoordinateButton_Click(object sender, RoutedEventArgs e)
        {
            AddCoordinate(
                xCoordinatepicket,
                yCoordinatepicket,
                coordinatsPicket,
                picketCoordinateListBox,
                (x, y) => $"X: {x}, Y: {y}"
            );
        }

        private void removepicketCoordinateButton_Click(object sender, RoutedEventArgs e)
        {
            if (picketCoordinateListBox.Items.Count > 0)
            {
                var item = picketCoordinateListBox.Items[picketCoordinateListBox.Items.Count - 1];
                picketCoordinateListBox.Items.Remove(item);
            }
        }

      

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if(squareCoordinateListBox.Items.Count < 4 || ProfileCoordinateListBox.Items.Count < 4 || picketCoordinateListBox.Items.Count < 4)
            {
                MessageShow.Information("Добавьте хотя бы 4 координат в каждый пункт.");
                return;
            }
            
         
            string name = nameSquareBox.Text;
            string height = heightBox.Text;
            var methodProfile = (profileComboBox.SelectedItem as ComboBoxItem)!.Content;

            if (Validator.IsNullOrEmptyOrWhiteSpace(name, height))
            {
                MessageShow.Information("У вас есть незаполненные поля!");
                return;
            }

            using var context = new MyDBContext();
            RepositoryCoordinatsProfile repositoryCoordinatsProfile = new(context);
            RepositoryCoordinatsSquare repositoryCoordinatsSquare = new(context);
            RepositoryProfile repositoryProfile = new(context);
            RepositorySquare repositorySquare = new(context);
            RepositoryPicket repositoryPicket = new(context);

            var square = new Square() { Высота = decimal.Parse(height), Название = name};
            await repositorySquare.Add(square);

            var existSquare = await repositorySquare.GetById(square.ID);
            foreach(var squareCoordinat in coordinatsSquares)
            {
                squareCoordinat.SquareID = existSquare.ID;
                await repositoryCoordinatsSquare.Add(squareCoordinat);
            }

            foreach (var profiliCoord in coordinatsProfiles)
            {
                int count = 0;
                await repositoryCoordinatsProfile.Add(profiliCoord);
                var profile = new Profile() { Square = square, МетодПрофилирования = methodProfile.ToString(),КоординатыID = profiliCoord.ID };
                await repositoryProfile.Add(profile);
                foreach (var picketCoord in coordinatsPicket)
                {
                    await repositoryPicket.Add(new Picket { Дистанция = (new Random()).Next(1, 150), Координаты = picketCoord, ПрофильID = profile.ID, Номер = ++count });
                }
            }
            MessageShow.Information("Успешно сохранено!");

            this.DialogResult = true;
            this.Close();
        }
    }
}
