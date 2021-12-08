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
    /// Interaction logic for Schedules.xaml
    /// </summary>
    public partial class Schedules : UserControl
    {
        public Schedules()
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
    }
}
