namespace Controle.Financeiro.API.Model
{
    public class ProximoCodigoResult
    {
        public ProximoCodigoResult(string codigoSequencia)
        {
            CodigoSequencia = codigoSequencia;
        }

        public string CodigoSequencia { get; private set; }
    }
}
