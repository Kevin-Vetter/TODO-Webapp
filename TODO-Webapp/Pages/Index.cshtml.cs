using Microsoft.AspNetCore.Mvc.RazorPages;
using TODO_Webapp.Service.Interface;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TODO_Webapp.Model;
using System.ComponentModel.DataAnnotations;

namespace TODO_Webapp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty, MaxLength(25, ErrorMessage = "Max 25 chars"), Required]
        public string Description { get; set; }
        [BindProperty]
        public Priority Priority { get; set; }

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

        public void OnPost()
        {
            _repo.CreateToDo(Description, DateTime.Today.Date);

        }
    }
}