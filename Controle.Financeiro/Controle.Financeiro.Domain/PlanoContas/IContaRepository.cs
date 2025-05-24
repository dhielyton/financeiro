using Controle.Financeiro.Domain.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle.Financeiro.Domain.PlanoContas
{
    public interface IContaRepository : IRepository<Conta>
    {
        Task<List<Conta>> GetAll();
        Task<Conta> GetByCodigoExtenso(string codigoExtenso);
        Task<int> GetCodigoMaxGrupoConta(Conta grupoConta);
    }

}
