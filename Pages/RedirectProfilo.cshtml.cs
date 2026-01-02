using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RipetizioniApp.Helpers;

namespace RipetizioniApp.Pages
{
    [Authorize]
    public class RedirectProfiloModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (User.Ruolo == "Docente")
                return Redirect("/docenteHome/profilo");

            if (User.Ruolo == "Studente")
                return Redirect("/studenteHome/profilo");


            return Page();
        }
    }
}
