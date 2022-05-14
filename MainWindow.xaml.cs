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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PuteshestyiePo_Rossii
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TestEntities _context = new TestEntities();
        private List<Tour> _tours = new List<Tour>();
        private string _SelectedType;
        private string _FindedName;
        public MainWindow()
        {
            InitializeComponent();
            ListTours.ItemsSource = _context.Tour.OrderBy(tour => tour.Name).ToList();

            List<Type> types = new List<Type>();
            types.Add(new Type() { Name = "Все типы" });
            types.AddRange(_context.Type.OrderBy(type => type.Name).ToList());
            CmdType.ItemsSource = types;

            this._tours = _context.Tour.ToList();
        }

        private void TxtFindedTourName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _tours = _context.Tour.OrderBy(tour => tour.Name).ToList();
            RefreshTours();
        }

        private void RefreshTours()
        {
            _FindedName = TxtFindedTourName.Text;
            if (_SelectedType != null)
            {
                if ((CmdType.SelectedItem as Type).Name != "Все типы")
                    {
                        Type type = CmdType.SelectedItem as Type;
                        _SelectedType = type.Name;
                        _tours = (from tour in _tours from tt in tour.Type where tt.Name == _SelectedType select tour).ToList();
                    }
                else if ((CmdType.SelectedItem as Type).Name == "Все типы")
                    {
                         _tours = _context.Tour.OrderBy(tour => tour.Name).ToList();
                    }
            }
            if (_FindedName != "")
            {
                _tours = _tours.OrderBy(tour => tour.Name).Where(tour => tour.Name.ToLower().Contains(_FindedName)).ToList();
            }
           
            if ((bool)ChdActual.IsChecked)
            {
                _tours = _tours.OrderBy(tour => tour.Name).Where(tour => tour.IsActual == true).ToList();
            }
            ListTours.ItemsSource = _tours;
        }

        private void CmdType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _tours = _context.Tour.OrderBy(tour => tour.Name).ToList();
            RefreshTours();
        }

        private void ChdActual_Checked(object sender, RoutedEventArgs e)
        {
            _tours = _context.Tour.OrderBy(tour => tour.Name).ToList();
            RefreshTours();
        }

        private void ChdActual_Unchecked(object sender, RoutedEventArgs e)
        {
            _tours = _context.Tour.OrderBy(tour => tour.Name).ToList();
            RefreshTours();
        }

        private void BtnShowHotelWindow_Click(object sender, RoutedEventArgs e)
        {
            Windows.HotelWindow window = new Windows.HotelWindow();
            window.ShowDialog();
        }
    }
}
