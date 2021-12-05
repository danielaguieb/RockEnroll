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
    /// Interaction logic for EnrollmentView.xaml
    /// </summary>
    public partial class EnrollmentView : UserControl
    {

        public EnrollmentView()
        {
            InitializeComponent();
            //RockEnrollHelper.InitializeCourses();
            for (int i = 0; i < RockEnrollHelper.pickedCourses.Count(); i++)
            {
                AddCourse(RockEnrollHelper.pickedCourses[i]);
            }

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
        public void AddCourse(Course course)
        {
            ClassInstance classInstance = new ClassInstance(course, 1, 1, 0);
            CourseView view = new CourseView(classInstance);
            view.deleteButton.IsEnabled = false;
            view.deleteButton.InvalidateVisual();
            view.cartButton.IsEnabled = true;
            view.cartButton.BringIntoView();

            this.courseListViewer.Children.Add(view);
        }

        public void checkAllCourses()
        {
            this.courseListViewer.ToolTip = "checkAllCourses";
            for ( int i = 0; i<this.courseListViewer.Children.Count; i++)
            {
                CourseView view = (CourseView)this.courseListViewer.Children[i];
                view.ToolTip = "Hello Kylie: " + i; //temp for now
                //view.cartButton.SetResourceReference("Resources\\checkMark.png"); //TODO
            }
        }

        public bool confirmCourses()
        {
            //TODO
            return true; //TODO
        }
    }
}
