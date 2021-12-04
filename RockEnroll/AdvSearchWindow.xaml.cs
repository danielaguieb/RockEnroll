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

namespace RockEnroll
{
    /// <summary>
    /// Interaction logic for AdvSearchWindow.xaml
    /// </summary>
    public partial class AdvSearchWindow : Window
    {
        SearchResultPanel searchresultpanel = new();
        Search searchInstance = new("");
        public AdvSearchWindow()
        {
            InitializeComponent();
            
            //Load out filter form
            foreach (string name in Enum.GetNames(typeof(Campus)))
            {
                if (name == "NONE") continue;
                campus.Items.Add(name);
            }
            foreach (string name in Enum.GetNames(typeof(Faculty)))
            {
                if (name == "NONE") continue;
                faculty.Items.Add(name);
            }
            foreach (string name in Enum.GetNames(typeof(RockEnrollHelper.Terms)))
            {
                if (name == "NONE") continue;
                session.Items.Add(name);
            }

            foreach (int subjectint in Enum.GetValues(typeof(Department)))
            {
                if (subjectint == 0) continue;
                subjectbox.Items.Add(Enum.GetName((Department)subjectint));
            }

            //Prepare search panel;
            putSearchHere.Children.Add(searchresultpanel);
            searchInstance.Proccess();
            searchresultpanel.updateResults(searchInstance);
        }
            

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchInstance.searchstring = searchbar.Text;
        }

        private void Apply_Button_Click(object sender, RoutedEventArgs e)
        {
            searchInstance.Proccess();
            searchresultpanel.updateResults(searchInstance);
        }
    }


}
