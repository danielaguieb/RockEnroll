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
            ReqExpander q = insertreqhere.Children[0] as ReqExpander;
            q.reqname.Text = "Core Requirments";
            ClassInstance c = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 311 && x.department == Department.SOCI), Terms.FALL2021, 1);
            CourseView n = new CourseView(ref c, true);
            q.reqs.Children.Add(n);

            ClassInstance c1 = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 331 && x.department == Department.SOCI), Terms.FALL2021, 0);
            CourseView n1 = new CourseView(ref c1, true);
            q.reqs.Children.Add(n1);

            ClassInstance c2 = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 315 && x.department == Department.SOCI), Terms.FALL2021, 0);
            CourseView n2 = new CourseView(ref c2, true);
            q.reqs.Children.Add(n2);

            ClassInstance c3 = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 333 && x.department == Department.SOCI), Terms.FALL2021, 0);
            CourseView n3 = new CourseView(ref c3, true);
            q.reqs.Children.Add(n3);

            ClassInstance c4 = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 313 && x.department == Department.SOCI), Terms.FALL2021, 0);
            CourseView n4 = new CourseView(ref c4, true);
            q.reqs.Children.Add(n4);

            ClassInstance c5 = new ClassInstance(RockEnrollHelper.allCourses.Find(x => x.courseID == 201 && x.department == Department.SOCI), Terms.FALL2021, 0);
            CourseView n5 = new CourseView(ref c5, true);
            q.reqs.Children.Add(n5);

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
        }

    
    }
}
