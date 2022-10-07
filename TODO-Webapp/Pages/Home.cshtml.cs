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
    public class HomeModel : PageModel
    {
        [BindProperty, MaxLength(25, ErrorMessage = "Max 25 chars"), Required]
        public string Description { get; set; }
        [BindProperty]
        public Priority Priority { get; set; }
        [BindProperty, Required]
        public string Guid { get; set; }
        [BindProperty]
        public bool Completed { get; set; }
        [BindProperty]
        public string? Participants { get; set; }

        public List<ToDo> ToDos { get; set; }

        private readonly IRepo _repo;

        public HomeModel(IRepo repo)
        {
            _repo = repo;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoggedIn") != "true")
                return RedirectToPage("/LogIn");
            else
            {
                int id = (int)HttpContext.Session.GetInt32("UserID");
                _repo.LoadList(id);
                ToDos = _repo.GetLoadedToDos();
            }
            return Page();
        }

        public IActionResult OnPostNew()
        {
            if (ModelState.IsValid)
            {
                _repo.CreateToDo(Description, Priority, (int)HttpContext.Session.GetInt32("UserID"), Participants);
            }
            return RedirectToPage();
        }
        public IActionResult OnPostEdit()
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateToDo(Guid, Description, Priority, Participants);
                return RedirectToPage();
            }
            return Page();
        }
        public IActionResult OnPostComplete()
        {
            _repo.CompleteToDo(Guid);
            return RedirectToPage();
        }
        public IActionResult OnPostDelete()
        {
            _repo.DeleteToDo(Guid);
            _repo.ClearList();
            return RedirectToPage();
        }

        public IActionResult OnPostDeleteCompleted()
        {
            _repo.DeleteCompleted();
            return RedirectToPage();
        }
    }
}