using System;

namespace RipetizioniApp.Models;

public record SessioneId(Guid Id);

public class Sessione
{
    public SessioneId Id { get; }
    public UtenteId Docente { get; }
    public UtenteId Studente { get; }

    public Sessione(UtenteId docente, UtenteId studente)
        => (Id, Docente, Studente) = (new SessioneId(Guid.NewGuid()), docente, studente);
}
