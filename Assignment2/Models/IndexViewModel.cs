using System;
using System.Collections.Generic;
using System.Globalization;

namespace Assignment2.Models
{
    public class IndexViewModel
    {
		public List<TodoViewModel> TodoViewModels { get; set; }

	    public IndexViewModel(List<TodoViewModel> todoViewModels)
	    {
		    TodoViewModels = todoViewModels;
	    }
	}
}
