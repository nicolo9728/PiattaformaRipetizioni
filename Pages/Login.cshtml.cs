using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RipetizioniApp.Infrastructure;
using RipetizioniApp.Models;

namespace RipetizioniApp.Pages
{

    public record LoginForm([Required] string Username, [Required] string Password);

    public class LoginModel(RipetizioniDbContext db) : PageModel
    {
        public string? Errore { get; private set; }

        public void OnGet(string errore)
        {
            Errore = errore;
        }

        public async Task<IActionResult> OnPost(LoginForm form)
        {
            if(!ModelState.IsValid)
                return Page();

            Utente? utente = await db.Utenti
                .Where((d) => d.Credenziali.Username == form.Username)
                .FirstOrDefaultAsync();

            if (utente == null)
                return Redirect("/login?errore=Username o password non validi");

            if (!utente.Credenziali.CheckPassword(form.Password))
                return Redirect("/login?errore=Username o password non validi");

            var identity = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, utente.Id.Id.ToString()),
                new Claim(ClaimTypes.Name, utente.Credenziali.Username),
                new Claim(ClaimTypes.Role, utente.GetType().Name)
            ], CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return Redirect("/");
        }
    }
}
