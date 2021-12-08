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
    /// Interaction logic for ScheduleView.xaml
    /// </summary>
    
    public partial class ScheduleView : UserControl
    {
        private ClassInstance classInstance;
        private string courseNameText;

        public ScheduleView(ref ClassInstance c)
        {
            InitializeComponent();
            this.classInstance = c;
            this.courseNameText = c.department.ToString() + "" + c.courseID.ToString();
        }
    }
}