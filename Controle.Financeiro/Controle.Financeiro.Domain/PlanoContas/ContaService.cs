using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle.Financeiro.Domain.PlanoContas
{
    public class ContaService
    {
        private readonly IContaRepository _contaRepository;

        public ContaService(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<string> SugerirProximoCodigoConta(string Id)
        {

            var conta = await _contaRepository.Get(Id);
            var codigoMaximo = await _contaRepository.GetCodigoMaxGrupoConta(conta);
            if (codigoMaximo == Conta.CodigoLimite)
            {
                if (conta.ContaMaster == null)
                    throw new InvalidOperationException($"Não é possível sugerir um código maior que {Conta.CodigoLimite}.");
                return await SugerirProximoCodigoConta(conta.ContaMasterId);
            }

            return $"{conta.CodigoExtenso}.{++codigoMaximo}";
        }
    }
}
