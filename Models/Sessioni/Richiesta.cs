using System;
using RipetizioniApp.Models.Sessioni.ModalitaAccettazione;

namespace RipetizioniApp.Models.Sessioni;

public record RichiestaId(Guid Id);

public class Richiesta
{
    public RichiestaId Id { get; }
    public IModalitaAccettazione Modalita { get; private set;}
    private ModalitaAccettazioneRappresentation ModalitaRappresentation { get => Modalita.ToRappresentation(); set => Modalita = value.ToModel();}

    public UtenteId IdDocente { get; }
    public UtenteId IdStudente { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Richiesta(){}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public Richiesta(IModalitaAccettazione modalita, UtenteId idDocente, UtenteId idStudente)
        => (Id, Modalita, IdDocente, IdStudente) = (new RichiestaId(Guid.NewGuid()), modalita, idDocente, idStudente);
}
