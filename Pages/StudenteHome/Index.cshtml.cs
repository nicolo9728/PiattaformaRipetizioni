using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RipetizioniApp.Infrastructure;
using RipetizioniApp.Models;
using RipetizioniApp.ViewModels;

namespace RipetizioniApp.Pages.StudenteHome
{
    [Authorize(Roles = "Studente")]
    public class IndexModel(RipetizioniDbContext db) : PageModel
    {
        public List<DocenteCercatoViewModel>? DocentiCercati { get; private set; }
        public string? MateriaCercata { get; private set; }

        public async Task OnGet(string? materiaCercata)
        {
            MateriaCercata = materiaCercata;
            if (materiaCercata != null)
                DocentiCercati = await db
                            .Docenti
                            .Where((d) => d.Materie.Lista.Any((materia) => EF.Functions.ILike(materia.Nome, $"%{materiaCercata}%")))
                            .Select(d => new DocenteCercatoViewModel(d.Id.Id, d.Credenziali.Username, d.Generalita.Nome, d.Generalita.Cognome, d.CriterioPagamento))
                            .ToListAsync();
        }
    }
}
