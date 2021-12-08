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

namespace RockEnroll
{
    /// <summary>
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : TextBox
    {
        
        public string placeHolderText { get; set; }
        public static SearchBarResultsContainer searchBarResultsContainer { get; set; }

        public SearchBar()
        {
            InitializeComponent();

            this.TextChanged += SearchBarTextChanged;
            this.GotFocus += SearchBarGotFocus;
            this.LostFocus += SearchBarLostFocus;
            searchBarResultsContainer = new SearchBarResultsContainer(this);
            searchBarResultsContainer.Width = 500;
            searchBarResultsContainer.Height = 500;

        }

        public void SearchBarTextChanged(object sender, RoutedEventArgs args)
        {

            searchBarResultsContainer.UpdateContents(Text);
            this.CaretBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

        }

        public void SearchBarLostFocus(object sender, RoutedEventArgs args)
        {

            if (searchBarResultsContainer.IsVisible) searchBarResultsContainer.Visibility = Visibility.Hidden;
            if (Text == "") Text = placeHolderText;

        }

        public void SearchBarGotFocus(object sender, RoutedEventArgs args)
        {

            if (!searchBarResultsContainer.IsVisible)
            {
                searchBarResultsContainer.Visibility = Visibility.Visible;
            }

            if (Text == placeHolderText) Text = "";

        }
    }
}
