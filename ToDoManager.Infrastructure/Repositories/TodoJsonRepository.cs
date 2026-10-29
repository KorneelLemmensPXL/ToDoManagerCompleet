using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using ToDoManger.Domain.Models;

namespace ToDoManager.Infrastructure.Repositories
{
    public class TodoJsonRepository
    {
        const string filename = "todos.json";

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public TodoJsonRepository()
        {
            if (!File.Exists(filename))
            {
                File.WriteAllText(filename, "[ ]");
            }
        }

        public List<ToDoItem> GetAll()
        {
            string jsonContent = File.ReadAllText(filename);
            List<ToDoItem>? items = JsonSerializer.Deserialize<List<ToDoItem>>(jsonContent);
            return items ?? new List<ToDoItem>();
        }

        public ToDoItem? Get(int id)
        {
            List<ToDoItem> todos = GetAll();

            ToDoItem? existing = todos.Find(item => item.Id == id);

            //if (existing == null)
            //{
            //    throw new Exception($"Item met id {id} bestaat niet.");
            //}
            return existing;
        }

        public void Add(ToDoItem item)
        {
            List<ToDoItem> todos = GetAll();
            ToDoItem? maxItem = todos.MaxBy(i => i.Id);

            if (maxItem is null)
            {
                item.Id = 1;
            }
            else
            {
                item.Id = maxItem.Id + 1;
            }

            todos.Add(item);
            string jsonContent = JsonSerializer.Serialize(todos, options);
            File.WriteAllText(filename, jsonContent);
        }

        public bool Remove(ToDoItem item)
        {
            List<ToDoItem> todos = GetAll();
            bool removed = todos.RemoveAll(i => i.Id == item.Id) > 0;
            string jsonContent = JsonSerializer.Serialize(todos, options);
            File.WriteAllText(filename, jsonContent);
            return removed;

        }

        public void Update(ToDoItem item)
        {
            List<ToDoItem> allTodos = GetAll();
            int index = allTodos.FindIndex(i => i.Id == item.Id);

            if(index != -1)
            {
                allTodos[index] = item;
                string jsonContent = JsonSerializer.Serialize(allTodos, options);
                File.WriteAllText(filename, jsonContent);
            }
            else
            {
                throw new InvalidOperationException($"Item met id {item.Id} bestaat niet.");
            }

        }
    }
}
