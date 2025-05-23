using Controle.Financeiro.Domain.Kernel;
using Controle.Financeiro.Domain.PlanoContas.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle.Financeiro.Domain.PlanoContas
{
    public class Conta: Entity
    {
        public Conta(string codigo, string descricao, TipoConta tipo, bool aceitaLancamento)
        {
            GenerateId();
            Descricao = descricao;
            Codigo = codigo;
            Tipo = tipo;
            AceitaLancamento = aceitaLancamento;
        }

        public Conta ContaMaster { get; private set; }
        public string ContaMasterId { get; set; }
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public TipoConta Tipo { get; set; }
        public bool AceitaLancamento { get; set; }

        public void AddContarMaster(Conta conta)
        {
            if (conta.Tipo != Tipo)
                throw new InvalidOperationContasTiposIncompativeisException();
            if (conta.AceitaLancamento)
                throw new InvalidOperationContaMasterAceitaLancamentoIsTrueException();

            ContaMaster = conta;
            ContaMasterId = conta.Id;
        }



    }
}
