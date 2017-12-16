using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment1.Interfaces;
using Assignment1.Models;
using Assignment2.Data;
using Assignment2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Assignment2.Controllers
{
	[Authorize]
    public class TodoController : Controller
    {
		private readonly ITodoRepository _repository;
	    private readonly UserManager<ApplicationUser> _userManager;
	    public TodoController(UserManager<ApplicationUser> userManager = null, ITodoRepository repository = null)
	    {
		    _repository = repository;
		    _userManager = userManager;
	    }

	    public async Task<IActionResult> Index()
	    {
		    ApplicationUser applicationUser = await _userManager.GetUserAsync(HttpContext.User);
			List<TodoItem> activeItems =  _repository.GetActive(new Guid (applicationUser.Id));
			List<TodoViewModel> todoViewModels = Mapper.Map<List<TodoItem>, List<TodoViewModel>>(activeItems);
			IndexViewModel indexViewModel = new IndexViewModel(todoViewModels);
			return View(indexViewModel);
	    }

	    public ActionResult Add()
	    {
		    return View();
	    }

	    public async Task<ActionResult> Completed()
	    {
		    ApplicationUser applicationUser = await _userManager.GetUserAsync(HttpContext.User);
		    List<TodoItem> items = _repository.GetCompleted(new Guid(applicationUser.Id));
		    List<TodoViewModel> todoViewModels = Mapper.Map<List<TodoItem>, List<TodoViewModel>>(items);
		    CompletedViewModel completedViewModel = new CompletedViewModel(todoViewModels);
		    return View(completedViewModel);
	    }


		[HttpPost]
	    public async Task<IActionResult> Add(AddTodoViewModel item)
	    {
		    if (ModelState.IsValid)
		    {
			    ApplicationUser applicationUser = await _userManager.GetUserAsync(HttpContext.User);
			    TodoItem todo = new TodoItem(item.Text, new Guid(applicationUser.Id))
			    {
				    DateDue = item.DateDue
			    };
			    if (item.Labels != null)
			    {
				    string[] labels = item.Labels.Split(',');
				    foreach (var l in labels)
				    {
					    TodoItemLabel todoItemLabel = new TodoItemLabel(l.Trim());
					    todoItemLabel = _repository.AddLabel(todoItemLabel);
					    todo.Labels.Add(todoItemLabel);
				    }
					
			    }
			    _repository.Add(todo);
			    return RedirectToAction("Index");
		    }
		    return View();
	    }

		[HttpGet("MarkAsCompleted/{Id}")]
		public async Task<IActionResult> MarkAsCompleted(Guid id)
		{
			ApplicationUser applicationUser = await _userManager.GetUserAsync(HttpContext.User);
			_repository.MarkAsCompleted(id, new Guid(applicationUser.Id));
			return RedirectToAction("Index");
		}

		[HttpGet("RemoveFromCompleted/{Id}")]
	    public async Task<IActionResult> RemoveFromCompleted(Guid id)
		{
			ApplicationUser applicationUser = await _userManager.GetUserAsync(HttpContext.User);
			_repository.RemoveFromCompleted(id, new Guid(applicationUser.Id));
			return RedirectToAction("Completed");
		}


	    [HttpGet("Delete/{Id}")]
		public async Task<IActionResult> Delete(Guid id)
	    {
			ApplicationUser applicationUser = await _userManager.GetUserAsync(HttpContext.User);
		    _repository.Remove(id, new Guid(applicationUser.Id));
		    return RedirectToAction("Completed");
		}
    }
}