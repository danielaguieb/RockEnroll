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
            RockEnrollHelper.InitializeCourses();
            AddCourse(RockEnrollHelper.allCourses[0]);
            
            
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
            ClassInstance classInstance = new ClassInstance(course, 1,1, 0);
            CourseView view = new CourseView(classInstance);
            view.HorizontalAlignment = HorizontalAlignment.Stretch;

            this.courseListViewer.Children.Add(view);
        }
    }
}
