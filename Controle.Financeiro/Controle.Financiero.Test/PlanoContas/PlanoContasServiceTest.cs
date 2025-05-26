using Controle.Financeiro.Domain.PlanoContas;
using Controle.Financeiro.Infrastructure.Context;
using Controle.Financeiro.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Controle.Financiero.Test.PlanoContas
{
    public class PlanoContasServiceTest
    {
        private readonly IContaRepository _contaRepository;
        private static FinanceiroDbContext _context;
        private readonly ContaService _contaService;
        public PlanoContasServiceTest()
        {


            
            string jsonPath = "PlanoContas/Data/ScenarioSugestaoProximoNumero.json";
            string json = File.ReadAllText(jsonPath);
            List<Conta> contas = JsonSerializer.Deserialize<List<Conta>>(json);

            var options = new DbContextOptionsBuilder<FinanceiroDbContext>()
                        .UseInMemoryDatabase("TestDb")
                        .Options;

            _context = new FinanceiroDbContext(options);
            _contaRepository = new ContaRepository(_context);
            _contaService = new ContaService(_contaRepository);

            _context.Contas.RemoveRange(_context.Contas);
            _context.SaveChanges();

            _context.Contas.AddRange(contas);
            _context.SaveChanges();

        }




        [Fact]
        public async Task SugerirProximoCodigoContaComSucesso()
        {
            var codigoConta = await _contaService.ProximoCodigo("b4b4b4b4-4444-4444-4444-444444444444");
            codigoConta.Should().Be("9.10");
        }

        [Fact]
        public async Task CadastrarContaNivel1ComSucesso()
        {

            var result = await _contaService.Cadastrar(1, "Receitas", TipoConta.Receita, false);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task CadastrarContaNivel2ComSucesso()
        {
            var contaMaster = await _contaRepository.GetByCodigoExtenso("2");
            var result = await _contaService.Cadastrar(1, "Multas", TipoConta.Despesa, true, contaMaster.Id);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task CadastrarContaComCodigoExtensoExistente()
        {
            var codigo = 2;
            Func<Task> action = async () => await _contaService.Cadastrar(codigo, "Multas", TipoConta.Despesa, false);
            await action.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"Já existe uma conta cadastrada com o código {codigo}.");
        }

        [Fact]
        public async Task CadastrarContaComContaMasterInexistente()
        {
            Func<Task> action = async () => await _contaService.Cadastrar(1, "Multas", TipoConta.Despesa, true, "inexistente-id");
            await action.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Conta não encontrada.");


        }

        [Fact]
        public async Task DeletarContaComSucesso()
        {
            Func<Task> action = async () => await _contaService.Deletar("e8e8e8e8-8888-8888-8888-888888888888");
            await action.Should().NotThrowAsync();

        }

        [Fact]
        public async Task DeletarContaComSubcontas()
        {
            Func<Task> action = async () => await _contaService.Deletar("b4b4b4b4-4444-4444-4444-444444444444");
            await action.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Não é possível excluir uma conta que possui subcontas.");
        }

        [Fact]
        public async Task DeletarIdInexistente()
        {
            Func<Task> action = async () => await _contaService.Deletar("inexistente-id");
            await action.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Conta não encontrada.");
        }
        [Fact]
        public async Task AtualizarContaComSucesso()
        {
            var conta = await _contaRepository.GetByCodigoExtenso("10");
            var result = await _contaService.Atualizar(conta.Id, conta.Codigo, "Taxa Condominial Atualizada", conta.Tipo, conta.AceitaLancamento, conta.ContaMasterId);
            result.Should().NotBeNull();
            result = await _contaRepository.GetByCodigoExtenso(conta.CodigoExtenso);
            result.Descricao.Should().Be("Taxa Condominial Atualizada");
        }
        [Fact]
        public async Task AtualizarContaComCodigoExtensoExistente()
        {
            var conta = await _contaRepository.GetByCodigoExtenso("10");
            var novoCodigo = 2; // Código que já existe
            Func<Task> action = async () => await _contaService.Atualizar(conta.Id, novoCodigo, "Taxa Condominial Atualizada", conta.Tipo, conta.AceitaLancamento, conta.ContaMasterId);
            await action.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"Já existe uma conta cadastrada com o código {novoCodigo}.");
        }
    }


}
