using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RipetizioniApp.Pages.StudenteHome
{
    [Authorize(Roles = "Studente")]
    public class profiloModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
