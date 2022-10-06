using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TODO_Webapp.Service.Interface;

namespace TODO_Webapp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty, Required]
        public string Username { get; set; }
        [BindProperty, Required]
        public string Password { get; set; }


        private readonly IRepo _repo;
        public LoginModel(IRepo repo)
        {
            _repo = repo;
        }


        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if(_repo.LogIn(Username, Password));
                return RedirectToPage("/Home");
            }
            return Page();
        }
    }
}
