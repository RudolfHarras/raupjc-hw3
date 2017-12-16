using System;
using System.Collections.Generic;

namespace Assignment1.Models
{
	public class TodoItemLabel
	{
		public Guid Id { get; set; }
		public string Value { get; set; }

		public List<TodoItem> LabelTodoItems { get; set; }

		public TodoItemLabel(string value)
		{
			Id = Guid.NewGuid();
			Value = value;
			LabelTodoItems = new List<TodoItem>();
		}

		public TodoItemLabel()
		{
		}
	}
}