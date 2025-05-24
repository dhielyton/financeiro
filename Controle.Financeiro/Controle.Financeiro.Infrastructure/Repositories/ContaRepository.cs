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

        public async Task<Conta> GetByCodigoExtenso(string codigoExtenso)
        {
            return await _dbSet.Include(x => x.ContaMaster).Where(x => x.CodigoExtenso == codigoExtenso).FirstOrDefaultAsync();
        }


    }
}
