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
        public CourseView(ref ClassInstance classInstance)
        {
            InitializeComponent();
            this.classInstance = classInstance;
            this.courseNameText.Content = classInstance.department.ToString() + "\n" + classInstance.courseID.ToString();
            this.courseTitleText.Content = classInstance.courseTitle;
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

        private void deleteCourse(object sender, RoutedEventArgs e)
        {
            (this.Parent as Panel).Children.Remove(this);
            RockEnrollHelper.RemoveCourse(classInstance);
        }
    }
}
