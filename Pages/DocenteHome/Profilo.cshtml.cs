using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RipetizioniApp.Helpers;
using RipetizioniApp.Infrastructure;
using RipetizioniApp.Models;
using RipetizioniApp.Models.Sessioni;
using RipetizioniApp.Models.Sessioni.ModalitaAccettazione;
using RipetizioniApp.ViewModels;

namespace RipetizioniApp.Pages.DocenteHome
{

    [Authorize]
    public class profiloModel(RipetizioniDbContext db) : PageModel
    {
        public Docente? Docente { get; private set;}

        public async Task OnGet(Guid idDocente)
        {
            Docente = await db.Docenti.Where((d)=>d.Id == new UtenteId(idDocente)).FirstAsync();
        }

        public async Task<IActionResult> OnGetEliminaMateria(string nomeMateria)
        {
            if(User.Ruolo != "Docente")
                return Forbid();

            var id = User.Id;
            Docente docente = await db.Docenti.Where((d)=> d.Id == id).FirstAsync();
            
            docente.Materie.RimuoviMateriaByNome(nomeMateria);

            await db.SaveChangesAsync();

            return Redirect(User.ProfiloURL);
        }

        public async Task<IActionResult> OnPostCreaSessione(Guid idDocente)
        {
            if(User.Ruolo != "Studente")
                return Forbid();
            
            UtenteId id = User.Id;
            CriterioPagamento criterio = await db.Docenti
                                                    .Where((d)=>d.Id == new UtenteId(idDocente))
                                                    .Select((d)=>d.CriterioPagamento)
                                                    .FirstAsync();
            
            Richiesta richiesta = criterio.CreaRichiesta();
            

            return Redirect($"/");
        }
    }
}
