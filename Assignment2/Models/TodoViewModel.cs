using System;
using System.Collections.Generic;
using System.Globalization;
using Assignment1.Models;

namespace Assignment2.Models
{
    public class TodoViewModel
    {
	    public Guid Id { get; set; }
	    public Guid UserId { get; set; }
		public String Text { get; set; }

		public DateTime DateDue { get; set; }
	    public DateTime? DateCompleted { get; set; }
	    public DateTime DateCreated { get; set; }
	    public bool IsCompleted => DateCompleted.HasValue;

	    public List<TodoItemLabel> Labels { get; set; }

	    public bool MarkAsCompleted()
	    {
		    if (IsCompleted) return false;
		    DateCompleted = DateTime.Now;
		    return true;
	    }

	    public string TimeLeft()
	    {
		    if (DateDue.Equals(null)) return ""; 
		    if (DateDue < DateTime.Now)
		    {
			    return "Deadline passed!";
		    }

		    string retDays = Math.Floor((DateDue - DateTime.Now).TotalDays).ToString(CultureInfo.CurrentCulture);
		    string retHours = (DateDue - DateTime.Now).Hours.ToString();
		    string retMinutes = (DateDue - DateTime.Now).Minutes.ToString();
		    string stringDays = retDays == "1" ? " day, " : " days, ";
		    string stringHours = retHours == "1" ? " hour, " : " hours, ";
		    string stringMinutes = retMinutes == "1" ? " minute left" : " minutes left";

			return retDays + stringDays  + retHours + stringHours + retMinutes + stringMinutes;
	    }

	    public TodoViewModel(string text, Guid userId)
	    {
		    Id = Guid.NewGuid();
		    Text = text;
		    DateCreated = DateTime.UtcNow;
		    UserId = userId;
		    Labels = new List<TodoItemLabel>();
	    }

	    public TodoViewModel(string text)
	    {
		    Id = Guid.NewGuid();
		    DateCreated = DateTime.UtcNow;
		    Text = text;
	    }

	    public DateTime? GetDateCompleted()
	    {
		    return DateCompleted;
	    }

	    public string GetTimeLeft()
	    {
		    return TimeLeft();
	    }

	    public DateTime GetDateDue()
	    {
		    return DateDue;
	    }
	}
}
