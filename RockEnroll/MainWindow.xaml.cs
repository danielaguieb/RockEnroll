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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CourseList _coursePage = new CourseList();
        AcademicRequirements _reqPage = new AcademicRequirements();
        Schedules _schedulePage = new Schedules();
        
        public MainWindow()
        {
            InitializeComponent();
            Switcher.pageSwitcher = this;
        }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        private void CourseTabClick(object sender, RoutedEventArgs e)
        {
            mainPanel.Children.Clear();
            mainPanel.Children.Add(_coursePage);
        }

        private void RequirementsTabClick(object sender, RoutedEventArgs e)
        {
            mainPanel.Children.Clear();
            mainPanel.Children.Add(_reqPage);
        }

        private void ScheduleTabClick(object sender, RoutedEventArgs e)
        {
            mainPanel.Children.Clear();
            mainPanel.Children.Add(_schedulePage);
        }

        private void AdvancedSearchClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
