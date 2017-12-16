using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Assignment1.Database;
using Assignment1.Interfaces;

namespace Assignment1.Models
{
	public class TodoSqlRepository : ITodoRepository
	{
		private readonly TodoDbContext _context;

		public TodoSqlRepository(TodoDbContext context)
		{
			_context = context;
		}

		public TodoItem Get(Guid todoId, Guid userId)
		{
			TodoItem todoItem = _context.TodoItems.Include(s => s.Labels).FirstOrDefault(l => l.Id.Equals(todoId));
			if (todoItem != null && !todoItem.UserId.Equals(userId)) throw new TodoAccessDeniedException("Access denied!");
			return todoItem;
		}

		public void Add(TodoItem todoItem)
		{
			TodoItem checkerTodoItem = _context.TodoItems.FirstOrDefault(s => s.Id.Equals(todoItem.Id));
			if (checkerTodoItem != null) throw new DuplicateTodoItemException("duplicate id: { " + todoItem.Id + " }");

			_context.TodoItems.Add(todoItem);
			_context.SaveChanges();
		}

		public bool Remove(Guid todoId, Guid userId)
		{
			TodoItem toBeRemovedTodoItem = _context.TodoItems.FirstOrDefault(s => s.Id.Equals(todoId));
			if (toBeRemovedTodoItem == null) return false;
			if (!toBeRemovedTodoItem.UserId.Equals(userId)) throw new TodoAccessDeniedException("Access denied!");

			_context.TodoItems.Remove(toBeRemovedTodoItem);
			_context.SaveChanges();
			return true;
		}

		public void Update(TodoItem todoItem, Guid userId)
		{
			TodoItem toBeUpdatedTodoItem = Get(todoItem.Id, userId);
			if (toBeUpdatedTodoItem == null)
			{
				Add(todoItem);
			}
			else
			{
				_context.Entry(toBeUpdatedTodoItem).State = EntityState.Modified;
			}
			_context.SaveChanges();
		}

		public bool MarkAsCompleted(Guid todoId, Guid userId)
		{
			TodoItem todoItem = _context.TodoItems.SingleOrDefault(s => s.Id == todoId && s.UserId == userId);
			if (todoItem == null) throw new TodoAccessDeniedException("Access denied!");
			if (!todoItem.MarkAsCompleted()) return false;
			_context.SaveChanges();
			return true;
		}

		public bool RemoveFromCompleted(Guid todoId, Guid userId)
		{
			TodoItem todoItem = _context.TodoItems.SingleOrDefault(s => s.Id == todoId && s.UserId == userId);
			if (todoItem == null) throw new TodoAccessDeniedException("Access denied!");
			if (!todoItem.RemoveFromCompleted()) return false;
			_context.SaveChanges();
			return true;
		}

		public List<TodoItem> GetAll(Guid userId)
		{
			return _context.TodoItems.Where(s => s.Id.Equals(userId))
				.OrderByDescending(t => t.DateCreated).ToList();
		}

		public List<TodoItem> GetActive(Guid userId)
		{
			return _context.TodoItems.Where(s => s.IsCompleted == false && s.UserId.Equals(userId)).ToList();
		}

		public List<TodoItem> GetCompleted(Guid userId)
		{
			return _context.TodoItems.Where(s => s.IsCompleted && s.UserId.Equals(userId)).ToList();
		}

		public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
		{
			return _context.TodoItems.Where(s => s.UserId.Equals(userId) && filterFunction(s)).ToList();
		}

		public TodoItemLabel AddLabel(TodoItemLabel item)
		{
			TodoItemLabel todoItemLabel = _context.TodoItemLabels.SingleOrDefault(l => l.Value == item.Value);
			if (todoItemLabel != null)
			{
				return todoItemLabel;
			}
			_context.TodoItemLabels.Add(item);
			_context.SaveChanges();
			return item;
		}


		
	}
}
