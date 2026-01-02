using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RipetizioniApp.Helpers;
using RipetizioniApp.Infrastructure;
using RipetizioniApp.Models;
using RipetizioniApp.ViewModels;

namespace RipetizioniApp.Pages.DocenteHome
{

    public record ConfiguraSalarioForm(string Tipo, int? TariffaEuro, int? TariffaCentesimi)
    {
        private Moneta? Tariffa => 
            TariffaEuro != null && TariffaCentesimi != null 
            ? Moneta.FromInts(TariffaEuro.Value, TariffaCentesimi.Value) 
            : null;
        public CriterioRappresentation CriterioRappresentation => 
            new CriterioRappresentation(CriterioRappresentation.GetTypeFromString(Tipo), Tariffa);
    }
    
    [Authorize(Roles = "Docente")]
    public class ConfiguraSalarioModel(RipetizioniDbContext db) : PageModel
    {
        public string? Tipo = null;
        public void OnGet(string? tipo)
        {
            Tipo = tipo;
        }



        public async Task<ActionResult> OnPost(ConfiguraSalarioForm form)
        {
            CriterioPagamento criterio = form.CriterioRappresentation.ToDomainModel();
            UtenteId id = User.Id;
            Docente docente = await db.Docenti.Where((d)=>d.Id == id).FirstAsync();

            docente.ConfiguraCriterioPagamento(criterio);

            await db.SaveChangesAsync();

            return Redirect(User.ProfiloURL);
        }
    }
}
