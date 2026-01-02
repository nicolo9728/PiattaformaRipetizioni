using System;

namespace RipetizioniApp.Models;

public record UtenteId(Guid Id);

public abstract class Utente
{
    public Generalita Generalita { get; }
    public Credenziali Credenziali { get; }
    public UtenteId Id { get; }

    public Utente(Generalita generalita, Credenziali credenziali)
    {
        Generalita = generalita;
        Credenziali = credenziali;
        Id = new UtenteId(Guid.NewGuid());
    }

#pragma warning disable CS8618 
    protected Utente() { }
#pragma warning restore CS8618
}
