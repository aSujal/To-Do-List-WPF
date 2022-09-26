using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace To_DoList.Themes
{
    /// <summary>
    /// Interaktionslogik für ListItem.xaml
    /// </summary>
    public partial class TaskBox : ListBoxItem
    {
        public TaskBoxStructure Structure { get => this.DataContext as TaskBoxStructure; }
        privMethods methods = new privMethods();
        
        public TaskBox()
        {
            InitializeComponent();
        }

        private void Task_Complete_Click(object sender, MouseButtonEventArgs e)
        {
            
            if (Task_Text.TextDecorations == TextDecorations.Strikethrough)
            {
                Task_Text.TextDecorations = null;
                Task_Text.Foreground = new SolidColorBrush(Colors.Crimson);

            }
            else
            {
                Task_Text.TextDecorations = TextDecorations.Strikethrough;
                Task_Text.Foreground = new SolidColorBrush(Colors.Green);
            }

        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
    public class TaskBoxStructure
    {
        public string? TaskName { get; set; }
    }
}
