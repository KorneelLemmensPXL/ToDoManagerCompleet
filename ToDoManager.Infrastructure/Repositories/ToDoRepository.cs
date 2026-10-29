using System;
using System.Collections.Generic;
using System.Text;
using ToDoManger.Domain.Models;

namespace ToDoManager.Infrastructure.Repositories
{
    public class ToDoRepository
    {
        private readonly List<ToDoItem> _todos;

        public ToDoRepository()
        {
            _todos = new List<ToDoItem>();
        }

        public List<ToDoItem> GetAll()
        {
            return new List<ToDoItem>(_todos);
        }

        public ToDoItem? Get(int id)
        {
            return _todos.Find(todo => todo.Id == id);
        }

        public void Add(ToDoItem item)
        {
            item.Id = _todos.Count + 1;
            _todos.Add(item);
        }

        public bool Remove(ToDoItem item)
        {
            return _todos.Remove(item);
        }
    }
}
