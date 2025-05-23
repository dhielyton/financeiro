using Controle.Financeiro.Domain.PlanoContas;
using Controle.Financeiro.Infrastructure.Context;
using Controle.Financeiro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle.Financeiro.IntegrationTest.Repositories
{
    public class ContaRepositoryTest
    {
        private readonly FinanceiroDbContext _dbContext;
        private readonly ContaRepository _contaRepository;
        public ContaRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<FinanceiroDbContext>()
                .UseSqlServer("Server=DESKTOP-V7712Q7\\SQLEXPRESS;Database=financeiro;User Id=admin;Password=x9aold1988;TrustServerCertificate=True;")
                .Options;

           
            _dbContext = new FinanceiroDbContext(options);
            _contaRepository = new ContaRepository(_dbContext);
        }

        [Fact]
        public async Task IncluirConta()
        {
            var conta = new Conta("1", "Receitas", TipoConta.Receita, false);
            var result = await _contaRepository.Insert(conta);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ObterConta()
        {
            var conta = new Conta("1", "Receitas", TipoConta.Receita, false);
            await _contaRepository.Insert(conta);
            var result = await _contaRepository.Get(conta.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AlterarConta()
        {
            var conta = new Conta("1", "Receitas", TipoConta.Receita, false);
            await _contaRepository.Insert(conta);
            conta.Descricao = "Ativo";
            var result = await _contaRepository.Update(conta);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ExcluirConta()
        {
            var conta = new Conta("1", "Receitas", TipoConta.Receita, false);
            await _contaRepository.Insert(conta);
            var result = await _contaRepository.Delete(conta);
            result = await _contaRepository.Get(result.Id);
            Assert.Null(result);
        }
    }
}
