using System.Collections.Generic;

namespace Assignment2.Models
{
    public class CompletedViewModel
    {
	    public List<TodoViewModel> TodoViewModels { get; set; }

	    public CompletedViewModel(List<TodoViewModel> todoViewModels)
	    {
		    TodoViewModels = todoViewModels;
	    }
    }
}
