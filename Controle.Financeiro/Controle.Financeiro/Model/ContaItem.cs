namespace Controle.Financeiro.API.Model
{
    public class ContaItem
    {
        public string Id{ get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public string AceitaLancamento { get; set; }


        public static ContaItem FromDomain(Controle.Financeiro.Domain.PlanoContas.Conta conta)
        {
            if (conta == null) return null;

            return new ContaItem
            {
                Id = conta.Id,
                Codigo = conta.CodigoExtenso,
                Descricao = conta.Descricao,
                Tipo = conta.Tipo.ToString(),
                AceitaLancamento = conta.AceitaLancamento ? "Sim" : "Não"
            };
        }

        public static List<ContaItem> FromDomainList(IEnumerable<Controle.Financeiro.Domain.PlanoContas.Conta> contas)
        {
            if (contas == null) return new List<ContaItem>();
            return contas.Select(FromDomain).ToList();
        }
    }
}
