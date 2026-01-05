using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RipetizioniApp.Infrastructure.Entities;
using RipetizioniApp.Infrastructure.EntityConfigurations.Converters;
using RipetizioniApp.Models;
using RipetizioniApp.Models.Sessioni;
using RipetizioniApp.Models.Sessioni.ModalitaAccettazione;

namespace RipetizioniApp.Infrastructure.EntityConfigurations;

public class RichiestaConfiguration : IEntityTypeConfiguration<RichiestaEntity>
{
    public void Configure(EntityTypeBuilder<RichiestaEntity> builder)
    {
        builder.ToTable("Richiesta");

        builder.HasKey((r) => r.Id);

        builder.Property((r) => r.Id)
            .HasConversion((_) => _.Id, (_) => new RichiestaId(_))
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Ignore((r) => r.Modalita);

        builder
            .ComplexProperty((p) => p.Modalita, (builder) =>
            {
                builder.Property((m) => m.Modalita)
                    .HasColumnName("Modalita")
                    .HasConversion((m) => m.Name, (m) => ModalitaAccettazioneRappresentation.GetTypeFromString(m));


                builder.Property((m) => m.Status)
                    .HasConversion((_) => _.Name, (_) => ConversioniStatus.GetTypeFromString(_))
                    .HasColumnName("Status");

                builder.ComplexProperty((m) => m.MetodoPagamentoAccettato, builder =>
                {
                    builder.Property((m) => m.MetodoPagamento)
                        .HasConversion((_) => _.Name, (_) => MetodoPagamentoRappresentation.GetTypeFromString(_))
                        .HasColumnName("MetodoPagamentoAccettato");

                    builder.Property((m) => m.Tariffa)
                        .HasConversion(new MonetaConverter())
                        .HasColumnName("TariffaAccettata");
                });

                builder.ComplexProperty((m) => m.MetodoPagamentoProposto, (builder) =>
                {
                    builder.Property((m) => m.MetodoPagamento)
                        .HasConversion((_) => _.Name, (_) => MetodoPagamentoRappresentation.GetTypeFromString(_))
                        .HasColumnName("MetodoPagamentoProposto");

                    builder.Property((m) => m.Tariffa)
                        .HasConversion(new MonetaConverter())
                        .HasColumnName("TariffaProposta");
                });

                builder.Property((c) => c.Causa)
                    .HasColumnName("Causa");


            })
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.OwnsMany((p) => p.Proposte, (builder) =>
        {
            builder.ToTable("Proposta");
            
            builder.Property((p) => p.IdDocente)
                .HasConversion(new NullConverter<UtenteId, Guid>(new UtenteIdConverter()));

            builder.Property((p) => p.IdStudente)
                .HasConversion(new NullConverter<UtenteId, Guid>(new UtenteIdConverter()));

            builder.Property((p) => p.Momento);

            builder.OwnsOne((p) => p.MetodoPagamento, builder =>
            {
                builder.Property((p) => p.MetodoPagamento)
                    .HasConversion((_) => _.Name, (_) => MetodoPagamentoRappresentation.GetTypeFromString(_))
                    .HasColumnName("MetodoPagamentoProposto");

                builder.Property((m) => m.Tariffa)
                    .HasConversion(new MonetaConverter())
                    .HasColumnName("TariffaProposta");
            });
        });

        builder.Property((r) => r.IdDocente)
            .HasConversion(new UtenteIdConverter())
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property((r) => r.IdStudente)
            .HasConversion(new UtenteIdConverter())
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasOne<Utente>()
            .WithMany()
            .HasForeignKey((r) => r.IdDocente);

        builder.HasOne<Utente>()
            .WithMany()
            .HasForeignKey((r) => r.IdStudente);
    }
}
