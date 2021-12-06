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

        //public RockEnrollHelper Helper { get; set; } = new();

        EnrollmentView _enrollmentPage;

        public MainWindow()
        {
            InitializeComponent();
            RockEnrollHelper.InitializeCourses();
            RockEnrollHelper.AddCourse(RockEnrollHelper.allCourses[0]);
            RockEnrollHelper.AddCourse(RockEnrollHelper.allCourses[1]);
            Switcher.pageSwitcher = this;
        }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        private void CourseTabClick(object sender, RoutedEventArgs e)
        {
            mainPanel.Children.Clear();
            mainPanel.Children.Add(RockEnrollHelper._coursePage);
            enrollButton.Content = "Enrollment Checkout";
        }

        private void RequirementsTabClick(object sender, RoutedEventArgs e)
        {
            mainPanel.Children.Clear();
            mainPanel.Children.Add(RockEnrollHelper._reqPage);
            enrollButton.Content = "Enrollment Checkout";
        }

        private void ScheduleTabClick(object sender, RoutedEventArgs e)
        {
            mainPanel.Children.Clear();
            mainPanel.Children.Add(RockEnrollHelper._schedulePage);
            enrollButton.Content = "Enrollment Checkout";
        }

        private void AdvancedSearchClick(object sender, RoutedEventArgs e)
        {
            if (RockEnrollHelper._advsearch == null) RockEnrollHelper._advsearch = new();
            RockEnrollHelper._advsearch.Owner = this;
            RockEnrollHelper._advsearch.Show();
        }

        private void scheduleTabButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void enrollButton_Click(object sender, RoutedEventArgs e)
        {
            if(enrollButton.Content.Equals("Enrollment Checkout"))
            {
                mainPanel.Children.Clear();
                _enrollmentPage = new EnrollmentView();
                mainPanel.Children.Add(_enrollmentPage);

                if ( _enrollmentPage.isAllToEnroll() )
                {
                    enrollButton.Content = "Enroll All";
                } 
                else if (_enrollmentPage.isActionNeeded() )
                {
                    enrollButton.Content = "Confirm Actions";
                }
                else
                {
                    enrollButton.Content = "Finish"; //Nothing to do
                }
            }
            else if (enrollButton.Content.Equals("Enroll All"))
            {
                _enrollmentPage.checkAllCourses();
                enrollButton.Content = "Confirm Actions";
            }
            else if (enrollButton.Content.Equals("Confirm Actions"))
            {
                bool result = _enrollmentPage.confirmCourses();
                if (result)
                {
                    enrollButton.Content = "Finish";
                }
            }
            else if (enrollButton.Content.Equals("Finish"))
            {
                // Go back to Course List
                mainPanel.Children.Clear();
                RockEnrollHelper.updateCoursePage();
                mainPanel.Children.Add(RockEnrollHelper._coursePage);
                enrollButton.Content = "Enrollment Checkout";
            }
            //TODO  --else do something
        }

    }
}
