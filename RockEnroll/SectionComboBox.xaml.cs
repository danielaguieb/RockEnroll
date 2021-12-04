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
    public partial class SectionComboBox: UserControl
    {
        public SectionComboBox(List<Lecture> items)
        {
            InitializeComponent();
            this.section.Content = "LEC:";
            this.comboBox.ItemsSource = items;
        }

        public SectionComboBox(List<Tutorial> items)
        {
            InitializeComponent();
            this.section.Content = "TUT:";
            this.comboBox.ItemsSource = items;
        }

        public SectionComboBox(List<Lab> items)
        {
            InitializeComponent();
            this.section.Content = "LAB:";
            this.comboBox.ItemsSource = items;
        }
    }
}
