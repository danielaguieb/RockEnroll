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
    public class PlaceholderTextBox : TextBox
    {

        private bool isTyping, textEmpty;
        public string placeholderText { get; set; }

        public PlaceholderTextBox()
        {
            PreviewMouseLeftButtonDown += _OnMouseDown;
            this.TextChanged += _TextChanged;
        }

        public void _OnMouseDown(object sender, MouseEventArgs e)
        {
            isTyping = true;
        }

        public void _TextChanged(object sender, RoutedEventArgs e)
        {
            

        }

    }
}
