using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace To_DoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HomePage homePage = new HomePage();
        SettingsPage settingsPage = new SettingsPage();
        privMethods methods = new privMethods();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Home_Btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = homePage;
            methods.dialogBox("Home", 60, MainPannel, 1000);
        }
        private void Settings_Btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = settingsPage;
            methods.dialogBox("Settings", 60, MainPannel, 1000);
        }

        private void NavigateBackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
            else
            {
                MainFrame.Navigate(homePage);
            }
        }

    }
    public class privMethods
    {
        Brush brush = new SolidColorBrush(Color.FromRgb(20, 20, 20));
        public async void dialogBox(string prompt, int BoxWidth, Grid grid, int Time)
        {
            Label label = new Label()
            {
                Content = prompt,
                Foreground = new SolidColorBrush(Colors.DimGray),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            Border dialogBox = new Border()
            {
                Width = BoxWidth,
                Height = 40,
                Background = brush,
                CornerRadius = new CornerRadius(8),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Child = label,
                Margin = new Thickness(0, 0, 0, 10)
            };
            DoubleAnimation animation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
            dialogBox.BeginAnimation(Border.OpacityProperty, animation);
            grid.Children.Add(dialogBox);
            await Task.Delay(Time);
            animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.5));
            dialogBox.BeginAnimation(Border.OpacityProperty, animation);
            await Task.Delay(500);
            grid.Children.Remove(dialogBox);

        }
    }

}
