using Microsoft.AspNetCore.Mvc.RazorPages;
using TODO_Webapp.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace TODO_Webapp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRepo _repo;

        public IndexModel(IRepo repo)
        {
            _repo = repo;
        }

        public void OnGet()
        {
        }
    }
}