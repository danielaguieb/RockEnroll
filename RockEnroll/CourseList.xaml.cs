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
using System.Windows.Shapes;

namespace RockEnroll
{
    /// <summary>
    /// Interaction logic for CourseList.xaml
    /// </summary>
    public partial class CourseList : UserControl
    {
        public CourseList()
        {
            InitializeComponent();
            
            
        }

        /*
        public int findAvailableTime(Course course)
        {
            for (int i = 0; i < course.lecturesList.Count(); i++) {
                bool conflict = false;
                for(int j = 0; j < student.currentSchedule.Count(); i++)
                {
                    int z = student.currentSchedule[j].lectureNum;
                    if (course.lecturesList[i].time.Equals(student.currentSchedule[j].lecturesList[z].time))
                    {
                        conflict = true;
                        break;
                    }
                }
                if (!conflict)
                {
                    return i;
                }
            }
            return 1;
        }
        */
        public void AddClass(ClassInstance c)
        {
            CourseView view = new CourseView(ref c, true);
            courseListViewer.RowDefinitions.Add(new RowDefinition());
            this.courseListViewer.Children.Add(view);
            Grid.SetRow(view, courseListViewer.RowDefinitions.Count - 1);
            displayConflict(view);

        }

        public void updateSections()
        {
            foreach (CourseView c in courseListViewer.Children)
            {
                c.classInfoGrid.Children.Clear();

                c.AddLectureDescription();

                c.AddTutorialDescription();
                c.AddLabDescription();
                displayConflict(c);
               

            }
        }

        public void displayConflict(CourseView c)
        {
            List<(ClassInstance, int, int, int)> conflicts = RockEnrollHelper.FindConflict(c.ClassInstance);
            if (conflicts.Count() != 0)
            {
                c.conflictText.Visibility = Visibility.Visible;
                string text = "Conflict with: ";
                for (int i = 0; i < conflicts.Count(); i++)
                {
                    text += conflicts[i].Item1.department.ToString() + " " + conflicts[i].Item1.courseID.ToString() + ": ";
                    if (conflicts[i].Item2 != -1)
                    {
                        text += "LEC 0" + (conflicts[i].Item2 + 1).ToString() + " ";
                    }

                    if (conflicts[i].Item3 != -1)
                    {
                        text += "TUT 0" + (conflicts[i].Item3 + 1).ToString() + " ";
                    }
                    if (conflicts[i].Item4 != -1)
                    {
                        text += "LAB 0" + (conflicts[i].Item3 + 1).ToString() + " ";
                    }

                    if (i != conflicts.Count() - 1)
                    {
                        text += ", ";
                    }
                }

                c.conflictText.Content = text;

            }
            else
            {
                c.conflictText.Content = "";
                c.conflictText.Visibility = Visibility.Hidden;
            }
        }
    }
}
