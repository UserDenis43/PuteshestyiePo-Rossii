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

namespace PuteshestyiePo_Rossii.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddHotelWindow.xaml
    /// </summary>
    public partial class AddHotelWindow : Window
    {
        private TestEntities _context;
        private HotelWindow _window;
        public AddHotelWindow(TestEntities testEntities, HotelWindow hotelWindow)
        {
            InitializeComponent();
            this._context = testEntities;
            this._window = hotelWindow;
            CmbNameCountry.ItemsSource = _context.Country.ToList();
        }

        private void BtnAddHotel_Click(object sender, RoutedEventArgs e)
        {
            _context.Hotel.Add(new Hotel()
            {
                    Name = TxtNameHotel.Text,
                    CountOfStars = Convert.ToInt32(TxtCountStars.Text),
                    Description = TxtDescriptionHotel.Text,
                    Country = CmbNameCountry.SelectedItem as Country
            });
            _context.SaveChanges();
            _window.RefreshHotels();
            this.Close();
        }
    }
}
