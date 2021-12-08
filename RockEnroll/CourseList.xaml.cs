using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
using Color = System.Windows.Media.Color;

namespace RockEnroll
{
    /// <summary>
    /// Interaction logic for CourseList.xaml
    /// </summary>
    public partial class CourseList : UserControl
    {
        public CourseList()
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
        public void AddClass(ClassInstance c, bool v)
        {
            CourseView view = new CourseView(ref c, v);
            courseListViewer.RowDefinitions.Add(new RowDefinition());
            this.courseListViewer.Children.Add(view);
            Grid.SetRow(view, courseListViewer.RowDefinitions.Count - 1);
            changeImage(this.schedule, RockEnrollHelper.schedulePath);
            changeImage(this.timeline, RockEnrollHelper.timelinePath);
            displayConflict(view);

        }

        public void updateSections()
        {
            foreach (CourseView c in courseListViewer.Children)
            {
                c.classInfoGrid.Children.Clear();

                c.AddLectureDescription();

                c.AddTutorialDescription();
                c.AddLabDescription();
                displayConflict(c);
               

            }
        }

        public void displayConflict(CourseView c)
        {
            List<(ClassInstance, int, int, int)> conflicts = RockEnrollHelper.FindConflict(c.ClassInstance);
            if (conflicts.Count() != 0)
            {
                c.conflictText.Visibility = Visibility.Visible;
                string text = "Conflict with: ";
                for (int i = 0; i < conflicts.Count(); i++)
                {
                    text += conflicts[i].Item1.department.ToString() + " " + conflicts[i].Item1.courseID.ToString() + ": ";
                    if (conflicts[i].Item2 != -1)
                    {
                        text += "LEC 0" + (conflicts[i].Item2 + 1).ToString() + " ";
                    }

                    if (conflicts[i].Item3 != -1)
                    {
                        text += "TUT 0" + (conflicts[i].Item3 + 1).ToString() + " ";
                    }
                    if (conflicts[i].Item4 != -1)
                    {
                        text += "LAB 0" + (conflicts[i].Item3 + 1).ToString() + " ";
                    }

                    if (i != conflicts.Count() - 1)
                    {
                        text += ", ";
                    }
                }

                c.conflictText.Content = text;

            }
            else
            {
                c.conflictText.Content = "";
                c.conflictText.Visibility = Visibility.Hidden;
            }
        }

    }
}
