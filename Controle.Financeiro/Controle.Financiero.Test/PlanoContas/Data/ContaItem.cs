using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle.Financiero.Test.PlanoContas.Data
{
    public class ContaItem
    {
        public string Id { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int Tipo { get; set; }
        public bool AceitaLancamento { get; set; }
        public string? ContaMasterId { get; set; }
        public string CodigoExtenso { get; set; } = string.Empty;
    }
}
