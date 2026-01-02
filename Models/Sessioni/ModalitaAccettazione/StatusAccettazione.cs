using System;

namespace RipetizioniApp.Models.Sessioni.ModalitaAccettazione;

public abstract record StatusAccettazione;

public record Accettata(MetodoPagamento MetodoAccettato) : StatusAccettazione;
public record NonAccettata(string Causa) : StatusAccettazione;
public record InAttesa : StatusAccettazione;

public static class DiscriminatedUnion
{
    extension(StatusAccettazione status)
    {
        public T Map<T>(
            Func<Accettata, T> f1,
            Func<NonAccettata, T> f2,
            Func<InAttesa, T> f3
        ) => status switch
        {
            Accettata a => f1(a),
            NonAccettata n => f2(n),
            InAttesa i => f3(i),
            _ => throw new Exception("Errore tipo non sopportato")
        };
    }
}