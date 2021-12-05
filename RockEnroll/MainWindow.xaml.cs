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
        EnrollmentView _enrollmentPage = new EnrollmentView();
        AdvSearchWindow _advsearch;

        //public RockEnrollHelper Helper { get; set; } = new();




        public MainWindow()
        {
            InitializeComponent();
            Switcher.pageSwitcher = this;
            //RockEnrollHelper.InitializeCourses();
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
            if (_advsearch == null) _advsearch = new();
            _advsearch.Owner = this;
            _advsearch.Show();
        }

        private void scheduleTabButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void enrollButton_Click(object sender, RoutedEventArgs e)
        {
            mainPanel.Children.Clear();
            mainPanel.Children.Add(_enrollmentPage);
            if (enrollButton.Content.Equals("Enrollment Checkout"))
            {
                enrollButton.Content = "Enroll All";
            } else if (enrollButton.Content.Equals("Enroll All"))
            {
                _enrollmentPage.checkAllCourses();
                enrollButton.Content = "Confirm Actions";
            } else if (enrollButton.Content.Equals("Confirm Actions"))
            {
                bool result = _enrollmentPage.confirmCourses();
                if (result)
                {
                    enrollButton.Content = "Finish";
                }
            }
            //TODO
        } 

    }
}
