using System;

namespace RipetizioniApp.Models;


public class Studente : Utente
{
    public Studente(Generalita generalita, Credenziali credenziali) : base(generalita, credenziali)  {}
    private Studente(){}
}
