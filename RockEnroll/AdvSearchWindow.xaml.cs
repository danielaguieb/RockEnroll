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
using System.Text.RegularExpressions;

namespace RockEnroll
{
    /// <summary>
    /// Interaction logic for AdvSearchWindow.xaml
    /// </summary>
    public partial class AdvSearchWindow : Window
    {
        SearchResultPanel searchresultpanel = new();
        Search searchInstance = new("");
        public AdvSearchWindow()
        {
            InitializeComponent();

            //Load out filter form
            foreach (string name in Enum.GetNames(typeof(Campus)))
            {
                if (name == "NONE")
                {
                    campus.Items.Add("");
                    continue;
                }
                campus.Items.Add(name);
            }
            foreach (string name in Enum.GetNames(typeof(Faculty)))
            {
                if (name == "NONE")
                {
                    faculty.Items.Add("");
                    continue;
                }
                faculty.Items.Add(name);
            }
            foreach (string name in Enum.GetNames(typeof(Terms)))
            {
                if (name == "NONE")
                {
                    session.Items.Add("");
                    continue;
                }
                session.Items.Add(name);
            }

            foreach (int subjectint in Enum.GetValues(typeof(Department)))
            {
                if (subjectint == 0)
                {
                    subjectbox.Items.Add("");
                    continue;
                }
                subjectbox.Items.Add(Enum.GetName((Department)subjectint));
            }

            //Prepare search panel;
            putSearchHere.Children.Add(searchresultpanel);
            searchInstance.Proccess();
            searchresultpanel.updateResults(searchInstance);
        }


        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Owner.Activate();
        }

        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchInstance.searchstring = searchbar.Text;
        }

        private void Apply_Button_Click(object sender, RoutedEventArgs e)
        {
            searchInstance.Proccess();
            searchresultpanel.updateResults(searchInstance);
        }

        private void Reset_Filters_Button_Click(object sender, RoutedEventArgs e)
        {
            AdvSearchWindow newwindow = new();
            newwindow.Owner = this.Owner;
            this.Owner.Activate();
            newwindow.Show();
            this.Close();
        }

        private void campus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (campus.SelectedIndex == -1) searchInstance.campus = Campus.NONE;
            else searchInstance.campus = (Campus)campus.SelectedIndex;
        }

        private void session_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (session.SelectedIndex == -1) searchInstance.enrollmentterm = Terms.NONE;
            else searchInstance.enrollmentterm = (Terms)session.SelectedIndex;
        }

        private void faculty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (faculty.SelectedIndex == -1) searchInstance.faculty = Faculty.NONE;
            else searchInstance.faculty = (Faculty)faculty.SelectedIndex;
        }

        private void subjectbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (subjectbox.SelectedIndex == -1) searchInstance.subjectname = Department.NONE;
            else searchInstance.subjectname = (Department)subjectbox.SelectedIndex;
        }

        private void coursenum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (coursenum.Text.Length == 0) searchInstance.courseNumber = -1;
            else searchInstance.courseNumber = int.Parse(coursenum.Text);
        }

        private void courseunits_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (courseunits.Text.Length == 0) searchInstance.unitsNumber = -1;
            else searchInstance.unitsNumber = int.Parse(courseunits.Text);
        }

        private void coursenum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex coursenumcheck = new Regex("[^0-9]");
            e.Handled = (coursenumcheck.IsMatch(e.Text) || (e.Text.Length + coursenum.Text.Length) > 3);
        }

        private void courseunits_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            {
                Regex coursenumcheck = new Regex("[^0-9]");
                e.Handled = coursenumcheck.IsMatch(e.Text);
            }
        }

        Regex checkifcoursenum = new Regex("^cn");
        bool firsttime1 = true;
        bool firsttime2 = true;
        private void Box_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox boxsender = sender as CheckBox;
            if (checkifcoursenum.IsMatch(boxsender.Name))
            {
                if (!firsttime1)
                {
                    if (boxsender != cnct && (bool)cnct.IsChecked) cnct.IsChecked = false;
                    if (boxsender != cneq && (bool)cneq.IsChecked) cneq.IsChecked = false;
                    if (boxsender != cnlt && (bool)cnlt.IsChecked) cnlt.IsChecked = false;
                    if (boxsender != cngt && (bool)cngt.IsChecked) cngt.IsChecked = false;
                }
                firsttime1 = false;
                switch (boxsender.Name)
                {
                    case ("cnct"):
                        searchInstance.courseCompare = Search.Compares.CONTAINS;
                        break;
                    case ("cneq"):
                        searchInstance.courseCompare = Search.Compares.EQ;
                        break;
                    case ("cnle"):
                        searchInstance.courseCompare = Search.Compares.LT;
                        break;
                    case ("cngt"):
                        searchInstance.courseCompare = Search.Compares.GT;
                        break;
                }
            }
            else
            {
                if (!firsttime2)
                {
                    if (boxsender != cuct && (bool)cuct.IsChecked) cuct.IsChecked = false;
                    if (boxsender != cueq && (bool)cueq.IsChecked) cueq.IsChecked = false;
                    if (boxsender != cult && (bool)cult.IsChecked) cult.IsChecked = false;
                    if (boxsender != cugt && (bool)cugt.IsChecked) cugt.IsChecked = false;
                }
                firsttime2 = false;
                switch (boxsender.Name)
                {
                    case ("cuct"):
                        searchInstance.courseCompare = Search.Compares.CONTAINS;
                        break;
                    case ("cueq"):
                        searchInstance.courseCompare = Search.Compares.EQ;
                        break;
                    case ("cule"):
                        searchInstance.courseCompare = Search.Compares.LT;
                        break;
                    case ("cugt"):
                        searchInstance.courseCompare = Search.Compares.GT;
                        break;
                }
            }
        }
        private void Box_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox boxsender = sender as CheckBox;
            if (checkifcoursenum.IsMatch(boxsender.Name)) searchInstance.courseCompare = Search.Compares.NONE;
            else searchInstance.unitsCompare = Search.Compares.NONE;
        }

        private void Additional_Req_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox boxsender = sender as CheckBox;
            switch (boxsender.Name)
            {
                case "prereq":
                    searchInstance.prereq = true;
                    break;
                case "nonconflict":
                    searchInstance.nonConflict = true;
                    break;
                case "open":
                    searchInstance.open = true;
                    break;
                case "waitlist":
                    searchInstance.waitListable = true;
                    break;
                case "otherSemester":
                    searchInstance.otherSemester = true;
                    break;
            }
        }
        private void Additional_Req_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox boxsender = sender as CheckBox;
            switch (boxsender.Name)
            {
                case "prereq":
                    searchInstance.prereq = false;
                    break;
                case "nonconflict":
                    searchInstance.nonConflict = false;
                    break;
                case "open":
                    searchInstance.open = false;
                    break;
                case "waitlist":
                    searchInstance.waitListable = false;
                    break;
                case "otherSemester":
                    searchInstance.otherSemester = false;
                    break;
            }
        }
    }

}
