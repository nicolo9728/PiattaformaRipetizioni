using System;

namespace RipetizioniApp.Models;

public record Moneta(int ParteIntera, int ParteDecimale);

public static class MonetaHelper
{
    extension(Moneta moneta)
    {
        public static Moneta FromInts(int ParteIntera, int ParteDecimale)
            => ParteDecimale >= 0 && ParteDecimale <= 99
                ? new Moneta(ParteIntera, ParteDecimale)
                : throw new ArgumentException("La parte decimale deve essere da 0 a 99");
        
        public static Moneta FromString(string stringa)
        {
            int[] parti = stringa
                    .Split(".")
                    .Select((s)=>Convert.ToInt32(s))
                    .ToArray();

            if(parti.Length != 2)
                throw new ArgumentException("La stringa deve essere divisa in 2 parti");
            
            return FromInts(parti[0], parti[1]);
        }

        public int ToInt()
            => moneta.ParteIntera * 100 + moneta.ParteDecimale;
        
        public string ToStringRap()
            => $"{moneta.ParteIntera}.{moneta.ParteDecimale:00}€";

        public static Moneta FromInt(int numero)
            => new Moneta(numero / 100, numero % 100);
    }
}
