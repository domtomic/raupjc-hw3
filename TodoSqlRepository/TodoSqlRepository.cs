using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoRepository
{
    public class TodoSqlRepository : ITodoRepository
    {

        private readonly TodoDbContext _context;

        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }

        public void Add(TodoItem todoItem)
        {
            if (_context.TodoItem.Contains(todoItem))
            {
                throw new DuplicateTodoItemException(todoItem.Id);
            }
            _context.TodoItem.Add(todoItem);
            _context.SaveChanges();
        }

        public TodoItem Get(Guid todoId, Guid userId)
        {
            TodoItem todoItem = _context.TodoItem.FirstOrDefault(item => item.Id.Equals(todoId));
            if (todoItem != null && !todoItem.UserId.Equals(userId))
            {
                throw new TodoAccessDeniedException(todoId, userId);
            }
            return todoItem;
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            return _context.TodoItem.Where(item => item.UserId.Equals(userId)).Where(item => item.IsCompleted.Equals(false)).ToList();
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            return _context.TodoItem.Where(item => item.UserId.Equals(userId)).OrderByDescending(i => i.DateCreated).ToList();
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            return _context.TodoItem.Where(item => item.UserId.Equals(userId)).Where(item => item.IsCompleted.Equals(true)).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            return _context.TodoItem.Where(item => item.UserId.Equals(userId)).Where(filterFunction).ToList();
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            TodoItem todoItem = _context.TodoItem.FirstOrDefault(item => item.Id.Equals(todoId));
            if (todoItem == null) return false;
            if (!todoItem.UserId.Equals(userId))
            {
                throw new TodoAccessDeniedException(todoId, userId);
            }
            todoItem.MarkAsCompleted();
            _context.SaveChanges();
            return true;
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            TodoItem todoItem = _context.TodoItem.FirstOrDefault(item => item.Id.Equals(todoId));
            if (todoItem == null) return false;
            if (!todoItem.UserId.Equals(userId))
            {
                throw new TodoAccessDeniedException(todoId, userId);
            }
            _context.TodoItem.Remove(todoItem);
            _context.SaveChanges();
            return true;
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            if (!todoItem.UserId.Equals(userId))
            {
                throw new TodoAccessDeniedException(todoItem.Id, userId);
            }
            TodoItem varItem = _context.TodoItem.FirstOrDefault(item => item.Id.Equals(todoItem.Id));
            if (varItem == null)
            {
                _context.TodoItem.Add(todoItem);
            }
            else
            {
                _context.Entry(varItem).CurrentValues.SetValues(todoItem);
            }
            _context.SaveChanges();
        }

        public class DuplicateTodoItemException : Exception
        {
            public DuplicateTodoItemException(Guid todoItemId) : base("duplicate id: " + todoItemId) { }
        }

        public class TodoAccessDeniedException : Exception
        {
            public TodoAccessDeniedException(Guid todoItemId, Guid todoUserId) : base("User with user id: " + todoUserId +
                "has no access privilage for item id: " + todoItemId) { }
        }
    }
}
