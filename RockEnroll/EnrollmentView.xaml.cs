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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace RockEnroll
{
    /// <summary>
    /// Interaction logic for EnrollmentView.xaml
    /// </summary>
    public partial class EnrollmentView : UserControl
    {

        public EnrollmentView()
        {
            InitializeComponent();
            for (int i = 0; i < RockEnrollHelper.student.currentSchedule.Count(); i++)
            {

                AddClass(RockEnrollHelper.student.currentSchedule[i]);
            }

        }

        public void AddClass(ClassInstance c)
        {
            CourseView view = new CourseView(ref c);
            Button actionButton = view.actionButton;
            changeButtonImage(actionButton, "Resources\\inCart.png");
            view.setActionMode(CourseView.ACTION_ENROLL);

            this.courseListViewer.Children.Add(view);
        }

        public void checkAllCourses()
        {
            this.courseListViewer.ToolTip = "checkAllCourses";
            for ( int i = 0; i<this.courseListViewer.Children.Count; i++)
            {
                CourseView view = (CourseView)this.courseListViewer.Children[i];
                //view.ToolTip = "Hello Kylie: " + i; //temp for now
                Button actionButton = view.actionButton;
                changeButtonImage(actionButton, "Resources\\enroll.png");

            }
        }

        public bool confirmCourses()
        {
            this.courseListViewer.ToolTip = "confirmCourses";
            //TODO
            return true; //TODO
        }

        public void finishCourses()
        {
            this.courseListViewer.ToolTip = "finishCourses";
            //TODO
        }

        public void changeButtonImage(Button button, String resoureName)
        {
            Uri resourceUri = new Uri(resoureName, UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
            BitmapFrame bitmap = BitmapFrame.Create(streamInfo.Stream);
            var brush = new ImageBrush();
            brush.ImageSource = bitmap;
            button.Background = brush;
        }
    }
}
