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

namespace RockEnroll
{
    /// <summary>
    /// Interaction logic for AdvSearchWindow.xaml
    /// </summary>
    public partial class AdvSearchWindow : Window
    {
        public AdvSearchWindow()
        {
            InitializeComponent();
            //Load out filter form
            foreach (string name in Enum.GetNames(typeof(Campus)))
            {
                if (name == "NONE") continue;
                campus.Items.Add(name);
            }
            foreach (string name in Enum.GetNames(typeof(Faculty)))
            {
                if (name == "NONE") continue;
                faculty.Items.Add(name);
            }
            foreach (string name in Enum.GetNames(typeof(RockEnrollHelper.Terms)))
            {
                if (name == "NONE") continue;
                session.Items.Add(name);
            }

            //get subject list
            List<string> subjectlist = new();
            bool isnotunique = false;
            RockEnrollHelper.InitializeCourses();
            foreach (Course acourse in RockEnrollHelper.allCourses)
            {
                isnotunique = false;
                foreach (string subjectname in subjectlist)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (subjectname[i] != acourse.courseID[i])
                        {
                            break;
                        }
                        if (i == 3) isnotunique = true;
                    }

                    if (isnotunique) break;
                }

                if (isnotunique) break;
                string newsubject = acourse.courseID.Substring(0, 4);
                subjectlist.Add(newsubject);
            }

            foreach (string subjectname in subjectlist)
            {
                subjectbox.Items.Add(subjectname);
            }

        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }


}
