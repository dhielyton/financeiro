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

        
    }
}
