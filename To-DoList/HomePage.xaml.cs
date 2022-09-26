using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using To_DoList.Themes;

namespace To_DoList
{
    /// <summary>
    /// Interaktionslogik für HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        privMethods methods = new privMethods();
        string PathToSave = string.Empty;
        public HomePage()
        {
            InitializeComponent();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskBoxTXT.Text == null || TaskBoxTXT.Text == "")
            {
                methods.dialogBox("Can't create an empty task", 180, MainPannel, 1500);
            }
            else
            {
                TaskBox newTask = new TaskBox();
                TaskBoxStructure task = new TaskBoxStructure();
                task.TaskName = TaskBoxTXT.Text;
                newTask.DataContext = task;
                ToDoList.Items.Add(newTask);
                TaskBoxTXT.Text = null;
            }
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ToDoList.Items.Clear();
            SaveBtn.Visibility = Visibility.Collapsed;
        }
        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            if (File.Exists(PathToSave) && (PathToSave != null || PathToSave != "") )
            {
                string[] TasksToSave = new string[ToDoList.Items.Count];
                foreach (TaskBox item in ToDoList.Items)
                {
                    if (item != null)
                    {
                        TasksToSave[i] = item.Task_Text.Text;
                        if (item.Task_Text.TextDecorations == TextDecorations.Strikethrough)
                        {
                            TasksToSave[i] += "./st";
                        }
                    }
                    i++;
                }
                File.WriteAllLines(PathToSave, TasksToSave);
                methods.dialogBox("Saved!", 80, MainPannel, 1500);
            }
            else
            {
                methods.dialogBox("Couldn't Find File", 150, MainPannel, 1500);
                await Task.Delay(2000);
                methods.dialogBox("Creating File", 100, MainPannel, 1500);
                File.WriteAllText(@"Data\Tasks.Txt", "");
                await Task.Delay(2000);
                methods.dialogBox("Created", 80, MainPannel, 1500);
            }
        }
        private async void SaveAsBtn_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            OpenFileDialog openFileDialog = new OpenFileDialog();
           
            //Start up location
            if (Directory.Exists(@"Data\"))
            {
                openFileDialog.InitialDirectory = @"Data\";
            }
            else
            {
                openFileDialog.InitialDirectory = @"C:\";
            }
            //

            if (openFileDialog.ShowDialog() == true)
            {
                PathToSave = openFileDialog.FileName;
                string[] TasksToSave = new string[ToDoList.Items.Count];
                foreach (TaskBox item in ToDoList.Items)
                {
                    if (item != null)
                    {
                        TasksToSave[i] = item.Task_Text.Text;
                        if (item.Task_Text.TextDecorations == TextDecorations.Strikethrough)
                        {
                            TasksToSave[i] += "./st";
                        }
                    }
                    i++;
                }
                File.WriteAllLines(PathToSave, TasksToSave);
                methods.dialogBox("Saved!", 80, MainPannel, 1500);
                SaveBtn.Visibility = Visibility.Visible;
                SaveBtn.Opacity = 0;
                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.5)
                };
                SaveBtn.BeginAnimation(OpacityProperty, animation);
            }
            else
            {
                methods.dialogBox("Couldn't Find File", 150, MainPannel, 1500);
                await Task.Delay(2000);
                methods.dialogBox("Creating File", 100, MainPannel, 1500);
                File.WriteAllText(@"Data\Tasks.Txt", "");
                await Task.Delay(2000);
                methods.dialogBox("Created", 80, MainPannel, 1500);

            }
        }
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            string PathToLoad = String.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //start up location
            if (Directory.Exists(@"Data\"))
            {
                openFileDialog.InitialDirectory = @"Data\";
            }
            else
            {
                openFileDialog.InitialDirectory = @"C:\";
            }
            //


            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                ToDoList.Items.Clear();
                PathToLoad = openFileDialog.FileName;
                string[] TasksToLoad = File.ReadAllLines(PathToLoad);
                foreach (string tasks in TasksToLoad)
                {
                    
                    TaskBox newTask = new TaskBox();
                    TaskBoxStructure taskStruct = new TaskBoxStructure();
                    string task = tasks;
                    if (tasks.Contains("./st"))
                    {   
                        task = TextWithOutCommand(tasks);
                        newTask.Task_Text.Foreground = new SolidColorBrush(Colors.Green);
                        newTask.Task_Text.TextDecorations = TextDecorations.Strikethrough;
                    }
                    taskStruct.TaskName = task;
                    newTask.DataContext = taskStruct;
                    ToDoList.Items.Add(newTask);
                    TaskBoxTXT.Text = null;
                }
                PathToSave = PathToLoad; //remember path
                SaveBtn.Opacity = 0;
                SaveBtn.Visibility = Visibility.Visible;
                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.5)
                };
                SaveBtn.BeginAnimation(OpacityProperty, animation);
            }
            else
            {
                methods.dialogBox("Couldn't Load!", 100, MainPannel, 1500);
            }
        }
        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                AddTask_Click(sender, new RoutedEventArgs());
            }
        }
        string TextWithOutCommand(string text)
        {
            string textReturn = string.Empty;
            for(int i = 0; i < text.Length; i++)
            {
                if (text[i] == '.' && text[i + 1] == '/')
                {
                    return textReturn;
                }
                else
                {
                    textReturn += text[i];
                }
            }
            return textReturn;
        }
    }
}
