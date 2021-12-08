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
        CourseView n;
        ClassInstance c;
        public SearchResultPanel()
        {
            InitializeComponent();
        }

        public void updateResults(Search search)
        {
            resultList.Children.Clear();
            foreach (KeyValuePair<Department,List<Course>> i in search.results)
            {
                Expander x = new();
                x.Header = Enum.GetName(i.Key);
                resultList.Children.Add(x);
                StackPanel y = new();
                x.Content = y;
                foreach (Course j in i.Value)
                {
                    c = new ClassInstance(j, Terms.FALL2021, 0);
                    n = new CourseView(ref c, true);
                    n.Width = 850;
                    y.Children.Add(n);
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
