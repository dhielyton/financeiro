using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Controle.Financeiro.Domain.PlanoContas
{
    public class ContaService
    {
        private readonly IContaRepository _contaRepository;

        public ContaService(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<Conta> Cadastrar(int codigo, string descricao, TipoConta tipo, bool aceitaLancamento, string? contaMasterId = null)
        {
            var conta = new Conta(codigo, descricao, tipo, aceitaLancamento);

            await verificaCodigoExtensoJaCadastrado(conta.CodigoExtenso);

            if (contaMasterId != null)
            {
                var contaMaster = await ObterContaPorId(contaMasterId);
                conta.AddContarMaster(contaMaster);
            }

            return await _contaRepository.Insert(conta);
        }

        public async Task<Conta> Atualizar(string id, int codigo, string descricao, TipoConta tipo, bool aceitaLancamento, string? contaMasterId = null)
        {
            var conta = await _contaRepository.Get(id);
            if (conta == null)
                throw new InvalidOperationException("Conta não encontrada.");

            conta.Codigo = codigo;
            conta.Descricao = descricao;
            conta.Tipo = tipo;
            conta.AceitaLancamento = aceitaLancamento;

            if (contaMasterId != null)
            {
                var contaMaster = await ObterContaPorId(contaMasterId);
                conta.AddContarMaster(contaMaster);
            }
            else
            {
                conta.ContaMasterId = null;
            }

            conta.GerarCodigoExtenso();

            await verificaCodigoExtensoJaCadastrado(conta.CodigoExtenso, id);

            return await _contaRepository.Update(conta);
        }

        private async Task verificaCodigoExtensoJaCadastrado(string codigo, string id = null)
        {
            var conta = await _contaRepository.GetByCodigoExtenso(codigo);
            if ((conta != null) && (conta.Id != id))
            {
                throw new InvalidOperationException($"Já existe uma conta cadastrada com o código {codigo}.");
            }
        }


        public async Task<Conta> ObterContaPorId(string id)
        {
            var conta = await _contaRepository.Get(id);
            if (conta == null)
                throw new InvalidOperationException("Conta não encontrada.");
            return conta;
        }

        public async Task Deletar(string id)
        {
            var conta = await _contaRepository.Get(id);
            if (conta == null)
                throw new InvalidOperationException("Conta não encontrada.");
            if ((!conta.AceitaLancamento) &&
                (await _contaRepository.GetCodigoMaxGrupoConta(conta) > 0))
            {
                throw new InvalidOperationException("Não é possível excluir uma conta que possui subcontas.");
            }
            await _contaRepository.Delete(conta);
        }

        public async Task<string> ProximoCodigo(string Id)
        {

            var conta = await _contaRepository.Get(Id);
            var codigoMaximo = await _contaRepository.GetCodigoMaxGrupoConta(conta);
            if (codigoMaximo == Conta.CodigoLimite)
            {
                if (conta.ContaMaster == null)
                    throw new InvalidOperationException($"Não é possível sugerir um código maior que {Conta.CodigoLimite}.");
                return await ProximoCodigo(conta.ContaMasterId);
            }

            return $"{conta.CodigoExtenso}.{++codigoMaximo}";
        }

        public async Task<IEnumerable<Conta>> GetAll()
        {
            return await _contaRepository.GetAll();
        }
    }
}
