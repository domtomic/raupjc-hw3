using System;
using System.Collections.Generic;
using TodoRepository;

namespace TodoApp.Models
{
    public class TodoViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }


        public DateTime? DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsCompleted => DateCompleted.HasValue;

        public List<TodoItemLabel> Labels { get; set; }
        public DateTime DateDue { get; set; }

        public bool MarkAsCompleted()
        {
            if (IsCompleted) return false;
            DateCompleted = DateTime.Now;
            return true;
        }

        public string RemainingTime()
        {
            if (DateDue.Equals(null)) return "";
            if (DateDue < DateTime.Now) return "Rok je prošao!";
            return (DateDue - DateTime.Now).ToString();

        }

        public TodoViewModel(string text, Guid userId)
        {
            Id = Guid.NewGuid();
            Text = text;
            DateCreated = DateTime.UtcNow;
            UserId = userId;
            Labels = new List<TodoItemLabel>();
        }
    }
}
