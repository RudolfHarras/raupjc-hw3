using System.Data.Entity;
using Assignment1.Models;

namespace Assignment1.Database
{
	public class TodoDbContext : DbContext
	{
		public TodoDbContext(string cnnstr) : base(cnnstr)
		{
		}

		public IDbSet<TodoItem> TodoItems { get; set; }
		public IDbSet<TodoItemLabel> TodoItemLabels { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<TodoItem>().HasKey(s => s.Id);

			modelBuilder.Entity<TodoItem>().Property(s => s.DateCompleted).IsOptional();
			modelBuilder.Entity<TodoItem>().Property(s => s.DateDue).IsOptional();

			modelBuilder.Entity<TodoItem>().Property(s => s.DateCreated).IsRequired();
			modelBuilder.Entity<TodoItem>().Property(s => s.IsCompleted).IsRequired();
			modelBuilder.Entity<TodoItem>().Property(s => s.Text).IsRequired();
			modelBuilder.Entity<TodoItem>().Property(s => s.UserId).IsRequired();

			modelBuilder.Entity<TodoItemLabel>().HasKey(s => s.Id);
			modelBuilder.Entity<TodoItemLabel>().Property(s => s.Value).IsRequired();

			modelBuilder.Entity<TodoItem>().HasMany(s => s.Labels).WithMany(l => l.LabelTodoItems);


		}
	}
}
