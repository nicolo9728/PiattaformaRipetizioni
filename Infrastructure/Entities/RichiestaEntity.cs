using System;
using RipetizioniApp.Models;
using RipetizioniApp.Models.Sessioni;
using RipetizioniApp.Models.Sessioni.ModalitaAccettazione;

namespace RipetizioniApp.Infrastructure.Entities;

public record RichiestaEntity(RichiestaId Id, ModalitaAccettazioneRappresentation Modalita, UtenteId IdDocente, UtenteId IdStudente, List<PropostaEntity> Proposte)
{
    public RichiestaEntity() : this(null!) { }
};

public record PropostaEntity(DateTime Momento, MetodoPagamentoRappresentation MetodoPagamento, UtenteId? IdDocente, UtenteId? IdStudente)
{
    public PropostaEntity() : this(null!) { }
};


public static class RichiestaEntityConversions
{
    extension(RichiestaEntity richiestaEntity)
    {
        public Richiesta ToDomainModel()
        {
            IModalitaAccettazione modalita = richiestaEntity.Modalita.ToModel();
            Richiesta richiesta = new Richiesta(modalita, richiestaEntity.IdDocente, richiestaEntity.IdStudente);

            if (modalita is ConcordareSulPrezzo concordare)
                typeof(ConcordareSulPrezzo)
                    .GetField("proposte", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                    .SetValue(concordare, richiestaEntity.Proposte.Select((p) => p.ToDomainModel()).ToList());

            return richiesta;
        }
    }

    extension(Richiesta richiesta)
    {
        public RichiestaEntity ToEntity()
        {
            List<Proposta> proposte = [];
            if (richiesta.Modalita is ConcordareSulPrezzo concordare)
                proposte = (List<Proposta>)typeof(ConcordareSulPrezzo)
                            .GetField("proposte", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                            .GetValue(concordare)!;

            return new RichiestaEntity(richiesta.Id, richiesta.Modalita.ToRappresentation(), richiesta.IdDocente, richiesta.IdStudente, [.. proposte.Select((p) => p.ToEntity())]);
        }
    }

    extension(PropostaEntity propostaEntity)
    {
        public Proposta ToDomainModel()
        {
            if (propostaEntity.IdDocente != null)
                return new PropostaDocente(propostaEntity.Momento, propostaEntity.MetodoPagamento.ToModel(), propostaEntity.IdDocente);

            if (propostaEntity.IdStudente != null)
                return new PropostaStudente(propostaEntity.Momento, propostaEntity.MetodoPagamento.ToModel(), propostaEntity.IdStudente!);

            throw new Exception("Errore di conversione");
        }
    }

    extension(Proposta proposta)
    {
        public PropostaEntity ToEntity() => new PropostaEntity(
                                    proposta.Momento,
                                    proposta.Metodo.toRappresentation(),
                                    proposta is PropostaDocente doc ? doc.IdDocente : null,
                                    proposta is PropostaStudente stu ? stu.IdStudente : null
                                );
    }
}
