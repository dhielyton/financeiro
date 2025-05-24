using Controle.Financeiro.Domain.PlanoContas;
using Microsoft.AspNetCore.Mvc;

namespace Controle.Financeiro.API.Controllers
{
    public class ContaController : Controller
    {
        private IContaRepository _contaRepository;

        public ContaController(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
