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
    /// Interaction logic for SectionDescription.xaml
    /// </summary>
    public partial class SectionDescription : UserControl
    {
        public SectionDescription(ClassInstance c, Lecture lec)
        {
            InitializeComponent();
            this.sectionText.Content = "LEC 0" + (c.lectureNum + 1).ToString();
            this.campusText.Content = lec.campus.ToString();
            this.timeText.Content = getTime(lec) + " " + lec.time.StartTime + "-" + lec.time.EndTime;
            this.roomText.Content = lec.room;
            this.seatsText.Content = "Seats: " + lec.currentStudents.ToString() + "/" + lec.maxStudents.ToString();
            this.waitlistText.Content = "WaitList: " + lec.currentWaitlist.ToString() + "/" + lec.maxWaitlist.ToString();
            this.instructorText.Content = lec.instructor;
            this.noteText.Content = lec.notes;
        }

        public SectionDescription(ClassInstance c, Tutorial tut)
        {
            InitializeComponent();
            this.sectionText.Content = "TUT 0" + (c.tutorialNum + 1).ToString();
            this.campusText.Content = tut.campus.ToString();
            this.timeText.Content = getTime(tut) + " " + tut.time.StartTime + "-" + tut.time.EndTime;
            this.roomText.Content = tut.room;
            this.seatsText.Content = "Seats: " + tut.currentStudents.ToString() + "/" + tut.maxStudents.ToString();
            this.waitlistText.Content = "WaitList: " + tut.currentWaitlist.ToString() + "/" + tut.maxWaitlist.ToString();
            this.instructorText.Content = tut.instructor;
            this.noteText.Content = tut.notes;
        }

        public SectionDescription(ClassInstance c, Lab lab)
        {
            InitializeComponent();
            this.sectionText.Content = "LAB 0" + (c.labNum + 1).ToString();
            this.campusText.Content = lab.campus.ToString();
            this.timeText.Content = getTime(lab) + " " + lab.time.StartTime + "-" + lab.time.EndTime;
            this.roomText.Content = lab.room;
            this.seatsText.Content = "Seats: " + lab.currentStudents.ToString() + "/" + lab.maxStudents.ToString();
            this.waitlistText.Content = "WaitList: " + lab.currentWaitlist.ToString() + "/" + lab.maxWaitlist.ToString();
            this.instructorText.Content = lab.instructor;
            this.noteText.Content = lab.notes;
        }

        public string getTime<T>(T t) where T:Assignable
        {
            string days = "";
            if (t.time.monday)
            {
                days += "M";
            }
            if (t.time.tuesday)
            {
                days += "T";
            }
            if (t.time.wednesday)
            {
                days += "W";
            }
            if (t.time.thursday)
            {
                days += "R";
            }
            if (t.time.friday)
            {
                days += "F";
            }

            return days;
        }
    }
}
