using System;
using System.Collections.Generic;
using System.Text;
using ToDoManger.Application.Services;

namespace TodoServiceTests
{
    public class TodoServiceTests
    {
        [Fact]
        public void AddTodo_DuplicateTitle_ThrowsInvalidOperationException()
        {
            // Arrange
            ToDoService service = new ToDoService();

            service.AddTodo("Buy milk", null, DateTime.Today.AddDays(1));

            // Act + Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                service.AddTodo("  BUY MILK  ", "Duplicate", DateTime.Today.AddDays(2));
            });
        }

        [Fact]
        public void DeleteTodo_PastDueDate_ThrowsInvalidOperationException()
        {
            // Arrange
            ToDoService service = new ToDoService();

            service.AddTodo("Old task", null, DateTime.Today.AddDays(-1));

            var todos = service.GetTodos();
            var todoToDelete = todos[0];

            // Act + Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                service.DeleteTodo(todoToDelete);
            });
        }

        [Fact]
        public void DeleteTodo_CompletedTodo_ThrowsInvalidOperationException()
        {
            // Arrange
            ToDoService service = new ToDoService();

            service.AddTodo("Finish assignment", null, DateTime.Today.AddDays(2));

            var todos = service.GetTodos();
            var todo = todos[0];

            service.CompleteTodo(todo);

            // Act + Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                service.DeleteTodo(todo);
            });
        }

        [Fact]
        public void CompleteTodo_SetsCompletedAt()
        {
            // Arrange
            ToDoService service = new ToDoService();

            service.AddTodo("Read chapter", null, DateTime.Today.AddDays(3));

            var todos = service.GetTodos();
            var todo = todos[0];

            // Act
            service.CompleteTodo(todo);

            // Assert
            Assert.True(todo.IsCompleted);
            Assert.NotNull(todo.CompletedAt);
        }


    }
}
