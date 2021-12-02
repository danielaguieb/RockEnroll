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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RockEnroll
{
    /// <summary>
    /// Interaction logic for CourseView.xaml
    /// </summary>
    public partial class CourseView : UserControl
    {
        public CourseView(Course course)
        {
            InitializeComponent();
        }

        private string courseName;
        public string CourseName
        {
            get { return courseName; }
            set { 
                courseName = value;
                this.courseNameText.Content = this.courseName;
            }
        }


        private string courseTitle;
        public string CourseTitle { 
            get { return courseTitle; }
            set
            {
                courseTitle = value;
                this.courseTitleText.Content = this.courseTitle;
            }
        }

        private string campus;
        public string Campus{
            get { return campus; }
            set
            {
                campus = value;
                this.campusText.Content = this.campus;
            }
        }

        private void deleteCourse(object sender, RoutedEventArgs e)
        {
            (this.Parent as Panel).Children.Remove(this);
        }
    }
}
