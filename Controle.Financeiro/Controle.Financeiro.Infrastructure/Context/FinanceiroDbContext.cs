using Controle.Financeiro.Domain.PlanoContas;
using Controle.Financeiro.IntegrationTest.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle.Financeiro.Infrastructure.Context
{
    public class FinanceiroDbContext:DbContext
    {
        public FinanceiroDbContext(DbContextOptions<FinanceiroDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ContaMap());
        }

        public DbSet<Conta> Contas { get; set; }
    }
}
