using System;
using System.Collections.Generic;

namespace Assignment1.Models
{
	public class TodoItem
	{
		public Guid Id { get; set; }
		public string Text { get; set; }

		public bool IsCompleted
		{
			get
			{
				return DateCompleted.HasValue;
			}
			set
			{
			}
		} 
		public DateTime? DateCompleted { get; set; }
		public DateTime DateCreated { get; set; }

		public TodoItem(string text) {
			Id = Guid.NewGuid();
			DateCreated = DateTime.UtcNow;
			Text = text;
		}

		public bool MarkAsCompleted() 
		{
			if (!IsCompleted) 
			{
				DateCompleted = DateTime.Now;
				return true;
			}
			return false;
		}

		public bool RemoveFromCompleted()
		{
			if (IsCompleted)
			{
				DateCompleted = null;
				return true;
			}
			return false;
		}


		public override bool Equals(object o)
		{
			return !ReferenceEquals(o, null) && Equals((TodoItem)o);
		}
		public bool Equals(TodoItem todoItem)
		{
			return !ReferenceEquals(todoItem, null) && this.Id.Equals(todoItem.Id);
		}
		public override int GetHashCode() {
			return Id.GetHashCode();
		}
		
		
		public Guid UserId { get; set; }
		public List<TodoItemLabel> Labels { get; set; }
		public DateTime? DateDue { get; set; }
		public TodoItem(string text, Guid userId)
		{
			Id = Guid.NewGuid();
			Text = text;
			DateCreated = DateTime.UtcNow;
			UserId = userId;
			Labels = new List<TodoItemLabel>();
		}
		public TodoItem()
		{
		}
	}
}
