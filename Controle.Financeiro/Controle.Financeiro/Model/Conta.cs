using Controle.Financeiro.Domain.PlanoContas;

namespace Controle.Financeiro.API.Model
{
    public class Conta
    {
        public string GrupoMasterId { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public TipoConta TipoConta { get; set; }
        public bool AceitaLancamento { get; set; }
    }
}
