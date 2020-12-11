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
using System.Windows.Shapes;

namespace Day08TodoListDB
{
    /// <summary>
    /// Interaction logic for AddEditTodoDialog.xaml
    /// </summary>
    public partial class AddEditTodoDialog : Window
    {
        public event Action<Todo> AddNewTodoCallback;

        public AddEditTodoDialog()
        {
            InitializeComponent();
        }

        public AddEditTodoDialog(Todo todo)
        {
            InitializeComponent();
            if (todo != null)
            {
                tbId.Text = todo.Id + string.Empty;
                tbTask.Text = todo.Task;
                dpDueDate.SelectedDate = todo.DueDate;
                if (todo.Status.Equals(TaskStatusEnum.Done))
                {
                    ckbDone.IsChecked = true;
                }
                else
                {
                    ckbDone.IsChecked = false;
                }
                btSave.Content = "Update";
            }
            else
            {
                btSave.Content = "Add";
            }
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbTask.Text) || dpDueDate.SelectedDate.Equals(null))
                {
                    MessageBox.Show("Please input value", "Information");
                    return;
                }
                int id;
                int.TryParse(tbId.Text, out id);
                string taskStr = tbTask.Text;
                DateTime dueDate = dpDueDate.SelectedDate.Value;
                bool? isDone = ckbDone.IsChecked;
                AddNewTodoCallback?.Invoke(new Todo(id, taskStr, dueDate, isDone == true? TaskStatusEnum.Done : TaskStatusEnum.Pending));
                DialogResult = true;
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show(ex.Message, "Error Information");
            }
            //
        }
    }
}
