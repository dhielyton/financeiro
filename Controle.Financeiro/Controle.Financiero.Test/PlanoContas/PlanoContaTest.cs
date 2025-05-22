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
            var conta = new Conta("1", "Receitas", Tipo.Receita, false);
            conta.Descricao.Should().NotBeNullOrEmpty();
            conta.Tipo.Should().Be(Tipo.Receita);
            conta.AceitaLancamento.Should().BeFalse();
        }

        [Fact]
        public void CadastrarContaComPaiComSucesso()
        {
            var conta = new Conta("1", "Receitas", Tipo.Receita, false);
            var subconta = new Conta("1.1","Taxa Condominial", Tipo.Receita, true);
            subconta.AddContarMaster(conta);
            subconta.Descricao.Should().NotBeNullOrEmpty();
            subconta.Tipo.Should().Be(Tipo.Receita);
            subconta.AceitaLancamento.Should().BeTrue();
            subconta.ContaMaster.Should().NotBeNull();
        }
    }

    public class Conta
    {
        public Conta(string codigo, string descricao, Tipo tipo, bool aceitaLancamento)
        {

            Descricao = descricao;
            Codigo = codigo;
            Tipo = tipo;
            AceitaLancamento = aceitaLancamento;
        }

        public Guid Id { get; set; }
        public Conta ContaMaster { get; private set; }
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public Tipo Tipo { get; set; }
        public bool AceitaLancamento { get; set; }

        public void AddContarMaster(Conta conta)
        {
            ContaMaster = conta;
        }



    }

    public enum Tipo
    {
        Receita,
        Despesa
    }
}
