using System;

namespace RipetizioniApp.Models;

public record Materia(string Nome);

public static class MateriaBuilders
{
    extension(Materia materia)
    {
        public static Materia FromString(string stringa)
            => !string.IsNullOrWhiteSpace(stringa) 
                ? new Materia(stringa)
                : throw new ArgumentException("Il nome della materia non puo essere vuota");
    }
}