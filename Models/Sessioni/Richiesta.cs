using System;
using RipetizioniApp.Models.Sessioni.ModalitaAccettazione;

namespace RipetizioniApp.Models.Sessioni;

public record RichiestaId(Guid Id);

public class Richiesta
{
    public RichiestaId Id { get; }
    public IModalitaAccettazione Modalita { get; private set; }

    public UtenteId IdDocente { get; }
    public UtenteId IdStudente { get; }

    public Richiesta(IModalitaAccettazione modalita, UtenteId idDocente, UtenteId idStudente)
        => (Id, Modalita, IdDocente, IdStudente) = (new RichiestaId(Guid.NewGuid()), modalita, idDocente, idStudente);
}
