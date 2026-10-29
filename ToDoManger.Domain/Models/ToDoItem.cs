using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ToDoManger.Domain.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Title { get; }
        public string? Description { get; }
        public DateTime DueDate { get; }
        public bool IsCompleted { get; private set; } 
        public DateTime? CompletedAt { get; private set; }

        public ToDoItem(string title, string? description, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("title is verplicht");
            }

            Title = title;
            Description = description;
            DueDate = dueDate;

            IsCompleted = false;
            CompletedAt = null;
            
        }

        public void MarkAsCompleted()
        {
            if(this.IsCompleted)
            {
                return;
            }
            this.IsCompleted = true;
            this.CompletedAt = DateTime.Now;

        }

        [JsonConstructor]
        public ToDoItem(int id, string title, string? description, DateTime dueDate, bool isCompleted, DateTime? completedAt)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            IsCompleted = isCompleted;
            CompletedAt = completedAt;
        }

        public override string ToString()
        {
            string status = IsCompleted ? "[Done]" : "[Open]";
            return $"{status} {Title} (due {DueDate:dd/MM/yyyy})";
        }
    }
}
