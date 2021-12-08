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
using System.Windows.Shapes;

namespace RockEnroll
{
    /// <summary>
    /// Interaction logic for SearchBarResultsContainer.xaml
    /// </summary>
    public partial class SearchBarResultsContainer : Window
    {
        public int itemPanelHeight { get; set; }
        //public List<Course> courseList { get; set; }
        public List<Tuple<Course, string>> courseList;
        public double xPosition { get; set; }
        public double yPosition { get; set; }
        public int itemCount
        {
            get
            {
                return courseList.Count();
            }
        }
        private SearchBar searchBar;

        public SearchBarResultsContainer(SearchBar searchBar)
        {
            InitializeComponent();
            courseList = new();
            listView.ItemsSource = courseList;
            this.ShowActivated = false;
            this.searchBar = searchBar;
        }

        public void UpdateContents(string searchTerm)
        {
            courseList.Clear();

            List<Course> courseListToSearch = RockEnrollHelper.allCourses;

            if (RockEnrollHelper.CurrentTerm == Terms.FALL2021)
            {
                courseListToSearch = RockEnrollHelper.allCourses;
            } else if (RockEnrollHelper.CurrentTerm == Terms.SPRING2022)
            {
                courseListToSearch = RockEnrollHelper.spring22Courses;
            } else if (RockEnrollHelper.CurrentTerm == Terms.SUMMER2022)
            {
                courseListToSearch = RockEnrollHelper.summer22Courses;
            } else if (RockEnrollHelper.CurrentTerm == Terms.WINTER2022)
            {
                courseListToSearch = RockEnrollHelper.winter22Courses;
            }

            if (searchTerm != "")
            {

                foreach (Course course in courseListToSearch)
                {

                    if ((course.department.ToString() + " " + course.courseID).StartsWith(searchTerm.ToUpper()) && !RockEnrollHelper.student.CurrentlyTaking(course, RockEnrollHelper.CurrentTerm))
                    {
                        bool prereqMet = true;
                        foreach (Course prerequisite in course.prerequisites)
                        {
                            if (!RockEnrollHelper.student.coursesTaken.Contains(prerequisite))
                            {
                                prereqMet = false;
                            }
                        }

                        string icon = "Resources/greenPlus.png";
                        if (!prereqMet) icon = "Resources/caution.png";

                        courseList.Add(new Tuple<Course, string>(course, icon));
                    }

                }
            }

            if (courseList.Count() == 0)
            {
                this.Visibility = Visibility.Hidden;

            } else
            {
                this.Visibility = Visibility.Visible;
            }

            listView.Items.Refresh();

        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (courseList.Count() == 0)
            {

                

            }
        }

        public void SelectCourse()
        {
            if (listView.SelectedItems.Count > 0)
            {
                Course courseToAdd = ((Tuple<Course, string>)listView.SelectedItems[0]).Item1;
                if (!RockEnrollHelper.student.currentSchedule.Contains(courseToAdd))
                {
                    RockEnrollHelper.AddCourse(courseToAdd);
                }
            }

            this.UpdateContents(searchBar.Text);

        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            ListViewItem listViewItem = sender as ListViewItem;
            if (listViewItem != null)
            {
                listViewItem.IsSelected = true;
                listView.SelectedItem = listViewItem;
                SelectCourse();
            }
        }

        public void SetLocation(Point point)
        {
            this.Left = point.X;
            this.Top = point.Y + searchBar.Height + 5;
            //this.Width = 515;
            this.Width = searchBar.ActualWidth;
        }
    }
}
