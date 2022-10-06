using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TODO_Webapp.Model;
using TODO_Webapp.Service.Interface;

namespace TODO_Webapp.Pages
{
    public class SignUpModel : PageModel
    {
        [BindProperty, Required]
        public string FirstName { get; set; }
        [BindProperty, Required]
        public string LastName { get; set; }
        [BindProperty, Required]
        public string Username { get; set; }
        [BindProperty, Required]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again!")]
        public string ConfirmPassword { get; set; }
        public string Error { get; set; }


        private readonly IRepo _repo;
        public SignUpModel(IRepo repo)
        {
            _repo = repo;
        }

        public void OnGet(string e)
        {
            Error = e;
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.SignUp(FirstName, LastName, Username, Password);
                    return RedirectToPage("/Login");
                }
                catch (UserAlreadyExistsException e)
                {
                    return RedirectToPage("SignUp", new {e = e.Message});
                }
            }
            return Page();
        }
    }
}
