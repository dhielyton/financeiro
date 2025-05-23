using Controle.Financeiro.Domain.PlanoContas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Controle.Financeiro.IntegrationTest.Repositories
{
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
           
            builder.ToTable("Contas");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .IsRequired()
                .HasMaxLength(36);

            builder.Property(c => c.Codigo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Tipo)
                .IsRequired();

            builder.Property(c => c.AceitaLancamento)
                .IsRequired();

            builder.Property(c => c.ContaMasterId)
                .HasMaxLength(36).IsRequired(false);


            builder.HasOne(c => c.ContaMaster)
                .WithMany()
                .HasForeignKey(c => c.ContaMasterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}