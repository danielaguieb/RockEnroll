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
        private Window parent;

        public const int STATE_ENROLL   = 1;
        public const int STATE_DROP     = 2;
        public const int STATE_SWAP     = 3;
        public const int STATE_WAITLIST = 4;

        public EnrollmentView(Window parent)
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
            this.parent = parent;
        }

        public bool isActionNeeded()
        {
            return this.actionNeeded;
        }

        public void changeToConfirmAction()
        {
            if (this.parent != null && this.parent is MainWindow)
            {
                MainWindow view = (MainWindow)this.parent;
                view.changeToConfirmAction();
            }
        }

        public bool AddClass(ClassInstance c)
        {
            bool foundCourseToAction = false;
            CourseView view = new CourseView(ref c, this);
            Button actionButton = view.actionButton;
            this.toEnrollCtr += view.setStatus(c, ref foundCourseToAction);

            /*
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
                    changeButtonImage(actionButton, "Resources\\enroll.png");
                    view.actionText.Content = "Enrolled";
                } else if ( c.dropped )
                {
                    changeButtonImage(actionButton, "Resources\\drop.png");
                    view.actionText.Content = "Dropped";
                } else if ( c.waitListed )
                {
                    changeButtonImage(actionButton, "Resources\\inCart.png");
                    view.actionText.Content = "Waitlisted";
                }
            } 
            else
            {
                changeButtonImage(actionButton, "Resources\\inCart.png");
                this.toEnrollCtr++;
            }*/

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

        public void enrollAllCourses()
        {
            this.courseListViewer.ToolTip = "Enrolled All Courses";
            for ( int i = 0; i<this.courseListViewer.Children.Count; i++)
            {
                CourseView view = (CourseView)this.courseListViewer.Children[i];
                Button actionButton = view.actionButton;
                CourseView.changeButtonImage(actionButton, "Resources\\enroll.png"); //check mark
                view.actionText.Content = "Enrolling";
            }

        }

        public bool confirmActions()
        {
            this.courseListViewer.ToolTip = "Confirmed Actions";

            String messageTitle = "Caution: Action item needs your attention.";
            String messageText = "Do you wish to enroll in the following courses?\r\n";

            //Check what action to take
            //1. Drop course
            String listOfCourses = getListOfCourses(STATE_DROP);
            if (!listOfCourses.Equals("")) { // there are courses to drop
                messageTitle = "Caution: Action item needs your attention.";
                messageText = "Do you wish to drop the following courses?\r\n";
                messageText += listOfCourses;
                MessageBoxResult d;
                d = MessageBox.Show(messageText, messageTitle, MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (d == MessageBoxResult.OK)
                {
                    dropCourses(listOfCourses);
                    return true;
                }
            }

            //2. Swap course
            listOfCourses = getListOfCourses(STATE_SWAP);
            if (!listOfCourses.Equals(""))
            { // there are courses to drop
                messageTitle = "Caution: Action item needs your attention.";
                messageText = "Do you wish to swap the following courses?\r\n";
                messageText += listOfCourses;
                MessageBoxResult d;
                d = MessageBox.Show(messageText, messageTitle, MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (d == MessageBoxResult.OK)
                {
                    swapCourses(listOfCourses);
                    return true;
                }
            }

            //3. Enroll courses
            listOfCourses = getListOfCourses(STATE_ENROLL);
            if (!listOfCourses.Equals("")) // there are course to enroll
            { // there are courses to drop
                messageTitle = "Caution: Action item needs your attention.";
                messageText = "Do you wish to enroll in the following courses?\r\n";

                messageText += listOfCourses;
                MessageBoxResult d;
                d = MessageBox.Show(messageText, messageTitle, MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (d == MessageBoxResult.OK)
                {
                    enrollCourses();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        private String getListOfCourses(int stateCode)
        {
            String listOfCourses = "";
            /*for (int i = 0; i < this.courseListViewer.Children.Count; i++)
            {
                CourseView view = (CourseView)this.courseListViewer.Children[i];
                listOfCourses += view.courseNameText.Content + "\r\n";
            }*/
            //for (int i = 0; i < RockEnrollHelper.student.currentSchedule.Count(); i++)
            //  ClassInstance c = (ClassInstance)RockEnrollHelper.student.currentSchedule[i];
            for (int i = 0; i < this.courseListViewer.Children.Count; i++)
            {
                CourseView view = (CourseView)this.courseListViewer.Children[i];
                String courseCode = view.courseNameText.Content.ToString();
                courseCode = courseCode.Replace("\n", " ");

                if ( stateCode == STATE_ENROLL && view.actionText.Content.ToString().Equals("Enrolling"))
                {
                    listOfCourses += "- " + courseCode + "\r\n";
                }
                else if ( stateCode == STATE_DROP && view.actionText.Content.ToString().Equals("Dropping") )
                {
                    listOfCourses += "- " + courseCode + "\r\n";
                }
                else if (stateCode == STATE_SWAP && view.actionText.Content.ToString().Equals("Swapping"))
                {
                    listOfCourses += "- " + courseCode + "\r\n";
                }
                else if (stateCode == STATE_WAITLIST && view.actionText.Content.ToString().Equals("Wait Listing"))
                {
                    listOfCourses += "- " + courseCode + "\r\n";
                }

            }
            return listOfCourses;
        }
        
        private void enrollCourses()
        {
            // Set text of all classes to Enrolled
            this.courseListViewer.ToolTip = "Enrolled Courses";
            String enrolledCourses = "";
            String waitlistedCourses = "";

            for (int i = 0; i < this.courseListViewer.Children.Count; i++)
            {
                CourseView view = (CourseView)this.courseListViewer.Children[i];
                Button actionButton = view.actionButton;
                String courseCode = view.courseNameText.Content.ToString();
                courseCode = courseCode.Replace("\n", " ");

                ClassInstance c = (ClassInstance)RockEnrollHelper.student.currentSchedule[i];
                if (RockEnrollHelper.checkClassCapacity(c.courseID) )
                {
                    c.enrolled = true;
                    c.dropped = false;
                    c.swapped = false;
                    c.waitListed = false;
                    view.actionText.Content = "Enrolled";
                    enrolledCourses += "- " + courseCode + "\r\n";
                }
                else
                {
                    c.waitListed = true;
                    c.enrolled = false;
                    c.dropped = false;
                    c.swapped = false;
                    view.actionText.Content = "WaitListed:1";
                    CourseView.changeButtonImage(actionButton, "Resources\\inCart.png");
                    waitlistedCourses += "- " + courseCode + "\r\n";
                }

            }

            // Show Notification on the page
            if (!enrolledCourses.Equals(""))
            {
                String message = "Successfully enrolled in course(s):\r\n" + enrolledCourses;
                this.NotificationBox.IsExpanded = true;
                addNotification(message, Brushes.DarkSeaGreen);
            }
            if (!waitlistedCourses.Equals(""))
            {
                String message = "You are waitlisted for:\r\n" + waitlistedCourses;
                this.NotificationBox.IsExpanded = true;
                addNotification(message, Brushes.LightSteelBlue);
            }
        }

        private void addNotification(String message, SolidColorBrush color)
        {
            TextBlock text = new TextBlock();
            text.Background = color;
            text.Text = message;

            this.StackPanelSection.Children.Add(text);
        }

        private void dropCourses(String listOfCourses)
        {
            // Set text of all classes to Enrolled
            this.courseListViewer.ToolTip = "Dropped Courses";
            for (int i = 0; i < this.courseListViewer.Children.Count; i++)
            {
                CourseView view = (CourseView)this.courseListViewer.Children[i];
                if ( view.actionText.Content.ToString().Equals("Dropping") )
                {
                    Button actionButton = view.actionButton;
                    view.actionText.Content = "Dropped";

                    ClassInstance c = (ClassInstance)RockEnrollHelper.student.currentSchedule[i];
                    c.dropped = true;
                    c.enrolled = false;
                    c.swapped = false;
                    c.waitListed = false;
                }
                
            }

            // Show Notification on the page
            String message = "Successfully dropped the course(s):\r\n" + listOfCourses;
            this.NotificationBox.IsExpanded = true;
            addNotification(message, Brushes.LightCoral);
        }

        private void swapCourses(String listOfCourses)
        {
            // Set text of all classes to Enrolled
            this.courseListViewer.ToolTip = "Swapped Courses";
            for (int i = 0; i < this.courseListViewer.Children.Count; i++)
            {
                CourseView view = (CourseView)this.courseListViewer.Children[i];
                if (view.actionText.Content.ToString().Equals("Swapping"))
                {
                    Button actionButton = view.actionButton;
                    view.actionText.Content = "Swapped";

                    ClassInstance c = (ClassInstance)RockEnrollHelper.student.currentSchedule[i];
                    c.swapped = true;
                    c.enrolled = false;
                    c.dropped = false;
                    c.waitListed = false;
                }

            }

            // Show Notification on the page
            String message = "You swapped time(s) for:\r\n" + listOfCourses;
            this.NotificationBox.IsExpanded = true;
            addNotification(message, Brushes.LemonChiffon);
        }

        /*public void changeButtonImage(Button button, String resoureName)
        {
            Uri resourceUri = new Uri(resoureName, UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
            BitmapFrame bitmap = BitmapFrame.Create(streamInfo.Stream);
            var brush = new ImageBrush();
            brush.ImageSource = bitmap;
            button.Background = brush;
        }*/
    }
}
