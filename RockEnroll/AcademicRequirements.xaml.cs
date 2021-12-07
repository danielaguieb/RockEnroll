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
            InitializeReqList();
        }

        private void InitializeReqList()
        {
            ReqExpander q = insertreqhere.Children[0] as ReqExpander;
            q.reqname.Text = "Core Requirments";

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
