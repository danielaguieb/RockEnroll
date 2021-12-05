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
    /// Interaction logic for CourseView.xaml
    /// </summary>
    public partial class CourseView : UserControl
    {
        public CourseView(ClassInstance classInstance)
        {
            InitializeComponent();
            this.classInstance = classInstance;
            this.courseNameText.Content = classInstance.department.ToString() + "\n" + classInstance.courseID.ToString();
            this.courseTitleText.Content = classInstance.courseTitle;
            AddSectionComboBox(classInstance.lecturesList);
            AddSectionComboBox(classInstance.tutorialsList);
            AddSectionComboBox(classInstance.labsList);

        }

        private ClassInstance classInstance;
        public ClassInstance ClassInstance
        {
            get { return classInstance; }
            set {
                classInstance = value;
            }
        }

        private void AddSectionComboBox<T>(List<T> list) where T: Assignable
        {
            if(list.Count() == 0)
            {
                return;
            }

            List<string> items = new List<string>(); 
            for (int i = 0; i < list.Count(); i++)
            {
                string days = "";
                if (list[i].time.monday)
                {
                    days += "M";
                }
                if (list[i].time.tuesday)
                {
                    days += "T";
                }
                if (list[i].time.wednesday)
                {
                    days += "W";
                }
                if (list[i].time.thursday)
                {
                    days += "R";
                }
                if (list[i].time.friday)
                {
                    days += "F";
                }

                string item = "0" + (i+1).ToString() + " " + days + list[i].time.StartTime + "-" + list[i].time.EndTime;
                items.Add(item);
            }
            //this.sectionsGrid.Children.Add(new SectionComboBox(items));
        }

        private void deleteCourse(object sender, RoutedEventArgs e)
        {
            (this.Parent as Panel).Children.Remove(this);
        }
        private void enrollCourse(object sender, RoutedEventArgs e)
        {
            //TODO(this.Parent as Panel).Children.Enroll(this);
        }
    }
}
