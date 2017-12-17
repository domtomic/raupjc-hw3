using System.Data.Entity;

namespace TodoRepository
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(string cnnstr):base(cnnstr)
        {
        }

        public IDbSet<TodoItem> TodoItem { get; set; }
        public IDbSet<TodoItemLabel> TodoLabel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItem>().HasKey(item => item.Id);
            modelBuilder.Entity<TodoItem>().Property(item => item.Text).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(item => item.DateCreated).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(item => item.DateDue);
            modelBuilder.Entity<TodoItem>().Property(item => item.DateCompleted);
            modelBuilder.Entity<TodoItem>().Property(item => item.UserId).IsRequired();
            modelBuilder.Entity<TodoItem>().HasMany(item => item.Labels).WithMany(label => label.LabelTodoItems);

            modelBuilder.Entity<TodoItemLabel>().HasKey(label => label.Id);
            modelBuilder.Entity<TodoItemLabel>().Property(label => label.Value).IsRequired();
        }
    }
}