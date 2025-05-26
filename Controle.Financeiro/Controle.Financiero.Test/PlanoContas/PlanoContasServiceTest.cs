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
            _context.Contas.AddRange(contas);
            _context.SaveChanges();
        }
        [Fact]
        public async Task SugerirProximoCodigoContaComSucesso()
        {
            var codigoConta = await _contaService.SugerirProximoCodigoConta("b4b4b4b4-4444-4444-4444-444444444444");
            Assert.Equal("9.10", codigoConta);
        }

        
    }
}
