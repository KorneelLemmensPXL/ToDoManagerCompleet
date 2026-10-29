using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ToDoManager.WPF
{
    /// <summary>
    /// Interaction logic for AddTodoWindow.xaml
    /// </summary>
    public partial class AddTodoWindow : Window
    {
        public string TodoTitle { get; private set; } = "";
        public string? TodoDescription { get; private set; }
        public DateTime TodoDueDate { get; private set; }

        public AddTodoWindow()
        {
            InitializeComponent();

            dueDatePicker.SelectedDate = DateTime.Today;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.TodoTitle = titleTextBox.Text;
            this.TodoDescription = descriptionTextBox.Text;
            this.TodoDueDate = dueDatePicker.SelectedDate!.Value;

            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
