using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using ToDoManger.Domain.Models;

namespace ToDoManager.Infrastructure.Repositories
{
    public class TodoCSVRepository
    {

        string filename;



        

        public TodoCSVRepository(string path)
        {

            filename = path;
            if (!File.Exists(filename))
            {
                File.WriteAllText(filename, "");  //Maakt een nieuw bestand aan
            }
        }

        public List<ToDoItem> GetAll()
        {
            List<ToDoItem> todos = new List<ToDoItem>();

            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine()!;

                    string[] values = line.Split(';',6);

                    int id = int.Parse( values[0]);
                    string title = values[1];
                    string description = values[2];
                    DateTime dueDate = DateTime.Parse(values[3]);
                    bool isCompleted = bool.Parse(values[4]);
                    DateTime? completedAt = DateTime.TryParse(values[5], out DateTime result) ? result : null;


                    ToDoItem student = new ToDoItem(id,title,description,dueDate,isCompleted,completedAt);
                    todos.Add(student);
                }
            }

            return todos;
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

        public void Add(ToDoItem newTodo)
        {
            List<ToDoItem> todos = GetAll();
            ToDoItem? maxItem = todos.MaxBy(i => i.Id);

            if (maxItem is null)
            {
                newTodo.Id = 1;
            }
            else
            {
                newTodo.Id = maxItem.Id + 1;
            }

            todos.Add(newTodo);
            //string jsonContent = JsonSerializer.Serialize(todos, options);
            //File.WriteAllText(filename, jsonContent);

            File.AppendAllText(filename, $"{newTodo.Id};{newTodo.Title};{newTodo.Description};{newTodo.DueDate};{newTodo.IsCompleted};{newTodo.CompletedAt}\n");
        }

        public bool Remove(ToDoItem item)
        {
            List<ToDoItem> todos = GetAll();
            bool removed = todos.RemoveAll(i => i.Id == item.Id) > 0;

            using (StreamWriter writer = new StreamWriter(filename, false)) // false = overschrijven
            {
                foreach (ToDoItem s in todos)
                {
                    writer.WriteLine($"{s.Id};{s.Title};{s.Description};{s.DueDate};{s.IsCompleted};{s.CompletedAt}");
                }
            }

            return removed;
        }

        public void Update(ToDoItem item)
        {
            List<ToDoItem> allTodos = GetAll();
            int index = allTodos.FindIndex(i => i.Id == item.Id);

            if (index != -1)
            {
                allTodos[index] = item;
                //string jsonContent = JsonSerializer.Serialize(allTodos, options);
                //File.WriteAllText(filename, jsonContent);
                using (StreamWriter writer = new StreamWriter(filename, false)) // false = overschrijven
                {
                    foreach (ToDoItem s in allTodos)
                    {
                        writer.WriteLine($"{s.Id};{s.Title};{s.Description};{s.DueDate};{s.IsCompleted};{s.CompletedAt}");
                    }
                }

            }
            else
            {
                throw new InvalidOperationException($"Item met id {item.Id} bestaat niet.");
            }

        }
    }
}
