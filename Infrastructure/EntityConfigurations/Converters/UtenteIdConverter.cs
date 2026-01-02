using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RipetizioniApp.Models;

namespace RipetizioniApp.Infrastructure.EntityConfigurations.Converters;

public class UtenteIdConverter : ValueConverter<UtenteId, Guid>
{
    public UtenteIdConverter() : base((_)=>_.Id, (_)=> new UtenteId(_))
    {
    }
}
