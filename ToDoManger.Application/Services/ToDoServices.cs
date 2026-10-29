using System;
using System.Collections.Generic;
using System.Text;
using ToDoManager.Infrastructure.Repositories;
using ToDoManger.Domain.Models;

namespace ToDoManger.Application.Services
{
    public class ToDoService
    {
        private readonly TodoCSVRepository _repository;
        private readonly FileLogger _logger;

        public ToDoService(string path)
        {
            _repository = new TodoCSVRepository(path);
            _logger = new FileLogger();
        }

        public List<ToDoItem> GetTodos()
        {
            return _repository.GetAll();
        }

        public void AddTodo(string title, string? description, DateTime dueDate)
        {
            if(_repository.GetAll().Any(t => string.Equals(t.Title, title.Trim(), StringComparison.OrdinalIgnoreCase)))   // gebruik hier Any ipv Find => any kijkt of het bestaat en geeft een bool terug en Find geeft het gevonden element terug
            {
                throw new InvalidOperationException("Er bestaat al een todo met deze Titel");
            }

            ToDoItem todo = new ToDoItem(title, description, dueDate);
            _repository.Add(todo);

            _logger.Log("ADD", $"TodoItem: {todo.Title}");
        }

        public void CompleteTodo(ToDoItem todo)
        {
            ToDoItem? item = _repository.Get(todo.Id);
            if(item is null)
            {
                throw new InvalidOperationException("Geen todo gevonden");
                
            }

            item.MarkAsCompleted();
            _repository.Update(item);

            _logger.Log("COMPLETE", $"TodoItem: {item.Title}");
        }

        public void DeleteTodo(ToDoItem todo)
        {
            if (todo.DueDate.Date < DateTime.Today)
            {
                throw new InvalidOperationException("De deadline van deze Todo is al verstreken");
            }

            if (todo.IsCompleted)
            {
                throw new InvalidOperationException("Je kan geen todo verwijderen die al completed is");
            }

            _repository.Remove(todo);

            _logger.Log("REMOVE", $"TodoItem: {todo.Title}");
        }
    }
}
