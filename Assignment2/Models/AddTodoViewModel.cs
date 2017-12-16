using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    public class AddTodoViewModel
    {
	    public string Labels { get; set; }
	    public DateTime DateDue { get; set; }

	    [Required]
		public String Text { get; set; }
		

    }
}
