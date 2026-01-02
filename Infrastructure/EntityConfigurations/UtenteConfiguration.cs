using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RipetizioniApp.Models;

namespace RipetizioniApp.Infrastructure.EntityConfigurations;

public class UtenteConfiguration : IEntityTypeConfiguration<Utente>
{
    public void Configure(EntityTypeBuilder<Utente> builder)
    {
        builder.HasKey((s)=>s.Id);
        builder.Property(s => s.Id)
            .HasConversion((_)=>_.Id, (_)=>new UtenteId(_));

        builder.HasDiscriminator<string>("Tipo")
            .HasValue<Studente>("Studente")
            .HasValue<Docente>("Docente");
        
        builder.ComplexProperty((d)=>d.Credenziali, (builder) =>
        {
            builder.Property((c)=>c.Username).HasColumnName("Username");
            builder.Property((c)=>c.Password).HasColumnName("Password");
        });

        builder.ComplexProperty((d)=>d.Generalita, (builder) =>
        {
            builder.Property((g)=>g.Nome).HasColumnName("Nome");
            builder.Property((g)=>g.Cognome).HasColumnName("Cognome");
            builder.Property((g)=>g.DataNascita).HasColumnName("Data_Nascita");
        });

        builder.ToTable("Utente");
    }
}
