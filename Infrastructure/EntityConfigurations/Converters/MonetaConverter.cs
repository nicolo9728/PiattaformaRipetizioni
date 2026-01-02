using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RipetizioniApp.Models;

namespace RipetizioniApp.Infrastructure.EntityConfigurations.Converters;

public class MonetaConverter : ValueConverter<Moneta, int>
{
    public MonetaConverter() : 
        base((m)=>m.ToInt(), (i)=>Moneta.FromInt(i)) {}
}
