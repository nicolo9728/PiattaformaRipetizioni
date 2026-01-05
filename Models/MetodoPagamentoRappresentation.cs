using System;

namespace RipetizioniApp.Models;

public record MetodoPagamentoRappresentation(Type MetodoPagamento, Moneta Tariffa)
{
    public MetodoPagamentoRappresentation():this(null, null){}
};


public static class Converter
{
    extension(MetodoPagamentoRappresentation rappresentation)
    {
        public MetodoPagamento ToModel()
            => (MetodoPagamento)Activator.CreateInstance(rappresentation.MetodoPagamento, rappresentation.Tariffa)!;

        public static Type GetTypeFromString(string nome)
            => new Type[]
            {
                typeof(PagamentoMensile),
                typeof(PagamentoOrario)
            }.First((t)=>t.Name == nome);
    }

    extension(MetodoPagamento metodo)
    {
        public MetodoPagamentoRappresentation toRappresentation()
            => metodo.Map(
                (m) => new MetodoPagamentoRappresentation(m.GetType(), m.TariffaMensile),
                (o) => new MetodoPagamentoRappresentation(o.GetType(), o.TariffaOraria));
    }
}