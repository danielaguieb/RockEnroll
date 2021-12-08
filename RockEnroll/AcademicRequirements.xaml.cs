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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace RockEnroll
{
    /// <summary>
    /// Interaction logic for AcademicRequirements.xaml
    /// </summary>
    public partial class AcademicRequirements : UserControl
    {
        public AcademicRequirements()
        {
            InitializeComponent();
        }

        public void updateScheduleImage()
        {
            changeImage(this.schedule, RockEnrollHelper.schedulePath);
        }

        public void updateTimelineImage()
        {
            changeImage(this.timeline, RockEnrollHelper.timelinePath);
        }

        public static void changeImage(Image img, String resoureName)
        {
            Uri resourceUri = new Uri(resoureName, UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
            BitmapFrame bitmap = BitmapFrame.Create(streamInfo.Stream);
            img.Source = bitmap;
        }

        public void InitializeReqList()
        {
            BrushConverter bc = new BrushConverter();
            //First expander
            ReqExpander q = insertreqhere.Children[0] as ReqExpander;
            q.reqname.Text = "Core Requirements";
            q.unitscomplete.Text = "Completed: 3.0 out of 18.0 units";
            q.unitscomplete.ToolTip = new ToolTip() { Content = "Units are a measurement system used to indicate the weight of a course.Most courses are 3.0 units" };
            ClassInstance c = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 311 && x.department == Department.SOCI), Terms.FALL2021, 1);
            CourseView n = new CourseView(ref c, true);
            q.reqs.Children.Add(n);

            c = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 313 && x.department == Department.SOCI), Terms.FALL2021, 0);
            n = new CourseView(ref c, true);
            q.reqs.Children.Add(n);

            c = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 331 && x.department == Department.SOCI), Terms.FALL2021, 0);
            n = new CourseView(ref c, true);
            q.reqs.Children.Add(n);


            c = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 315 && x.department == Department.SOCI), Terms.FALL2021, 0);
            n = new CourseView(ref c, true);
            q.reqs.Children.Add(n);

            c = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 333 && x.department == Department.SOCI), Terms.FALL2021, 0);
            n = new CourseView(ref c, true);
            q.reqs.Children.Add(n);

            c = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 201 && x.department == Department.SOCI), Terms.FALL2021, 0);
            n = new CourseView(ref c, true);
            q.reqs.Children.Add(n);

            c = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 321 && x.department == Department.SOCI), Terms.FALL2021, 0);
            n = new CourseView(ref c, true);
            recommended.Children.Add(n);

            c = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 313 && x.department == Department.SOCI), Terms.FALL2021, 0);
            n = new CourseView(ref c, true);
            recommended.Children.Add(n);

            c = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 331 && x.department == Department.SOCI), Terms.FALL2021, 0);
            n = new CourseView(ref c, true);
            recommended.Children.Add(n);

            Button but4 = new Button();
            but4.Content = "Option";
            but4.Margin = new Thickness(5);
            but4.Click += but2_Click;
            recommended.Children.Add(but4);


            q = insertreqhere.Children[1] as ReqExpander;
            q.reqname.Text = "Advanced-Level Sociology";
            q.unitscomplete.Text = "Completed: 0.0 out of 12.0 units";
            q.unitscomplete.ToolTip = new ToolTip() { Content = "Units are a measurement system used to indicate the weight of a course.Most courses are 3.0 units" };


            q = insertreqhere.Children[2] as ReqExpander;
            q.reqname.Text = "Sociology Options";
            q.unitscomplete.Text = "Completed: 0.0 out of 12.0 units";
            q.unitscomplete.ToolTip = new ToolTip() { Content = "Units are a measurement system used to indicate the weight of a course.Most courses are 3.0 units" };

            q = insertreqhere.Children[3] as ReqExpander;
            q.reqname.Text = "Options";
            q.unitscomplete.Text = "Completed: 0.0 out of 72.0 units";
            q.unitscomplete.ToolTip = new ToolTip() { Content = "Units are a measurement system used to indicate the weight of a course.Most courses are 3.0 units" };

            q = insertreqhere.Children[4] as ReqExpander;
            q.reqname.Text = "Faculty of Science Options";
            q.expandee.Background = (Brush)bc.ConvertFrom("#FFF0FFF0");
            q.check.Opacity = 100;
            q.unitscomplete.Text = "Completed: 6.0 out of 6.0 units";
            q.unitscomplete.ToolTip = new ToolTip() { Content = "Units are a measurement system used to indicate the weight of a course.Most courses are 3.0 units" };

            //Second

            q = insertreqhere.Children[2] as ReqExpander;

            c = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 321 && x.department == Department.SOCI), Terms.FALL2021, 0);
            n = new CourseView(ref c, true);
            q.reqs.Children.Add(n);

            c = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 325 && x.department == Department.SOCI), Terms.FALL2021, 0);
            n = new CourseView(ref c, true);
            q.reqs.Children.Add(n);

            c = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 371 && x.department == Department.SOCI), Terms.FALL2021, 0);
            n = new CourseView(ref c, true);
            q.reqs.Children.Add(n);

            q = insertreqhere.Children[1] as ReqExpander;

            Button but1 = new Button();
            but1.Content = "Explore options";
            but1.Margin = new Thickness(5);
            but1.Click += but1_Click ;
            q.reqs.Children.Add(but1);

            q = insertreqhere.Children[3] as ReqExpander;

            Button but2 = new Button();
            but2.Margin = new Thickness(5);
            but2.Content = "Explore options";
            but2.Click += but2_Click;
            q.reqs.Children.Add(but2);

            q = insertreqhere.Children[4] as ReqExpander;

            Button but3 = new Button();
            but3.Margin = new Thickness(5);
            but3.Content = "Explore options";
            but3.Click += but3_Click;
            q.reqs.Children.Add(but3);


            /*
            foreach (Course i in RockEnrollHelper.allCourses)
            {
                if ((i.department != Department.SOCI) || (i.courseID < 400)) continue;
                c = new ClassInstance(i, Terms.FALL2021, 0);
                n = new CourseView(ref c, true);
                q.reqs.Children.Add(n);
            }

            q = insertreqhere.Children[3] as ReqExpander;
            foreach (Course i in RockEnrollHelper.allCourses)
            {
                if (i.department == Department.SOCI) continue;
                c = new ClassInstance(i, Terms.FALL2021, 0);
                n = new CourseView(ref c, true);
                q.reqs.Children.Add(n);
            }
            */
            q = insertreqhere.Children[4] as ReqExpander;
            foreach (Course i in RockEnrollHelper.allCourses)
            {
                if (i.faculty != Faculty.Science) continue;
                c = new ClassInstance(i, Terms.FALL2021, 0);
                n = new CourseView(ref c, true);
                q.reqs.Children.Add(n);
            }

        }

        private void but1_Click(object sender, RoutedEventArgs e)
        {
            if (RockEnrollHelper._advsearch == null) RockEnrollHelper._advsearch = new();
            RockEnrollHelper._advsearch.Show();
            RockEnrollHelper._advsearch.pubreset();
            RockEnrollHelper._advsearch.searchbar.Text = "";
            RockEnrollHelper._advsearch.coursenum.Text = "399";
            RockEnrollHelper._advsearch.subjectbox.SelectedIndex = (int)Department.SOCI;
            RockEnrollHelper._advsearch.cngt.IsChecked = true;
            RockEnrollHelper._advsearch.searchInstance.courseCompare = Search.Compares.GT;
            RockEnrollHelper._advsearch.pubapply();
        }

        private void but2_Click(object sender, RoutedEventArgs e)
        {
            if (RockEnrollHelper._advsearch == null) RockEnrollHelper._advsearch = new();
            RockEnrollHelper._advsearch.Show();
            RockEnrollHelper._advsearch.pubreset();
            RockEnrollHelper._advsearch.searchbar.Text = "";
            RockEnrollHelper._advsearch.pubapply();
        }

        private void but3_Click(object sender, RoutedEventArgs e)
        {
            if (RockEnrollHelper._advsearch == null) RockEnrollHelper._advsearch = new();
            RockEnrollHelper._advsearch.Show();
            RockEnrollHelper._advsearch.pubreset();
            RockEnrollHelper._advsearch.faculty.SelectedIndex = (int)Faculty.Science;
            RockEnrollHelper._advsearch.searchbar.Text = "";
            RockEnrollHelper._advsearch.pubapply();
        }

    }
}
