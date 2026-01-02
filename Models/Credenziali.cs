using System;

namespace RipetizioniApp.Models;

public record Credenziali(string Username, string Password);


public static class LoginHelper
{
    extension(Credenziali credenziali)
    {
        public bool CheckPassword(string password)
            => BCrypt.Net.BCrypt.Verify(password, credenziali.Password);
    }
}