using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TODO_Webapp.Service.Interface;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TODO_Webapp.Model;
using Microsoft.AspNetCore.Http;
using System.Globalization;

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
        public DateTime DeadLine { get; }
        public List<ToDo> ToDos { get; set; }

        private readonly IRepo _repo;

        public IndexModel(IRepo repo)
        {
            _repo = repo;
            ToDos = _repo.GetAllToDos();
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostNew()
        {
            if (ModelState.IsValid)
            {
                _repo.CreateToDo(Description, DateTime.Today.Date, Priority);
            }
            return RedirectToPage();
        }
        public void OnPostEdit()
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateToDo(Guid, Description, DeadLine, Priority, Completed);
            }
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

        public IActionResult OnPostDeleteCompleted()
        {
            _repo.DeleteCompleted();
            return RedirectToPage();
        }
    }
}