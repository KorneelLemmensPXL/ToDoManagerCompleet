using System;
using System.Collections.Generic;
using System.Text;
using ToDoManger.Domain.Models;

namespace TodoManager.Domain.Tests
{
    public class TodoItemTests
    {
        [Fact]
        public void Constructor_EmptyTitle_ThrowsArgumentException()
        {
            // Arrange
            string title = "";
            string? description = null;
            DateTime dueDate = DateTime.Today.AddDays(1);

            // Act + Assert
            Assert.Throws<ArgumentException>(() =>
            {
                ToDoItem item = new ToDoItem(title, description, dueDate);
            });
        }

        [Fact]
        public void Constructor_WhitespaceTitle_ThrowsArgumentException()
        {
            // Arrange
            string title = "   ";
            string? description = "Some description";
            DateTime dueDate = DateTime.Today.AddDays(1);

            // Act + Assert
            Assert.Throws<ArgumentException>(() =>
            {
                ToDoItem item = new ToDoItem(title, description, dueDate);
            });
        }

        [Fact]
        public void Constructor_NullDescription_DoesNotThrow()
        {
            // Arrange
            string title = "Buy milk";
            string? description = null;
            DateTime dueDate = DateTime.Today.AddDays(1);

            // Act
            ToDoItem item = new ToDoItem(title, description, dueDate);

            // Assert
            Assert.Null(item.Description);
            Assert.Equal("Buy milk", item.Title);
        }

        [Fact]
        public void MarkAsCompleted_SetsIsCompletedAndCompletedAt()
        {
            // Arrange
            ToDoItem item = new ToDoItem("Study C#", null, DateTime.Today.AddDays(3));

            // Act
            item.MarkAsCompleted();

            // Assert
            Assert.True(item.IsCompleted);
            Assert.NotNull(item.CompletedAt);
        }
    }
}
