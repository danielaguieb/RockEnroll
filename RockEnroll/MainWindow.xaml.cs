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
            RockEnrollHelper.InitializeCourses();
            RockEnrollHelper.AddCourse(RockEnrollHelper.allCourses[0]);
            RockEnrollHelper.AddCourse(RockEnrollHelper.allCourses[1]);
            RockEnrollHelper.AddCourse(RockEnrollHelper.allCourses[2]);
            InitializeComponent();
            Switcher.pageSwitcher = this;
            mainPanel.Children.Add(RockEnrollHelper._reqPage);
            enrollButton.Content = "Enrollment Checkout";
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
            //RockEnrollHelper._advsearch.Owner = this;
            RockEnrollHelper._reqPage.InitializeReqList();
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

        public void changeToConfirmAction()
        {
            enrollButton.Content = "Confirm Actions";
        }

        private void enrollButton_Click(object sender, RoutedEventArgs e)
        {
            if(enrollButton.Content.Equals("Enrollment Checkout"))
            {
                mainPanel.Children.Clear();
                _enrollmentPage = new EnrollmentView(this);
                mainPanel.Children.Add(_enrollmentPage);

                if ( _enrollmentPage.isAllToEnroll() )
                {
                    //enrollButton.Content = "Confirm Actions"; //TODO - force confirm actions for now
                    enrollButton.Content = "Enroll All";
                }
                else if (_enrollmentPage.isActionNeeded() )
                {
                    enrollButton.Content = "Confirm Actions";
                }
                else
                {
                    //enrollButton.Content = "Confirm Actions"; //TODO - force confirm actions for now
                    enrollButton.Content = "Finish"; //Nothing to do
                }
            }
            else if (enrollButton.Content.Equals("Enroll All"))
            {
                _enrollmentPage.enrollAllCourses();
                enrollButton.Content = "Confirm Actions";
            }
            else if (enrollButton.Content.Equals("Confirm Actions"))
            {
                bool result = _enrollmentPage.confirmActions();
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
