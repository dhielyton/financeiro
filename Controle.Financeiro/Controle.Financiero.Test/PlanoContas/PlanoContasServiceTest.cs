using Controle.Financeiro.Domain.PlanoContas;
using Controle.Financeiro.Infrastructure.Context;
using Controle.Financeiro.Infrastructure.Repositories;
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
        private readonly FinanceiroDbContext _context;
        public PlanoContasServiceTest()
        {

            string jsonPath = "PlanoContas/Data/ScenarioSugestaoProximoNumero.json";
            string json = File.ReadAllText(jsonPath);
            List<Conta> contas = JsonSerializer.Deserialize<List<Conta>>(json);

            var options = new DbContextOptionsBuilder<FinanceiroDbContext>()
                        .UseInMemoryDatabase("TestDb") // ou UseSqlServer("sua_connection_string")
                        .Options;

            _context = new FinanceiroDbContext(options);
            _contaRepository = new ContaRepository(_context);
           
            _context.Contas.AddRange(contas);
            _context.SaveChanges();
        }
        [Fact]
        public async Task SugerirProximoCodigoContaComSucesso() 
        {
            var codigoConta = await SugerirProximoCodigoConta("b4b4b4b4-4444-4444-4444-444444444444");
            Assert.Equal("9.10", codigoConta);
        }

        public async Task<string> SugerirProximoCodigoConta(string Id)
        {
            
            var conta = await _contaRepository.Get(Id);
            var codigoMaximo = await _contaRepository.GetCodigoMaxGrupoConta(conta);
            if(codigoMaximo == Conta.CodigoLimite)
            {
                if (conta.ContaMaster == null)
                    throw new InvalidOperationException($"Não é possível sugerir um código maior que {Conta.CodigoLimite}.");
                return  await SugerirProximoCodigoConta(conta.ContaMasterId);
            }
                
            return $"{conta.CodigoExtenso}.{++codigoMaximo}";
        }
    }
}
