using Controle.Financeiro.Domain.PlanoContas;
using Controle.Financeiro.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle.Financeiro.Infrastructure.Repositories
{
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
        public ContaRepository(FinanceiroDbContext financeiroDbContext) : base(financeiroDbContext)
        {
        }

        public Task<List<Conta>> GetAll()
        {
            return _dbSet.OrderBy(x => x.CodigoExtenso).ToListAsync();
        }

        public async Task<Conta> GetByCodigoExtenso(string codigoExtenso)
        {
            return await _dbSet.Include(x => x.ContaMaster).Where(x => x.CodigoExtenso == codigoExtenso).FirstOrDefaultAsync();
        }

        public async Task<int> GetCodigoMaxGrupoConta(Conta grupoConta)
        {

            var codigos =  await _dbSet
                        .Where(x => x.ContaMasterId == grupoConta.Id)
                        .Select(x => x.Codigo)
                        .ToListAsync();
            return codigos.DefaultIfEmpty(0).Max();
        }


    }
}
