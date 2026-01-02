using System;

namespace RipetizioniApp.Models;

public abstract record MetodoPagamento();

public record PagamentoMensile(Moneta TariffaMensile) : MetodoPagamento;
public record PagamentoOrario(Moneta TariffaOraria) : MetodoPagamento;


public static class MetodoPagamentoDiscriminatedUnion
{
    extension(MetodoPagamento metodo)
    {
        public T Map<T>(Func<PagamentoMensile, T> f1, Func<PagamentoOrario, T> f2)
            => metodo switch
            {
                PagamentoMensile m => f1(m),
                PagamentoOrario o => f2(o),
                _ => throw new Exception("Tipo non valido")
            };
    }
}

public static class MetodoPagamentoBuilder
{
    extension(CriterioPagamento criterio)
    {
        public MetodoPagamento MetodoPagamento 
            => criterio.Map<MetodoPagamento>(
                c => throw new Exception("Se il criterio non è definito non è possibile originare un metodo di pagamento"),
                c => throw new Exception("Se il criterio è da concordare non è possibile originare un metodo di pagamento"),
                m => new PagamentoMensile(m.TariffaMensile),
                o => new PagamentoOrario(o.TariffaOraria)
            );
        
        public bool CanDefineMetodoPagamento 
            => criterio is CriterioPagamentoMensile || criterio is CriterioPagamentoOgniOra;
    }
}