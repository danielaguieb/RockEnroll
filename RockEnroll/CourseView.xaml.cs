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
    /// Interaction logic for CourseView.xaml
    /// </summary>
    public partial class CourseView : UserControl
    {
        public const int ACTION_DELETE = 1;
        public const int ACTION_ENROLL = 2;

        private int actionMode = ACTION_DELETE;
        private String saveActionText;

        public CourseView(ref ClassInstance classInstance)
        {
            InitializeComponent();
            this.classInstance = classInstance;
            this.courseNameText.Content = classInstance.department.ToString() + "\n" + classInstance.courseID.ToString();
            this.courseTitleText.Content = classInstance.courseTitle;
            if (classInstance.enrolled)
            {
                this.actionText.Content = "Enrolled";
                // -- Checkbox is checked so when it is unchecked (by the user) it indicates a drop action
                this.CourseCheckBox.IsChecked = true;
            }
            saveActionText = this.actionText.Content.ToString();
            AddLectureSections(classInstance.lecturesList);
            AddLectureSections(classInstance.lecturesList);
            AddTutorialSections(classInstance.tutorialsList);
            AddLabSections(classInstance.labsList);

        }

        private ClassInstance classInstance;
        public ClassInstance ClassInstance
        {
            get { return classInstance; }
            set {
                classInstance = value;
            }
        }

        private void AddLectureSections(List<Lecture> list)
        {
            if(list.Count() == 0)
            {
                return;
            }
            SectionComboBox s = new SectionComboBox(list, ref this.classInstance);
            this.sectionsGrid.Children.Add(s);
            Grid.SetRow(s, 0);

        }

        private void AddTutorialSections(List<Tutorial> list)
        {
            if (list.Count() == 0)
            {
                return;
            }
            SectionComboBox s = new SectionComboBox(list, ref this.classInstance);
            this.sectionsGrid.Children.Add(s);
            Grid.SetRow(s, 1);

        }

        private void AddLabSections(List<Lab> list)
        {
            if (list.Count() == 0)
            {
                return;
            }
            SectionComboBox s = new SectionComboBox(list, ref this.classInstance);
            this.sectionsGrid.Children.Add(s);
            Grid.SetRow(s, 2);

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
                    (this.Parent as Panel).Children.Remove(this);
                    RockEnrollHelper.RemoveCourse(classInstance);
                    break;
                case ACTION_ENROLL:
                    //showMessage("Attempting to enroll", "Enroll");
                    this.panelDropDown.Visibility = Visibility.Visible;
                    this.menuDropDown.Visibility = Visibility.Visible;
                    //if button label is Remove, do the removal accordingly
                    //else if enroll, do the appropriate action
                    Panel panel = (this.Parent as Panel);
                    //TODO
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
        }

        private void enrollAction(object sender, RoutedEventArgs e)
        {
            this.panelDropDown.Visibility = Visibility.Collapsed;
            this.menuDropDown.Visibility = Visibility.Collapsed;
            //TODO
            changeButtonImage(actionButton, "Resources\\enroll.png"); //check mark
            this.actionText.Content = "Enrolling";
        }

        private void dropAction(object sender, RoutedEventArgs e)
        {
            this.panelDropDown.Visibility = Visibility.Collapsed;
            this.menuDropDown.Visibility = Visibility.Collapsed;
            //TODO
            changeButtonImage(actionButton, "Resources\\drop.png"); //check mark
            this.actionText.Content = "Dropping";

        }

        private void swapAction(object sender, RoutedEventArgs e)
        {
            this.panelDropDown.Visibility = Visibility.Collapsed;
            this.menuDropDown.Visibility = Visibility.Collapsed;
            //TODO
            changeButtonImage(actionButton, "Resources\\swap.png"); //check mark
            this.actionText.Content = "Swapping";
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
