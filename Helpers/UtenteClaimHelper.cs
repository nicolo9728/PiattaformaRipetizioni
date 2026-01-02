using System;
using System.Security.Claims;
using RipetizioniApp.Models;

namespace RipetizioniApp.Helpers;

public static class UtenteClaimHelper
{
    extension(ClaimsPrincipal principal)
    {
        public UtenteId Id => 
            new UtenteId(Guid.Parse(principal.Claims.First((c)=>c.Type == ClaimTypes.NameIdentifier).Value));
        
        public string Ruolo => 
            principal.Claims.First((c)=>c.Type == ClaimTypes.Role).Value;
        
        public string Username => 
            principal.Claims.First((c)=>c.Type == ClaimTypes.Name).Value;
    }
}
