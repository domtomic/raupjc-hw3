using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoRepository;

namespace TodoApp.Models
{
    public class TodoModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDue { get; set; }
        public Guid UserId { get; set; }
        public bool IsCompleted => DateCompleted.HasValue;
        public List<TodoItemLabel> Labels { get; set; }

        public bool MarkAsCompleted()
        {
            if (!IsCompleted)
            {
                DateCompleted = DateTime.Now;
                return true;
            }
            return false;
        }

        public string RemainingTime()
        {
            if (DateDue.Equals(null)) return "";
            if (DateDue < DateTime.Now) return "Rok je prošao!";
            return (DateDue - DateTime.Now).ToString();
        }

        public TodoModel(string text, Guid userId)
        {
            Id = Guid.NewGuid();
            Text = text;
            DateCreated = DateTime.UtcNow;
            UserId = userId;
            Labels = new List<TodoItemLabel>();
        }
    }
}
