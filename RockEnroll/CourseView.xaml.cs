﻿using System;
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

        public CourseView(ref ClassInstance c, bool view = false)
        {
            InitializeComponent();
            this.classInstance = c;
            this.courseNameText.Content = c.department.ToString() + "\n" + c.courseID.ToString();
            this.courseTitleText.Text = c.courseTitle;
            this.campusText.Content = c.lecturesList[c.lectureNum].campus.ToString();
            if (c.enrolled)
            {
                this.actionText.Content = "Enrolled";
                // -- Checkbox is checked so when it is unchecked (by the user) it indicates a drop action
                this.CourseCheckBox.IsChecked = true;
            }
            saveActionText = this.actionText.Content.ToString();
            AddLectureSections(c.lecturesList);
            AddLectureDescription();
            AddTutorialSections(c.tutorialsList);
            AddTutorialDescription();
            AddLabSections(c.labsList);
            AddLabDescription();
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
                        }
                        missingPrereq.Add(c.prerequisites[i]);
                        missingreq += c.prerequisites[i].department.ToString() + c.prerequisites[i].courseID.ToString();
                    }

                    if(i != c.prerequisites.Count() - 1)
                    {
                        missingreq += ", ";
                    }
                }

                this.mprereqText.Content = missingreq;
            }
            addRequisite(c.prerequisites, 0);
            addRequisite(c.antirequisites, 1);
            addRequisite(c.successors, 2);
            this.courseDesc.Text = "Description: " + c.courseDescription;
            BrushConverter bc = new BrushConverter();
            this.courseNameText.Background = (Brush)bc.ConvertFrom("#aebcd6ff");

            if (view)
            {
                this.setActionMode(ACTION_ADD);
                if (RockEnrollHelper.student.coursesTaken.Contains(c.lecturesList[0].course))
                {
                    changeButtonImage(actionButton, "Resources\\completed.png");
                    this.actionButton.IsHitTestVisible = false;
                    this.actionText.Content = "Completed";
                    this.actionText.Background = Brushes.Green;
                    this.actionText.Foreground = Brushes.White;
                    this.courseNameText.Background = (Brush)bc.ConvertFrom("#FFF0FFF0");

                }
                else if (RockEnrollHelper.student.currentSchedule.Contains(c.lecturesList[0].course))
                {
                    changeButtonImage(actionButton, "Resources\\in-progress.png");
                    this.actionButton.IsHitTestVisible = false;

                    this.courseNameText.Background = (Brush)bc.ConvertFrom("#FFDCDCDC");
                }
                else if(this.missingPrereq.Count() != 0)
                {
                    changeButtonImage(actionButton, "Resources\\warning.png");
                    this.actionText.Content = "Prerequisites Missing";
                    this.actionText.Background = Brushes.Yellow;
                    this.actionText.Foreground = Brushes.White;
                    this.courseExpander.Background = (Brush)bc.ConvertFrom("#FFFFFACD");
                    this.actionButton.IsHitTestVisible = false;
                }
                else
                {
                    this.actionText.Content = "";
                    changeButtonImage(actionButton, "Resources\\plus.png");
                }
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
            SectionComboBox s = new SectionComboBox(list, ref this.classInstance);
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
            SectionComboBox s = new SectionComboBox(list, ref this.classInstance);
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
                case ACTION_ADD:
                    RockEnrollHelper.AddCourse(classInstance);
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
            //TODO
            changeButtonImage(actionButton, "Resources\\drop.png"); //check mark
            this.actionText.Content = "Dropping";
            this.actionText.Background = Brushes.Red;
            this.actionText.Foreground = Brushes.White;

        }

        private void swapAction(object sender, RoutedEventArgs e)
        {
            this.panelDropDown.Visibility = Visibility.Collapsed;
            this.menuDropDown.Visibility = Visibility.Collapsed;
            //TODO
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
