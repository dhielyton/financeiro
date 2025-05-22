using Controle.Financeiro.Domain.PlanoContas;
using Controle.Financeiro.Domain.PlanoContas.Exceptions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle.Financiero.Test.PlanoContas
{
    public class PlanoContaTest
    {
        [Fact]
        public void CadastrarContaComSucesso()
        {
            var conta = new Conta("1", "Receitas", TipoConta.Receita, false);
            conta.Descricao.Should().NotBeNullOrEmpty();
            conta.Tipo.Should().Be(TipoConta.Receita);
            conta.AceitaLancamento.Should().BeFalse();
        }

        [Fact]
        public void CadastrarContaComPaiComSucesso()
        {
            var conta = new Conta("1", "Receitas", TipoConta.Receita, false);
            var subconta = new Conta("1.1","Taxa Condominial", TipoConta.Receita, true);
            subconta.AddContarMaster(conta);
            subconta.Descricao.Should().NotBeNullOrEmpty();
            subconta.Tipo.Should().Be(TipoConta.Receita);
            subconta.AceitaLancamento.Should().BeTrue();
            subconta.ContaMaster.Should().NotBeNull();
        }

        [Fact]
        public void AgruparContasComTiposDiferentes()
        {
            var conta = new Conta("1", "Receitas", TipoConta.Receita, false);
            var subconta = new Conta("2.1", "Salario", TipoConta.Despesa, true);
            Action act = () => subconta.AddContarMaster(conta);
            act.Should().Throw<InvalidOperationContasTiposIncompativeisException>();
        }

        [Fact]
        public void AdicionarContaFilhaUmaContaQueAceitaLancamentos()
        {
            var conta = new Conta("2.1.4", "Ferias", TipoConta.Despesa, true);
            var subconta = new Conta("2.1.2", "Salario", TipoConta.Despesa, true);
            Action act = () => subconta.AddContarMaster(conta);
            act.Should().Throw<InvalidOperationContaMasterAceitaLancamentoIsTrueException>();
        }
    }

    

    

    

    
}
