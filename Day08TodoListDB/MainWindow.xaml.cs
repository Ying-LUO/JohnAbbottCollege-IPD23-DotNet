using CsvHelper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
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

namespace Day08TodoListDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Todo> todoList = new List<Todo>();
        private Todo currTodo;
        Database db;
        string operation = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            db = new Database();
            loadDataFromDatabase();
            lstView.ItemsSource = todoList;
            sortedList.DataContext = todoList;
        }

        private void loadDataFromDatabase()
        {
            try
            {
                todoList = db.GetAllTodos();
                foreach (Todo t in todoList)
                {
                    Console.WriteLine(t.ToString());
                }
                operation = "Load data from database";
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show("Error loading data from database: " + ex.Message, "Error Information");
            }
        }

        private void CommandBinding_Exit(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void CommandBinding_ExportToCsv(object sender, ExecutedRoutedEventArgs e)
        {// feature ask to export all records
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";  
            saveFileDialog.Title = "Export to csv";
            try
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var writer = new StreamWriter(saveFileDialog.FileName))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(todoList);
                    }
                }
                operation = "Export to csv";
                lstView.SelectedIndex = 0;//for status bar selection change
            }
            catch (Exception ex) when (ex is IOException || ex is DataInvalidException)
            {
                MessageBox.Show("Error saving data into csv file: " + ex.Message, "Error Information");
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddEditTodoDialog addEditDialog = new AddEditTodoDialog(null);
                addEditDialog.Owner = this;

                addEditDialog.AddNewTodoCallback += (t) => { currTodo = t; };
                bool? result = addEditDialog.ShowDialog();  

                if (result == true)
                {
                    int newId = db.AddTodo(currTodo);
                    currTodo.Id = newId;
                    todoList.Add(currTodo);
                    lstView.Items.Refresh();
                }
                operation = "Add new Todo";
                lstView.SelectedIndex = lstView.Items.Count - 1;//for status bar selection change
                
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show(ex.Message, "Error Information");
            }
        }

        private void miDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstView.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose at least one item to delete", "Information");
                return;
            }
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure to delete?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            try
            {
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Todo todo = (Todo)lstView.SelectedItem;
                    db.DeleteTodo(todo.Id);
                    todoList.Remove(todo);
                    lstView.Items.Refresh();
                }
                operation = "Delete one record";
                lstView.SelectedIndex = 0;
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show(ex.Message, "Error Information");
            }
        }

        private void Update_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstView.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose one item to update", "Information");
                return;
            }

            try
            {
                int index = lstView.SelectedIndex;
                AddEditTodoDialog addEditDialog = new AddEditTodoDialog((Todo)lstView.SelectedItem);
                addEditDialog.Owner = this;

                addEditDialog.AddNewTodoCallback += (c) => { currTodo = c; };
                bool? result = addEditDialog.ShowDialog();  // this line must be stay after the assignment, otherwise value is not assigned

                if (result == true)
                {
                    db.UpdateTodo(currTodo);
                    todoList.ElementAt(index).Id = currTodo.Id;
                    todoList.ElementAt(index).Task = currTodo.Task;
                    todoList.ElementAt(index).DueDate = currTodo.DueDate;
                    todoList.ElementAt(index).Status = currTodo.Status;
                }
                lstView.Items.Refresh();
                operation = "Update one record";
                lstView.SelectedIndex = 0;
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show(ex.Message, "Error Information");
            }
        }

        
        private void SortByTask_Checked(object sender, RoutedEventArgs e)
        {
            List<Todo> sortedList = db.GetAllTodos().OrderBy(t => t.Task).ToList();
            lstView.ItemsSource = sortedList;
        }

        private void SortByDueDate_Checked(object sender, RoutedEventArgs e)
        {
            List<Todo> sortedList = db.GetAllTodos().OrderBy(t => t.DueDate).ToList();
            lstView.ItemsSource = sortedList;
        }

        private void StatusBar(object sender, RoutedEventArgs e)
        { 
            lblStatus.Text = string.Format("Status of last operation: " + operation);
        }
    }
}
