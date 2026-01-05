using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace RipetizioniApp.Infrastructure.EntityConfigurations.Converters;

public class NullConverter<TModel, TProvider> : ValueConverter<TModel?, TProvider?> 
{ 
    public NullConverter(ValueConverter<TModel, TProvider> innerConverter) : base(
        model => (TProvider)(model == null ? default : innerConverter.ConvertToProvider(model))!, 
        provider => (TModel) (provider == null ? default : innerConverter.ConvertFromProvider(provider))!
    ) { } 
}