using Controle.Financeiro.API.Model;
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
        private readonly ContaService _contaService;

        public ContaController(ContaService contaService)
        {
            _contaService = contaService;
        }

        [HttpPost]

        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Incluir(Model.Conta contaModel)
        {
            try
            {


                var conta = await _contaService.Cadastrar(contaModel.Codigo, contaModel.Descricao, contaModel.TipoConta, contaModel.AceitaLancamento, contaModel.ContaMasterId);
                return Ok(conta);
            }
            catch (Exception)
            {

                return BadRequest();
            }


        }
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                _contaService.Deletar(id);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Atualizar(string id, [FromBody] Model.Conta contaModel)
        {
            try
            {
                var conta = await _contaService.Atualizar(
                    id,
                    contaModel.Codigo,
                    contaModel.Descricao,
                    contaModel.TipoConta,
                    contaModel.AceitaLancamento,
                    contaModel.ContaMasterId
                );
                return Ok(conta);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("/planodecontas")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var contas = await _contaService.GetAll();
                return Ok(Model.ContaItem.FromDomainList(contas));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("/proximocodigo")]
        public async Task<IActionResult> GetProximoCodigo(string grupoMasterId)
        {
            try
            {
                var codigo = await _contaService.ProximoCodigo(grupoMasterId);
                return Ok(new ProximoCodigoResult(codigo));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
