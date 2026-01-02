using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RipetizioniApp.Models;

namespace RipetizioniApp.Infrastructure.EntityConfigurations;

public class StudenteConfiguration : IEntityTypeConfiguration<Studente>
{
    public void Configure(EntityTypeBuilder<Studente> builder){}
}
