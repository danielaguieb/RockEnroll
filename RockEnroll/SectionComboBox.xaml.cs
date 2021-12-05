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
    /// Interaction logic for SectionComboBox.xaml
    /// </summary>
    public partial class SectionComboBox : UserControl
    {
        public SectionComboBox(List<Lecture> list, ref ClassInstance classInstance)
        {
            InitializeComponent();

            this.section.Content = "LEC:";
            this.comboBox.ItemsSource = getItemSource(list);
            this.classInstance = classInstance;
            this.sectionType = 0;
            this.comboBox.SelectedIndex = classInstance.lectureNum;
            

        }

        public SectionComboBox(List<Tutorial> list, ref ClassInstance classInstance)
        {
            InitializeComponent();

            this.section.Content = "TUT:";
            this.comboBox.ItemsSource = getItemSource(list);
            this.classInstance = classInstance;
            this.sectionType = 1;
            this.comboBox.SelectedIndex = classInstance.tutorialNum;

        }
        public SectionComboBox(List<Lab> list, ref ClassInstance classInstance)
        {
            InitializeComponent();

            this.section.Content = "LAB:";
            this.comboBox.ItemsSource = getItemSource(list);
            this.classInstance = classInstance;
            this.sectionType = 3;
            this.comboBox.SelectedIndex = classInstance.labNum;

        }

        private ClassInstance classInstance { get; set; }

        //0 is lecture, 1 is tutorial, 2 is lab
        private int sectionType { get; set; }

        private List<ClassSection> getItemSource<T>(List<T> list) where T: Assignable
        {

            List<ClassSection>  items = new List<ClassSection> ();
            for (int i = 0; i < list.Count(); i++)
            {
                string days = "";
                if (list[i].time.monday)
                {
                    days += "M";
                }
                if (list[i].time.tuesday)
                {
                    days += "T";
                }
                if (list[i].time.wednesday)
                {
                    days += "W";
                }
                if (list[i].time.thursday)
                {
                    days += "R";
                }
                if (list[i].time.friday)
                {
                    days += "F";
                }

                string item = "0" + (i + 1).ToString() + " " + days + ": " + list[i].time.StartTime + "-" + list[i].time.EndTime;
                ClassSection s = new ClassSection(item, i);
                items.Add(s);
            }

            return items;
        }

        private void SectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            int i = (int)this.comboBox.SelectedValue;
            switch (this.sectionType)
            {
                case 0:
                    RockEnrollHelper.SwapSection(this.classInstance, i, this.classInstance.tutorialNum, this.classInstance.lectureNum);
                    break;
                case 1:
                    RockEnrollHelper.SwapSection(this.classInstance, this.classInstance.lectureNum, i, this.classInstance.lectureNum);
                    break;
                case 2:
                    RockEnrollHelper.SwapSection(this.classInstance, this.classInstance.lectureNum, this.classInstance.tutorialNum, i);
                    break;

            }


        }

    }

    public class ClassSection
    {
        public string displayText { get; set; }

        public int index { get; set; }

        public ClassSection(string text, int i)
        {
            displayText = text;
            index = i;
        }
    }



}
