using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RipetizioniApp.Infrastructure.EntityConfigurations.Converters;
using RipetizioniApp.Models;

namespace RipetizioniApp.Infrastructure.EntityConfigurations;

public class DocenteConfiguration : IEntityTypeConfiguration<Docente>
{
    public void Configure(EntityTypeBuilder<Docente> builder)
    {
        builder.Ignore((d)=>d.CriterioPagamento);
        builder
            .ComplexProperty<CriterioRappresentation>("CriterioRappresentation", (builder) =>
            {
                builder
                    .Property((c)=>c.Tariffa)
                    .HasConversion<MonetaConverter>()
                    .HasColumnName("Tariffa");
                
                builder.Property((c)=>c.CriterioPagamento)
                    .HasConversion((t)=>t.Name, (s) => CriterioRappresentation.GetTypeFromString(s))
                    .HasColumnName("CriterioPagamento");
            })
            .UsePropertyAccessMode(PropertyAccessMode.Property);
        
        builder.OwnsOne((d)=>d.Materie, (builder) =>
        {
            builder.Ignore((m)=>m.Lista);
            builder.OwnsMany((m)=>m.Lista, (builder) =>
            {
                builder.WithOwner().HasForeignKey("DocenteId");
                builder.Property((m)=>m.Nome).HasColumnName("Nome");
            });

            builder.Navigation((m)=>m.Lista)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasField("_materie");
            
        }).UsePropertyAccessMode(PropertyAccessMode.PreferField);
    }

}
