using Controle.Financeiro.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle.Financeiro.IntegrationTest.Context
{
    public class FinanceiroDBContextTest
    {
        [Fact]
        public void Deve_Criar_Database_E_Tabela_Se_Nao_Existir()
        {
            var options = new DbContextOptionsBuilder<FinanceiroDbContext>()
                .UseSqlServer("Server=DESKTOP-V7712Q7\\SQLEXPRESS;Database=financeiro;User Id=admin;Password=@admin;TrustServerCertificate=True;")
                .Options;

            using var context = new FinanceiroDbContext(options);
            // Cria o banco de dados e as tabelas se não existirem
            context.Database.EnsureCreated();

            bool canConnect = context.Database.CanConnect();
            Assert.True(canConnect, "Não foi possível conectar ou criar o banco de dados FinanceiroTestDb.");
        }
    }
}
