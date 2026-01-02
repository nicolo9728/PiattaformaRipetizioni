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
            .HasConversion((_)=>_.Id, (_)=>new RichiestaId(_));

        builder.Property((r)=>r.IdDocente)
            .HasConversion(new UtenteIdConverter());
        
        builder.Property((r)=>r.IdStudente)
            .HasConversion(new UtenteIdConverter());

        builder.Ignore((r)=>r.Modalita);

        builder
            .OwnsOne<ModalitaAccettazioneRappresentation>("ModalitaRappresentation", (builder) =>
            {
                builder.Property((m)=>m.Modalita)
                    .HasColumnName("Modalita")
                    .HasConversion((m)=>m.Name, (m)=>ModalitaAccettazioneRappresentation.GetTypeFromString(m));
                
                
                builder.Property((m)=>m.Status)
                    .HasConversion((_)=>_.Name, (_)=>ModalitaAccettazioneRappresentation.GetTypeFromString(_))
                    .HasColumnName("Status");
                
                builder.OwnsOne((m)=>m.MetodoPagamentoAccettato, builder =>
                {
                    builder.Property((m)=>m.MetodoPagamento)
                        .HasConversion((_)=>_.Name, (_)=>MetodoPagamentoRappresentation.GetTypeFromString(_))
                        .HasColumnName("MetodoPagamentoAccettato");
                    
                    builder.Property((m)=>m.Tariffa)
                        .HasConversion(new MonetaConverter())
                        .HasColumnName("TariffaAccettata");
                });

                builder.OwnsOne((m)=>m.MetodoPagamentoProposto, (builder) =>
                {
                    builder.Property((m)=>m.MetodoPagamento)
                        .HasConversion((_)=>_.Name, (_)=>MetodoPagamentoRappresentation.GetTypeFromString(_))
                        .HasColumnName("MetodoPagamentoProposto");
                    
                    builder.Property((m)=>m.Tariffa)
                        .HasConversion(new MonetaConverter())
                        .HasColumnName("TariffaProposta");
                });
                
                builder.Property((c)=>c.Causa)
                    .HasColumnName("Causa");
                
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
