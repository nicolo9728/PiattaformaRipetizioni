using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace RipetizioniApp.Models;

public class ListaMaterie : IEnumerable<Materia>
{
    private List<Materia> _materie = new List<Materia>();
    public List<Materia> Lista => _materie.OrderBy((m)=>m.Nome).ToList();
    
    public void Aggiungi(Materia materia)
    {
        if(_materie.Contains(materia)) return;
        
        _materie.Add(materia);
    }


    public void RimuoviMateriaByNome(string nome)
    {
        Materia? materia = _materie.Find((m)=>m.Nome == nome);
        if(materia != null)
            _materie.Remove(materia);
    }

    public IEnumerator<Materia> GetEnumerator()
    {
        return Lista.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
