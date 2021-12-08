using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
using Color = System.Windows.Media.Color;

namespace RockEnroll
{
    /// <summary>
    /// Interaction logic for CourseView.xaml
    /// </summary>
    public partial class CourseView : UserControl
    {
        public const int ACTION_DELETE = 1;
        public const int ACTION_ENROLL = 2;
        public const int ACTION_ADD = 3;

        private int actionMode = ACTION_DELETE;
        private String saveActionText;

        private List<Course> missingPrereq = new List<Course>();

        private UserControl parent;

        private bool view;
            
            public CourseView(ref ClassInstance c, bool view = false)
        {
            InitializeComponent();
            this.classInstance = c;
            this.view = view;
            this.courseNameText.Content = c.department.ToString() + "\n" + c.courseID.ToString();
            this.courseTitleText.Text = c.courseTitle;
            this.campusText.Content = c.lecturesList[c.lectureNum].campus.ToString();
            bool foundCourseToAction = false;
            this.setStatus(classInstance, ref foundCourseToAction);

            saveActionText = this.actionText.Content.ToString();
            if(c.prerequisites.Count() != 0)
            {
                string missingreq = "";
                for(int i = 0; i < c.prerequisites.Count(); i++)
                {
                    if (!RockEnrollHelper.student.coursesTaken.Contains(c.prerequisites[i]))
                    {
                        if (i == 0)
                        {
                            missingreq = "Missing Prerequisites: ";
                        }else if (i != c.prerequisites.Count() - 1)
                        {
                            missingreq += ", ";
                        }
                        missingPrereq.Add(c.prerequisites[i]);
                        missingreq += c.prerequisites[i].department.ToString() + " " + c.prerequisites[i].courseID.ToString();
                    }
                }

                

                this.mprereqText.Content = missingreq;
            }
            addRequisite(c.prerequisites, 0);
            addRequisite(c.antirequisites, 1);
            addRequisite(c.successors, 2);
            this.unitsText.Text = "Course Units: " + c.courseUnits.ToString() + " units";
            this.courseDesc.Text = "Description: " + c.courseDescription;
            BrushConverter bc = new BrushConverter();
            this.courseNameText.Background = (Brush)bc.ConvertFrom("#aebcd6ff");

            if (view)
            {
                this.setActionMode(ACTION_ADD);
                this.courseGrid.Children.Remove(CourseCheckBox);
                
                this.sectionsGrid.Children.Clear();
                this.titleGrid.Children.Remove(actionText);
                this.sectionsGrid.Children.Add(actionText);
                sectionsGrid.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetRow(actionText, 0);
                actionText.HorizontalAlignment = HorizontalAlignment.Center;

                if (RockEnrollHelper.student.coursesTaken.Contains(c.lecturesList[0].course))
                {
                    changeButtonImage(actionButton, "Resources\\checkmark.png");
                    this.actionButton.IsHitTestVisible = false;
                    this.actionText.Content = "Completed";
                    this.actionText.Background = Brushes.Green;
                    this.actionText.Foreground = Brushes.White;
                    this.courseNameText.Background = (Brush)bc.ConvertFrom("#FFF0FFF0");

                }
                else if (RockEnrollHelper.student.currentSchedule.Contains(c.lecturesList[0].course))
                {
                    this.setActionMode(ACTION_DELETE);
                    this.actionButton.IsHitTestVisible = false;

                    this.courseNameText.Background = (Brush)bc.ConvertFrom("#FFDCDCDC");
                }
                else if(this.missingPrereq.Count() != 0)
                {
                    changeButtonImage(actionButton, "Resources\\caution.png");
                    this.actionText.Content = "Prerequisites Missing";
                    this.actionText.Background = Brushes.DarkOrange;
                    this.actionText.Foreground = Brushes.White;
                    this.courseNameText.Background = (Brush)bc.ConvertFrom("#FFFFFACD");
                    this.actionButton.IsHitTestVisible = false;
                }
                else
                {
                    this.actionText.Content = "";
                    changeButtonImage(actionButton, "Resources\\greenplus.png");
                }
            }
            else
            {
                AddLectureSections(c.lecturesList);
                AddLectureDescription();
                AddTutorialSections(c.tutorialsList);
                AddTutorialDescription();
                AddLabSections(c.labsList);
                AddLabDescription();
            }

        }


        private void addRequisite(List<Course> req, int row)
        {
            if (req.Count() != 0)
            {
                String reqText = "";
                switch (row)
                {
                    case 0:
                        reqText += "Prerequisites: ";
                        break;
                    case 1:
                        reqText += "Antirequisites: ";
                        break;
                    case 2:
                        reqText += "Prerequisite for: ";
                        break;
                }
                for (int i = 0; i < req.Count(); i++)
                {
                    reqText += req[i].department.ToString() + " " + req[i].courseID.ToString();
                    if (i != req.Count() - 1)
                    {
                        reqText += ", ";
                    }
                }
                Label r = new Label();
                r.Content = reqText;
                addChild(courseInfoGrid, r, row);
            }
        }
        private void addChild(Grid grid, Control child, int row)
        {
            grid.Children.Add(child);
            Grid.SetRow(child, row);
        }

        public CourseView(ref ClassInstance classInstance, UserControl parent) : this(ref classInstance)
        {
            this.parent = parent;
        }

        private ClassInstance classInstance;
        public ClassInstance ClassInstance
        {
            get { return classInstance; }
            set {
                classInstance = value;
            }
        }

        public int setStatus(ClassInstance c, ref bool foundCourseToAction)
        {

            if (c.enrolled)
            {
                changeButtonImage(actionButton, "Resources\\enroll.png");
                this.actionText.Content = "Enrolled";
                this.CourseCheckBox.IsChecked = true;
                return 0;
            }
            else if (c.swapped || c.dropped || c.waitListed)
            {
                foundCourseToAction = true;
                if (c.swapped)
                {
                    changeButtonImage(actionButton, "Resources\\enroll.png");
                    this.actionText.Content = "Enrolled";
                    this.actionText.Background = Brushes.Blue;
                    this.actionText.Foreground = Brushes.White;
                }
                else if (c.dropped)
                {
                    changeButtonImage(actionButton, "Resources\\drop.png");
                    this.actionText.Content = "Dropped";
                    this.actionText.Background = Brushes.Red;
                    this.actionText.Foreground = Brushes.White;
                }
                else if (c.waitListed)
                {
                    changeButtonImage(actionButton, "Resources\\inCart.png");
                    this.actionText.Content = "Waitlisted";
                    this.actionText.Background = Brushes.Blue;
                    this.actionText.Foreground = Brushes.White;
                }
            }else
            {
                if (view == null)
                {
                    changeButtonImage(actionButton, "Resources\\inCart.png");
                    this.actionText.Background = Brushes.Blue;
                    this.actionText.Foreground = Brushes.White;
                }
                return 1;
            }
            return 0;
        }

        private void AddLectureSections(List<Lecture> list)
        {
            if(list.Count() == 0)
            {
                return;
            }
            SectionComboBox s = new SectionComboBox(list, ref this.classInstance, this);
            this.sectionsGrid.Children.Add(s);
            Grid.SetRow(s, 0);     

        }

        public void AddLectureDescription()
        {
            if (classInstance.lecturesList.Count() == 0)
            {
                return;
            }
            SectionDescription s = new SectionDescription(this.classInstance, classInstance.lecturesList[classInstance.lectureNum]);
            this.classInfoGrid.Children.Add(s);
            Grid.SetRow(s, 0);
        }

        private void AddTutorialSections(List<Tutorial> list)
        {
            if (list.Count() == 0)
            {
                return;
            }
            SectionComboBox s = new SectionComboBox(list, ref this.classInstance, this);
            this.sectionsGrid.Children.Add(s);
            Grid.SetRow(s, 1);

        }

        public void AddTutorialDescription()
        {
            if (classInstance.tutorialsList.Count() == 0)
            {
                return;
            }
            SectionDescription s = new SectionDescription(this.classInstance, classInstance.tutorialsList[classInstance.tutorialNum]);
            this.classInfoGrid.Children.Add(s);
            Grid.SetRow(s, 1);
        }

        private void AddLabSections(List<Lab> list)
        {
            if (list.Count() == 0)
            {
                return;
            }
            SectionComboBox s = new SectionComboBox(list, ref this.classInstance, this);
            this.sectionsGrid.Children.Add(s);
            Grid.SetRow(s, 2);

        }

        public void AddLabDescription()
        {
            if (classInstance.labsList.Count() == 0)
            {
                return;
            }
            SectionDescription s = new SectionDescription(this.classInstance, classInstance.tutorialsList[classInstance.labNum]);
            this.classInfoGrid.Children.Add(s);
            Grid.SetRow(s, 2);
        }

        public void swapRequested(ClassInstance c)
        {
            //TODO - Check if the new class to swap to has space
            if (!(this.parent != null && this.parent is EnrollmentView))
            {
                return;
            }
            changeButtonImage(actionButton, "Resources\\swap.png");
            this.actionText.Content = "Swapping";

            if (this.parent != null && this.parent is EnrollmentView)
            {
                EnrollmentView view = (EnrollmentView)this.parent;
                view.changeToConfirmAction();
            }
        }

        /*
        private void deleteCourse(object sender, RoutedEventArgs e)
        {
            (this.Parent as Panel).Children.Remove(this);
            RockEnrollHelper.RemoveCourse(classInstance);
        }*/


        //Renamed the deleteCourse to something generic as we will need to reuse for Enrollment too
        private void actionCourse(object sender, RoutedEventArgs e)
        {
            switch (this.getActionMode())
            {
                case ACTION_DELETE:
                    string messageText = this.classInstance.department.ToString() + " " + this.classInstance.courseID.ToString();
                    MessageBoxResult d;
                    d = MessageBox.Show( "Do you want to remove the following course from the cart? \r\n" + messageText, " ", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    if ((d == MessageBoxResult.OK))
                    {
                        if (classInstance.enrolled)
                        {
                            this.actionText.Content = "Dropping";
                            this.actionText.Background = Brushes.Red;
                            this.actionText.Foreground = Brushes.White;
                            break;
                        }
                        if (view)
                        {
                            RockEnrollHelper.RemoveCourse(classInstance);
                            this.actionMode = ACTION_ADD;
                            this.actionText.Background = Brushes.White;
                            this.actionText.Foreground = Brushes.White;
                            changeButtonImage(actionButton, "Resources\\greenplus.png");
                            break;
                        }
                    (this.Parent as Panel).Children.Remove(this);
                        RockEnrollHelper.RemoveCourse(classInstance);
                    }
                    break;
                case ACTION_ENROLL:
                    //showMessage("Attempting to enroll", "Enroll");
                    this.panelDropDown.Visibility = Visibility.Visible;
                    this.menuDropDown.Visibility = Visibility.Visible;
                    break;
                case ACTION_ADD:
                    RockEnrollHelper.AddCourse(classInstance);
                    changeButtonImage(actionButton, "Resources\\trash-can.png");
                    this.actionText.Content = "In Cart";
                    this.actionText.Background = Brushes.Blue;
                    this.actionText.Foreground = Brushes.White;
                    this.actionMode = ACTION_DELETE;
                    break;
                default:
                    break;
            }

        }

        private void checkBoxAction(object sender, RoutedEventArgs e)
        {
            if (CourseCheckBox.IsChecked == false)
            {
                this.saveActionText = this.actionText.Content.ToString();
                this.actionText.Content = "Dropping";
                //TODO - somehow change the button to "Enrollment Checkout"
            }
            else
            {
                this.actionText.Content = this.saveActionText;
            }

        }

        private void inCartAction(object sender, RoutedEventArgs e)
        {
            this.panelDropDown.Visibility = Visibility.Collapsed;
            this.menuDropDown.Visibility = Visibility.Collapsed;
            //TODO
            changeButtonImage(actionButton, "Resources\\inCart.png"); //check mark
            this.actionText.Content = "In Cart";
            this.actionText.Background = Brushes.Blue;
            this.actionText.Foreground = Brushes.White;
        }

        private void enrollAction(object sender, RoutedEventArgs e)
        {
            this.panelDropDown.Visibility = Visibility.Collapsed;
            this.menuDropDown.Visibility = Visibility.Collapsed;
            //TODO
            changeButtonImage(actionButton, "Resources\\enroll.png"); //check mark
            this.actionText.Content = "Enrolling";
            this.actionText.Background = Brushes.Blue;
            this.actionText.Foreground = Brushes.White;
        }

        private void dropAction(object sender, RoutedEventArgs e)
        {
            this.panelDropDown.Visibility = Visibility.Collapsed;
            this.menuDropDown.Visibility = Visibility.Collapsed;
            
            changeButtonImage(actionButton, "Resources\\drop.png"); //check mark
            this.actionText.Content = "Dropping";
            this.actionText.Background = Brushes.Red;
            this.actionText.Foreground = Brushes.White;

            //TODO - signal the parent class to refresh the buttons below
            /*EnrollmentView enrollView = (EnrollmentView)this.Parent;
            MainWindow mainWindow = (MainWindow)enrollView.Parent;
            mainWindow.enrollButton.Content = "Confirm Actions";*/

            if (this.parent != null && this.parent is EnrollmentView)
            {
                EnrollmentView view = (EnrollmentView)this.parent;
                view.changeToConfirmAction();
            }
       
        }

        private void swapAction(object sender, RoutedEventArgs e)
        {
            this.panelDropDown.Visibility = Visibility.Collapsed;
            this.menuDropDown.Visibility = Visibility.Collapsed;
            //TODO //only allow if there is space in the new class
            changeButtonImage(actionButton, "Resources\\swap.png"); //check mark
            this.actionText.Content = "Swapping";
            this.actionText.Background = Brushes.Yellow;
            this.actionText.Foreground = Brushes.White;
        }

        public void setActionMode(int actionMode)
        {
            this.actionMode = actionMode;
        }

        public int getActionMode()
        {
            return this.actionMode;
        }

        public void showMessage(String messageTitle, String messageText)
        {
            //Console.WriteLine(messageTitle + "\t" + messageText);

            MessageBoxResult d;
            d = MessageBox.Show(messageTitle, messageText, MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (d == MessageBoxResult.Yes)
            {
                //Close(); //The Close() didnt work.
            }
        }

        public static void changeButtonImage(Button button, String resoureName)
        {
            Uri resourceUri = new Uri(resoureName, UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
            BitmapFrame bitmap = BitmapFrame.Create(streamInfo.Stream);
            var brush = new ImageBrush();
            brush.ImageSource = bitmap;
            button.Background = brush;
        }

        private void notificationStatus(object sender, RoutedEventArgs e)
        {

                string messageTitle = "Caution: Action item needs your attention.";
                string messageText;
                if ((bool)bell.IsChecked)
                {
                    messageText = "Do you wish to enable notifications (to your UofC email) for the following course?\r\n";
                }
                else
                {
                    messageText = "Do you wish to disable notifications (to your UofC email) for the following course?\r\n";
                }

                messageText += this.classInstance.department.ToString() + " " + this.classInstance.courseID.ToString();
                MessageBoxResult d;
                d = MessageBox.Show(messageText, messageTitle, MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (!(d == MessageBoxResult.OK))
                {
                    bell.Checked -= notificationStatus;
                    bell.Unchecked -= notificationStatus;
                    bell.IsChecked = bell.IsChecked = (!bell.IsChecked);
                    bell.Checked += notificationStatus;
                    bell.Unchecked += notificationStatus;
            }
            
        }

        private void showtimesClick(object sender, MouseButtonEventArgs e)
        {
            RockEnrollHelper.schedulePath = "Resources/showalltimes.png";
            RockEnrollHelper._coursePage.updateScheduleImage();

        }

        public void tobeImplemented()
        {
            //Console.WriteLine(messageTitle + "\t" + messageText);

            MessageBoxResult d;
            d = MessageBox.Show("To be Implemented", "", MessageBoxButton.OK, MessageBoxImage.Information);
            if (d == MessageBoxResult.Yes)
            {
                //Close(); //The Close() didnt work.
            }
        }

    }
}
