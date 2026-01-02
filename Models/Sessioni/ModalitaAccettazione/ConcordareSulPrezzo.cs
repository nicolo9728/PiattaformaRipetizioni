using System;

namespace RipetizioniApp.Models.Sessioni.ModalitaAccettazione;

public abstract record Proposta(DateTime Momento, MetodoPagamento Metodo);
public record PropostaDocente(DateTime Momento, MetodoPagamento Metodo, UtenteId IdDocente) : Proposta(Momento, Metodo);
public record PropostaStudente(DateTime Momento, MetodoPagamento Metodo, UtenteId IdStudente): Proposta(Momento, Metodo);


public class ConcordareSulPrezzo : IModalitaAccettazione
{
    private List<Proposta> proposte = new List<Proposta>();
    private StatusAccettazione status = new InAttesa();

    public StatusAccettazione getStatus() => status;

    public IEnumerable<Proposta> Proposte => proposte.OrderByDescending((p)=>p.Momento);

    public bool CanStudenteProporre() => this.proposte.Count() == 0 || Proposte.First() is PropostaDocente;
    public bool CanDocenteProporre() => this.proposte.Count() == 0 || Proposte.First() is PropostaStudente;

    public bool CanDocenteAccept() => this.proposte.Count() != 0 && Proposte.First() is PropostaStudente;
    public bool CanStudenteAccept() => this.proposte.Count() != 0 && Proposte.First() is PropostaDocente;

    public void Proponi(Proposta proposta)
    {
        if((proposta is PropostaDocente && !CanDocenteProporre()) || (proposta is PropostaStudente && !CanStudenteProporre()))
            throw new Exception();
        
        proposte.Add(proposta);
    }

    public void ChiudiTrattativa()
        => status = new NonAccettata("La trattativa è stata chiusa");
    
    public void Accetta()
    {
        if(proposte.Count == 0)
            throw new Exception("La trattativa non ha ancora nessuna proposta");
        
        Proposta proposta = Proposte.First();
        status = new Accettata(proposta.Metodo);
    }
    
}
