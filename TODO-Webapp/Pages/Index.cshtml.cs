using Microsoft.AspNetCore.Mvc.RazorPages;
using TODO_Webapp.Service.Interface;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TODO_Webapp.Model;

namespace TODO_Webapp.Pages
{
    public class IndexModel : PageModel
    {
        public List<ToDo> ToDos { get; set; }
        private readonly IRepo _repo;

        public IndexModel(IRepo repo)
        {
            _repo = repo;
        }

        public void OnGet()
        {
            _repo.CreateToDo("Køb ind", DateTime.Today.Date);
            ToDos = _repo.GetAllToDos();
        }
    }
}