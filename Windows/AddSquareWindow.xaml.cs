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
    /// Логика взаимодействия для AddSquareWindow.xaml
    /// </summary>
    public partial class AddSquareWindow : Window
    {
        private List<CoordinatsSquare> coordinatsSquares = new();
        private List<CoordinatsProfile> coordinatsProfiles = new();
        public AddSquareWindow()
        {
            InitializeComponent();
        }

        // Универсальный метод для добавления координат
        private void AddCoordinate<T>(TextBox xCoordinateTextBox, TextBox yCoordinateTextBox, List<T> coordinateList, ListBox coordinateListBox, Func<int, int, T> createCoordinate)
        {
            string x = xCoordinateTextBox.Text;
            string y = yCoordinateTextBox.Text;

            // Проверка на пустые значения
            if (Validator.IsNullOrEmptyOrWhiteSpace(x, y))
            {
                MessageShow.Information("У вас есть незаполненные координаты!");
                return;
            }

            // Преобразуем строки в целые числа
            int xCoordinate = int.Parse(x);
            int yCoordinate = int.Parse(y);

            // Создаем координату
            T coordinate = createCoordinate(xCoordinate, yCoordinate);

            // Добавляем в список
            coordinateList.Add(coordinate);

            string coordinateText = $"X: {x} Y: {y}";

            // Проверка на уникальность координаты в списке
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

        // Обработчики событий
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

        }

        private void removepicketCoordinateButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
