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
            HttpContext.Session.SetString("LoggedIn", "false");
            HttpContext.Session.Remove("UserID");
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int ID = _repo.LogIn(Username, Password);
                    if (ID != 0)
                    {
                        HttpContext.Session.SetInt32("UserID", ID);
                        HttpContext.Session.SetString("LoggedIn", "true");
                        return RedirectToPage("/Home");
                    }
                }
                catch (Exception)
                {

                }
            }
            return Page();
        }
    }
}
