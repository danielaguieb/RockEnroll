using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace RockEnroll
{
    /// <summary>
    /// Interaction logic for EnrollmentView.xaml
    /// </summary>
    public partial class EnrollmentView : UserControl
    {
        private bool actionNeeded = false;
        private int toEnrollCtr = 0;

        public EnrollmentView()
        {
            InitializeComponent();
            for (int i = 0; i < RockEnrollHelper.student.currentSchedule.Count(); i++)
            {
                bool foundCourseToAction = AddClass(RockEnrollHelper.student.currentSchedule[i]);
                if (foundCourseToAction)
                {
                    this.actionNeeded = true;
                }
            }

        }

        public bool isActionNeeded()
        {
            return this.actionNeeded;
        }

        public bool AddClass(ClassInstance c)
        {
            bool foundCourseToAction = false;
            CourseView view = new CourseView(ref c);
            Button actionButton = view.actionButton;
            if ( c.enrolled )
            {
                changeButtonImage(actionButton, "Resources\\enroll.png");
                view.actionText.Content = "Enrolled";
                view.CourseCheckBox.IsChecked = true;
            }
            else if ( c.swapped || c.dropped || c.waitListed )
            {
                foundCourseToAction = true;
                if (c.swapped)
                {
                    changeButtonImage(actionButton, "Resources\\swap.png");
                } else if ( c.dropped )
                {
                    changeButtonImage(actionButton, "Resources\\drop.png");
                } else if ( c.waitListed )
                {
                    changeButtonImage(actionButton, "Resources\\inCart.png");
                }
            } 
            else
            {
                changeButtonImage(actionButton, "Resources\\inCart.png");
                this.toEnrollCtr++;
            }
            view.setActionMode(CourseView.ACTION_ENROLL); //TODO only if there is something to action

            this.courseListViewer.Children.Add(view);
            return foundCourseToAction;
        }

        public bool isAllToEnroll()
        {
            if ( toEnrollCtr == RockEnrollHelper.student.currentSchedule.Count() )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void checkAllCourses()
        {
            this.courseListViewer.ToolTip = "checkAllCourses";
            for ( int i = 0; i<this.courseListViewer.Children.Count; i++)
            {
                CourseView view = (CourseView)this.courseListViewer.Children[i];
                Button actionButton = view.actionButton;
                changeButtonImage(actionButton, "Resources\\enroll.png"); //check mark
                view.actionText.Content = "Enrolling";
            }

        }

        public bool confirmCourses()
        {
            this.courseListViewer.ToolTip = "confirmCourses";
 
            String messageTitle = "Caution: Action item needs your attention.";
            String messageText = "Do you wish to enroll in the following courses?\r\n";

            //Concatenate all the courses into listOfCourses
            String listOfCourses = getListOfCourses();
            messageText += listOfCourses;
            MessageBoxResult d;
            d = MessageBox.Show(messageText, messageTitle, MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (d == MessageBoxResult.OK)
            {
                enrollCourses(listOfCourses);
                return true;
            } else
            {
                return false;
            }
            
        }

        private String getListOfCourses()
        {
            String listOfCourses = "";
            /*for (int i = 0; i < this.courseListViewer.Children.Count; i++)
            {
                CourseView view = (CourseView)this.courseListViewer.Children[i];
                listOfCourses += view.courseNameText.Content + "\r\n";
            }*/
            for (int i = 0; i < RockEnrollHelper.student.currentSchedule.Count(); i++)
            {
                ClassInstance sched = (ClassInstance)RockEnrollHelper.student.currentSchedule[i];
                listOfCourses += sched.department + " " + sched.courseID + "\r\n";
            }
            return listOfCourses;
        }
        
        private void enrollCourses(String listOfCourses)
        {
            // Set text of all classes to Enrolled
            this.courseListViewer.ToolTip = "Enrolled Courses";
            for (int i = 0; i < this.courseListViewer.Children.Count; i++)
            {
                CourseView view = (CourseView)this.courseListViewer.Children[i];
                Button actionButton = view.actionButton;
                view.actionText.Content = "Enrolled";

                ClassInstance sched = (ClassInstance)RockEnrollHelper.student.currentSchedule[i];
                sched.enrolled = true;
            }

            // Show Notification on the page
            this.NotificationText.Text = "Successfully enrolled in courses:\r\n" + listOfCourses;
            this.NotificationBox.IsExpanded = true;
        }

        public void changeButtonImage(Button button, String resoureName)
        {
            Uri resourceUri = new Uri(resoureName, UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
            BitmapFrame bitmap = BitmapFrame.Create(streamInfo.Stream);
            var brush = new ImageBrush();
            brush.ImageSource = bitmap;
            button.Background = brush;
        }
    }
}
