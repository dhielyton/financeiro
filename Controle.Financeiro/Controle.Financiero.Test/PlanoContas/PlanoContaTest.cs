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
        public void CadastrarContaNivel1ComSucesso()
        {
            var conta = new Conta(1, "Receitas", TipoConta.Receita, false);
            conta.Descricao.Should().NotBeNullOrEmpty();
            conta.Tipo.Should().Be(TipoConta.Receita);
            conta.AceitaLancamento.Should().BeFalse();
            conta.CodigoExtenso.Should().Be("1");
        }

        [Fact]
        public void CadastrarContaNivel2ComSucesso()
        {
            var conta = new Conta(1, "Receitas", TipoConta.Receita, false);
            var subconta = new Conta(1,"Taxa Condominial", TipoConta.Receita, true);
            subconta.AddContarMaster(conta);
            subconta.Descricao.Should().NotBeNullOrEmpty();
            subconta.Tipo.Should().Be(TipoConta.Receita);
            subconta.AceitaLancamento.Should().BeTrue();
            subconta.ContaMaster.Should().NotBeNull();
            subconta.ContaMasterId.Should().Be(conta.Id);
            subconta.CodigoExtenso.Should().Be("1.1");  
        }

        [Fact]
        public void CadastrarContaNivel3ComSucesso()
        {
            var contaNivel1 = new Conta(2, "Despesas", TipoConta.Despesa, false);
            var contaNivel2 = new Conta(1, "Com Pessoal", TipoConta.Despesa, false);
            contaNivel2.AddContarMaster(contaNivel1);
            var contaNivel3 = new Conta(1, "Salário", TipoConta.Despesa, true);
            contaNivel3.AddContarMaster(contaNivel2);

            contaNivel1.CodigoExtenso.Should().Be("2");
            contaNivel2.CodigoExtenso.Should().Be("2.1");
            contaNivel3.CodigoExtenso.Should().Be("2.1.1");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1000)]
        public void CadastrarContaComCodigoForaDoIntervalo(int codigo)
        {
            var conta = new Conta(1, "Receitas", TipoConta.Receita, false);
            Action act = () => new Conta(0, "Taxa Condominial", TipoConta.Receita, true);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void AgruparContasComTiposDiferentes()
        {
            var conta = new Conta(1, "Receitas", TipoConta.Receita, false);
            var subconta = new Conta(1, "Salario", TipoConta.Despesa, true);
            Action act = () => subconta.AddContarMaster(conta);
            act.Should().Throw<InvalidOperationContasTiposIncompativeisException>();
        }

        [Fact]
        public void AdicionarContaFilhaAUmaContaQueAceitaLancamentos()
        {
            var conta = new Conta(4, "Ferias", TipoConta.Despesa, true);
            var subconta = new Conta(2, "Salario", TipoConta.Despesa, true);
            Action act = () => subconta.AddContarMaster(conta);
            act.Should().Throw<InvalidOperationContaMasterAceitaLancamentoIsTrueException>();
        }
    }

    

    

    

    
}
