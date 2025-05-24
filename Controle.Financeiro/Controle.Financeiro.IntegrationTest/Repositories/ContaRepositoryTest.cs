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
                .UseSqlServer("Server=DESKTOP-V7712Q7\\SQLEXPRESS;Database=financeiro;User Id=admin;Password=@admin;TrustServerCertificate=True;")
                .Options;

           
            _dbContext = new FinanceiroDbContext(options);
            _contaRepository = new ContaRepository(_dbContext);
        }

        [Fact]
        public async Task IncluirContaNivel1()
        {
            var conta = new Conta(1, "Receitas", TipoConta.Receita, false);
            var result = await _contaRepository.Insert(conta);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task IncluirContaNivel2()
        {
            var contaMaster = await _contaRepository.GetByCodigoExtenso("1");
            var conta = new Conta(3, "Multas", TipoConta.Receita, true);
            conta.AddContarMaster(contaMaster);
            var result = await _contaRepository.Insert(conta);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ObterContaById()
        {
            var conta = new Conta(1, "Receitas", TipoConta.Receita, false);
            await _contaRepository.Insert(conta);
            var result = await _contaRepository.Get(conta.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ObterContaByCodigoExtenso()
        {
            
            var result = await _contaRepository.GetByCodigoExtenso("1");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AlterarConta()
        {
            var conta = new Conta(1, "Receitas", TipoConta.Receita, false);
            await _contaRepository.Insert(conta);
            conta.Descricao = "Ativo";
            var result = await _contaRepository.Update(conta);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ExcluirConta()
        {
            var conta = new Conta(1, "Receitas", TipoConta.Receita, false);
            await _contaRepository.Insert(conta);
            var result = await _contaRepository.Delete(conta);
            result = await _contaRepository.Get(result.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task ObterCodigoMaxGrupoContaMaiorQueZero() 
        {
            var grupoConta = await _contaRepository.GetByCodigoExtenso("1");
            var codigoMax = await _contaRepository.GetCodigoMaxGrupoConta(grupoConta);
            Assert.True(codigoMax == 3);
        }

        [Fact]
        public async Task ObterCodigoMaxGrupoContaIgualAZero()
        {
            var grupoConta = await _contaRepository.GetByCodigoExtenso("1.3");
            var codigoMax = await _contaRepository.GetCodigoMaxGrupoConta(grupoConta);
            Assert.True(codigoMax == 0);
        }

        [Fact]
        public async Task ObterTodasContas()
        {
            var contas = await _contaRepository.GetAll();
            Assert.NotEmpty(contas);
        }
    }
}
