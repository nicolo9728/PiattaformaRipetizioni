using System;
using System.Collections.ObjectModel;

namespace RipetizioniApp.Models;

public class Docente : Utente
{
    public ListaMaterie Materie { get; } = new ListaMaterie();
    public CriterioPagamento CriterioPagamento { get; private set; } = new CriterioNonImpostato();
    private CriterioRappresentation? CriterioRappresentation { get => CriterioPagamento.CriterioSqlRappresentation; set => CriterioPagamento = value!.ToDomainModel(); }

    public Docente(Generalita generalita, Credenziali credenziali) : base(generalita, credenziali) { }
    private Docente() { }

    public void ConfiguraCriterioPagamento(CriterioPagamento criterio) =>
        CriterioPagamento = criterio;
}
