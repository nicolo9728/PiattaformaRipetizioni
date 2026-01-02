namespace RipetizioniApp.Models;

public abstract record CriterioPagamento;

public record CriterioNonImpostato: CriterioPagamento;
public record CriterioConcordare : CriterioPagamento;
public record CriterioPagamentoMensile(Moneta TariffaMensile) : CriterioPagamento;
public record CriterioPagamentoOgniOra(Moneta TariffaOraria): CriterioPagamento;


public static class CritarioPagamentoDiscriminatedUnion
{
    extension(CriterioPagamento criterio)
    {
        public T Map<T>(
            Func<CriterioNonImpostato, T> f1, 
            Func<CriterioConcordare, T> f2, 
            Func<CriterioPagamentoMensile, T> f3, 
            Func<CriterioPagamentoOgniOra, T> f4) =>
                criterio switch
                {
                   CriterioNonImpostato c => f1(c),
                   CriterioConcordare c => f2(c),
                   CriterioPagamentoMensile c => f3(c),
                   CriterioPagamentoOgniOra c => f4(c),
                   _ => throw new ArgumentException("Errore")
                };

    }
}