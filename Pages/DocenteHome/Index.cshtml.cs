using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RipetizioniApp.Pages.DocenteHome
{
    [Authorize(Roles = "Docente")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
