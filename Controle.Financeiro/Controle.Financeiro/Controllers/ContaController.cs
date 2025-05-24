using Controle.Financeiro.Domain.PlanoContas;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Controle.Financeiro.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : Controller
    {
        private IContaRepository _contaRepository;

        public ContaController(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Conta),(int) HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Incluir(Model.Conta contaModel)
        {
            try
            {
                if(contaModel == null)
                    return BadRequest("Conta não informada.");
                var conta = new Conta(contaModel.Codigo, contaModel.Descricao, contaModel.TipoConta, contaModel.AceitaLancamento);
                if (contaModel.GrupoMasterId != null)
                {
                    var contaMaster = await _contaRepository.Get(contaModel.GrupoMasterId);
                    conta.AddContarMaster(contaMaster);
                }
               
                conta = await _contaRepository.Insert(conta);
                return Ok(conta);
            }
            catch (Exception)
            {

                return BadRequest();
            }


        }
        [HttpDelete]
        [Route("/id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var conta = await _contaRepository.Get(id);
                await _contaRepository.Delete(conta);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<Model.ContaItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var contas = await _contaRepository.GetAll();
                return Ok(Model.ContaItem.FromDomainList(contas));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
