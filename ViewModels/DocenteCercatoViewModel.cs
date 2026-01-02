using System;
using RipetizioniApp.Models;

namespace RipetizioniApp.ViewModels;

public record DocenteCercatoViewModel(Guid Id, string Username, string Nome, string Cognome, CriterioPagamento Criterio);
