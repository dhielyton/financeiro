using Controle.Financeiro.Domain.PlanoContas;
using Controle.Financiero.Test.PlanoContas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Controle.Financiero.Test.PlanoContas
{
    public static class GerarContasUtil
    {

        public static List<ContaItem> Deserializar(string jsonPath)
        {
            string json = File.ReadAllText(jsonPath);
           return JsonSerializer.Deserialize<List<ContaItem>>(json).OrderBy(x => x.CodigoExtenso).ToList();

        }
    }
}
