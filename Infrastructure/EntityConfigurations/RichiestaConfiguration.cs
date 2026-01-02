using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RipetizioniApp.Infrastructure.EntityConfigurations.Converters;
using RipetizioniApp.Models;
using RipetizioniApp.Models.Sessioni;
using RipetizioniApp.Models.Sessioni.ModalitaAccettazione;

namespace RipetizioniApp.Infrastructure.EntityConfigurations;

public class RichiestaConfiguration : IEntityTypeConfiguration<Richiesta>
{
    public void Configure(EntityTypeBuilder<Richiesta> builder)
    {
        builder.HasKey((r)=>r.Id);

        builder.Property((r)=>r.Id)
            .HasConversion(new UtenteIdConverter());

        builder.Property((r)=>r.IdDocente)
            .HasConversion(new UtenteIdConverter());
        
        builder.Property((r)=>r.IdStudente)
            .HasConversion(new UtenteIdConverter());

        builder.Ignore((r)=>r.Modalita);

        builder
            .ComplexProperty<ModalitaAccettazioneRappresentation>("ModalitaRappresentation", (builder) =>
            {
                builder.Property((m)=>m.Modalita)
                    .HasColumnName("Modalita")
                    .HasConversion((m)=>m.Name, (m)=>ModalitaAccettazioneRappresentation.GetTypeFromString(m));
                
                builder.ComplexProperty((m)=>m.Status);
            })
            .UsePropertyAccessMode(PropertyAccessMode.PreferProperty);
        
        builder.HasOne<Utente>()
            .WithMany()
            .HasForeignKey((r)=>r.IdDocente);
        
        builder.HasOne<Utente>()
            .WithMany()
            .HasForeignKey((r)=>r.IdStudente);
    }
}
