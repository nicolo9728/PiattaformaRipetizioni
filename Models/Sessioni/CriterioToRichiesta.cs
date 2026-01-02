using System;
using RipetizioniApp.Models.Sessioni.ModalitaAccettazione;

namespace RipetizioniApp.Models.Sessioni;

public static class CriterioToRichiesta
{
    extension(CriterioPagamento criterio)
    {
        public Richiesta CreaRichiesta(UtenteId idDocente, UtenteId idStudente)
            => criterio.Map(
                (c)=> throw new Exception("Il criterio deve essere impostato"),
                (c)=> new Richiesta(new ConcordareSulPrezzo(), idDocente, idStudente),
                (c) => new Richiesta(new DocenteDeveAccettare(new PagamentoMensile(c.TariffaMensile)), idDocente, idStudente),
                (c) => new Richiesta(new DocenteDeveAccettare(new PagamentoOrario(c.TariffaOraria)), idDocente, idStudente)
            );
    }
}
