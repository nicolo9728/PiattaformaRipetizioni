namespace RipetizioniApp.Models;

public record CriterioRappresentation(Type CriterioPagamento, Moneta? Tariffa);

public static class CriterioConversion
{
    extension(CriterioPagamento criterio)
    {
        public CriterioRappresentation CriterioSqlRappresentation
            => criterio.Map(
                (_) => new CriterioRappresentation(typeof(CriterioNonImpostato), null),
                (_) => new CriterioRappresentation(typeof(CriterioConcordare), null),
                (mensile) => new CriterioRappresentation(typeof(CriterioPagamentoMensile), mensile.TariffaMensile),
                (orario) => new CriterioRappresentation(typeof(CriterioPagamentoOgniOra), orario.TariffaOraria)
            );


    }

    extension(CriterioRappresentation criterio)
    {
        public static Type GetTypeFromString(string stringa)
            => new Type[] {
                typeof(CriterioNonImpostato),
                typeof(CriterioConcordare),
                typeof(CriterioPagamentoMensile),
                typeof(CriterioPagamentoOgniOra)
            }.First((t) => t.Name == stringa);

        public CriterioPagamento ToDomainModel() =>
            (CriterioPagamento)Activator.CreateInstance(criterio.CriterioPagamento, criterio.Tariffa != null ? [criterio.Tariffa] : [])!;
    }
}