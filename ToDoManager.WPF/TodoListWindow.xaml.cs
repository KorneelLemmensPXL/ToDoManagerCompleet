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
using ToDoManger.Application.Services;
using ToDoManger.Domain.Models;

namespace ToDoManager.WPF
{
    /// <summary>
    /// Interaction logic for TodoListWindow.xaml
    /// </summary>
    public partial class TodoListWindow : Window
    {
        private readonly ToDoService _todoService;
        public TodoListWindow()
        {
            InitializeComponent();
            _todoService = new ToDoService();
            RefreshTodos();
            
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshTodos();
        }

        private void RefreshTodos()
        {
            todosListBox.Items.Clear();

            List<ToDoItem> todos = _todoService.GetTodos();

            foreach (ToDoItem todo in todos)
            {
                todosListBox.Items.Add(todo); // relies on TodoItem.ToString()
            }

            ClearDetails();
        }

        private void ClearDetails()
        {
            titleTextBlock.Text = "";
            dueDateTextBlock.Text = "";
            completedTextBlock.Text = "";
            completedAtTextBlock.Text = "";
            descriptionTextBlock.Text = "";
        }

        private void TodosListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(todosListBox.SelectedItem is not ToDoItem todo)
            {
                ClearDetails();
                return;
            }

            titleTextBlock.Text = todo.Title;
            dueDateTextBlock.Text = todo.DueDate.ToString("dd/MM/yyyy");
            descriptionTextBlock.Text = todo.Description ?? string.Empty;
            completedTextBlock.Text = todo.IsCompleted ? "ja" : "nee";
            completedAtTextBlock.Text = todo.CompletedAt?.ToString("dd/MM/yyyy");
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddTodoWindow addWindow = new AddTodoWindow();
            addWindow.Owner = this;

            bool? result = addWindow.ShowDialog();
            if (result != true)
            {
                return;
            }

            try
            {
                _todoService.AddTodo(addWindow.TodoTitle, addWindow.TodoDescription, addWindow.TodoDueDate);
                RefreshTodos();
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (todosListBox.SelectedItem is not ToDoItem todo)
            {
                MessageBox.Show("Geen geldig todoitem geselecteerd");
                return;
            }

            try
            {
                _todoService.CompleteTodo(todo);
                
                RefreshTodos();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (todosListBox.SelectedItem is not ToDoItem todo)
            {
                MessageBox.Show("Geen geldig todoitem geselecteerd");
                return;
            }

            try
            {
                _todoService.DeleteTodo(todo);
                RefreshTodos();
            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
