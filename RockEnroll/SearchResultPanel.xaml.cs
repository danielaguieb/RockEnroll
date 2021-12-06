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
    /// Interaction logic for SearchResultPanel.xaml
    /// </summary>
    public partial class SearchResultPanel : UserControl
    {
        public SearchResultPanel()
        {
            InitializeComponent();
        }

        public void updateResults(Search search)
        {
            resultList.Items.Clear();
            foreach (KeyValuePair<Department,List<Course>> i in search.results)
            {
                Expander x = new();
                x.Header = Enum.GetName(i.Key);
                resultList.Items.Add(x);
                StackPanel y = new();
                x.Content = y;
                foreach (Course j in i.Value)
                {
                    TextBlock z = new();
                    z.Text = j.department + j.courseID + ": " + j.courseTitle;
                    y.Children.Add(z);
                }
            }
            //
            //foreach (Course i in search.results)
            //{
            //    resultList.Items.Add(i.courseTitle);
            //}
        }
    }
}
