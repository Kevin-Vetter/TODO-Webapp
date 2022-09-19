using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TODO_Webapp.Service.Interface;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TODO_Webapp.Model;

namespace TODO_Webapp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty, MaxLength(25, ErrorMessage = "Max 25 chars"), Required]
        public string Description { get; set; }
        [BindProperty]
        public Priority Priority { get; set; }
        [BindProperty, Required]
        public string Guid { get; set; }
        [BindProperty]
        public bool Completed { get; set; }
        [BindProperty, Required]
        public DateTime DeadLine { get; set; }

        public List<ToDo> ToDos { get; set; }

        private readonly IRepo _repo;

        public IndexModel(IRepo repo)
        {
            _repo = repo;
            ToDos = _repo.GetAllToDos();
        }

        public void OnGet()
        {
            _repo.CreateToDo("somting", DateTime.Today.Date, Priority.Low);
            _repo.CreateToDo("somting", DateTime.Today.Date, Priority.Normal);
            _repo.CreateToDo("somting", DateTime.Today.Date, Priority.High);
        }

        public IActionResult OnPostNew()
        {
                _repo.CreateToDo(Description, DateTime.Today.Date, Priority);
                return RedirectToPage();
        }
        public void OnPostEdit()
        {
            _repo.UpdateToDo(Guid, Description, DeadLine, Priority, Completed);
        }
        public IActionResult OnPostComplete()
        {
            _repo.CompleteToDo(Guid);
            return RedirectToPage();
        }
        public IActionResult OnPostDelete()
        {
            _repo.DeleteToDo(Guid);
            // HACK: there must be a better way
            return RedirectToPage();
        }
    }
}