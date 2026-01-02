using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RipetizioniApp.Helpers;
using RipetizioniApp.Infrastructure;
using RipetizioniApp.Models;
using RipetizioniApp.ViewModels;

namespace RipetizioniApp.Pages.DocenteHome
{
    public record MateriaForm(string Nome);

    [Authorize(Roles = "Docente")]
    public class AggiungiMateriaModel(RipetizioniDbContext db) : PageModel
    {
        public string? Errore { get; private set; }
        public void OnGet(string errore)
        {
            Errore = errore;
        }

        public async Task<IActionResult> OnPost(MateriaForm form)
        {
            try
            {
                Materia materia = Materia.FromString(form.Nome);
                UtenteId id = User.Id;

                Docente docente = db.Docenti.Where((d) => d.Id == id).First();

                docente.Materie.Aggiungi(materia);

                await db.SaveChangesAsync();

                return Redirect(User.ProfiloURL);
            }
            catch (Exception err)
            {
                return Redirect($"/docenteHome/aggiungiMateria?errore={err.Message}");
            }
        }
    }
}
