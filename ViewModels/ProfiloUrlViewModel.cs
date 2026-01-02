using System;
using System.Security.Claims;
using RipetizioniApp.Helpers;
using RipetizioniApp.Models;

namespace RipetizioniApp.ViewModels;

public static class ProfiloUrlViewModel
{
    extension(ClaimsPrincipal utente)
    {
        public string ProfiloURL => 
            utente.Ruolo switch
            {
                "Studente" => $"/studenteHome/{utente.Id.Id}",
                "Docente" => $"/profiloDocente/{utente.Id.Id}",
                _ => throw new Exception()
            };
    }
}
