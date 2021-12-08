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
    /// Interaction logic for AcademicRequirements.xaml
    /// </summary>
    public partial class AcademicRequirements : UserControl
    {
        public AcademicRequirements()
        {
            InitializeComponent();
        }

        public void InitializeReqList()
        {
            //First expander
            ReqExpander q = insertreqhere.Children[0] as ReqExpander;
            q.reqname.Text = "Core Requirements";
            ClassInstance c = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 311 && x.department == Department.SOCI), Terms.FALL2021, 1);
            CourseView n = new CourseView(ref c, true);
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

            c = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 313 && x.department == Department.SOCI), Terms.FALL2021, 0);
            n = new CourseView(ref c, true);
            q.reqs.Children.Add(n);

            c = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 201 && x.department == Department.SOCI), Terms.FALL2021, 0);
            n = new CourseView(ref c, true);
            q.reqs.Children.Add(n);

            q = insertreqhere.Children[1] as ReqExpander;
            q.reqname.Text = "Advanced-Level Sociology";

            q = insertreqhere.Children[2] as ReqExpander;
            q.reqname.Text = "Sociology Options";

            q = insertreqhere.Children[3] as ReqExpander;
            q.reqname.Text = "Options";

            q = insertreqhere.Children[4] as ReqExpander;
            q.reqname.Text = "Faculty of Science Options";
            q.bordee.Background = Brushes.LightGreen;
            q.check.Opacity = 100;

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
            but1.Click += but1_Click ;
            q.reqs.Children.Add(but1);

            q = insertreqhere.Children[3] as ReqExpander;

            Button but2 = new Button();
            but2.Content = "Explore options";
            but2.Click += but2_Click;
            q.reqs.Children.Add(but2);

            q = insertreqhere.Children[4] as ReqExpander;

            Button but3 = new Button();
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
