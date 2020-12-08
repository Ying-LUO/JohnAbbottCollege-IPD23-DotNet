using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using static Day05TodoList.Todo;

namespace Day05TodoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Todo> todoList = new ObservableCollection<Todo>();
        private StatusEnum status;
        private string path = @"..\..\todos.txt";
        private string text = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            readFromFile();
            cmbStatus.ItemsSource = Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>();
            lsViewTodo.ItemsSource = todoList;

        }

        private void readFromFile()
        {
            string[] dataStr = File.ReadAllLines(path);
            foreach(string s in dataStr)
            {
                string[] data = s.Split(';');
                if (data.Length != 4)
                {
                    throw new InvalidValueException("Failed on reading from file");
                }
                try
                {
                    int diff;
                    int.TryParse(data[1], out diff);
                    DateTime date;
                    DateTime.TryParse(data[2], out date);
                    StatusEnum status;
                    Enum.TryParse(data[3], out status);
                    todoList.Add(new Todo(data[0], diff, date, status));
                }
                catch (InvalidValueException ex)
                {
                    throw new InvalidValueException("Error in parsing data to Todo List" + ex.Message);
                }
                
            }
        }

        private void btExport_Click(object sender, RoutedEventArgs e)
        {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text File (*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == true)
                {
                    
                    foreach (Todo todo in todoList)
                    {
                        text += todo.ToString();
                    }
                File.WriteAllText(saveFileDialog.FileName, text);
                }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            File.WriteAllText(path, string.Empty);
            foreach (Todo todo in todoList)
            {
                text += todo.ToString();
            }
            File.WriteAllText(path, text);
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbTask.Text) || dpDueDate.SelectedDate.Equals(null) || cmbStatus.SelectedItem.Equals(null))
            {
                MessageBox.Show("Please input value");
                return;
            }
            Enum.TryParse(cmbStatus.Text, out status);
            todoList.Add(new Todo(tbTask.Text, (int)sldDiff.Value, dpDueDate.SelectedDate.Value, status));
            clearInput();
            lsViewTodo.Items.Refresh();
        }
        
        private void clearInput()
        {
            tbTask.Text = string.Empty;
            sldDiff.Value = 1;
            dpDueDate.SelectedDate = null;
            cmbStatus.SelectedIndex = -1;
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lsViewTodo.SelectedItems.Count!=1)
            {
                MessageBox.Show("Please choose one item to update");
                return;
            }
            int index = lsViewTodo.SelectedIndex;
            Enum.TryParse(cmbStatus.Text, out status);
            todoList.ElementAt(index).Task = tbTask.Text;
            todoList.ElementAt(index).Difficulty = (int)sldDiff.Value;
            todoList.ElementAt(index).DueDate = dpDueDate.SelectedDate.Value;
            todoList.ElementAt(index).Status = status;
            clearInput();
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lsViewTodo.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose at least one item to delete");
                return;
            }
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure to delete?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                todoList.RemoveAt(lsViewTodo.SelectedIndex);
                clearInput();
            }
        }

        private void listView_Click(object sender, SelectionChangedEventArgs e)
        {
            int index = lsViewTodo.SelectedIndex;
            Enum.TryParse(cmbStatus.Text, out status);
            tbTask.Text = todoList.ElementAt(index).Task;
            sldDiff.Value = todoList.ElementAt(index).Difficulty;
            dpDueDate.SelectedDate = todoList.ElementAt(index).DueDate;
            cmbStatus.Text = todoList.ElementAt(index).Status.ToString();
        }
    }
}
