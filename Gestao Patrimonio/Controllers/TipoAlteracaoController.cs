using Gestao_Patrimonio.Applications.Services;
using Gestao_Patrimonio.DTOs.TipoAlteracao;
using Gestao_Patrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Patrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAlteracaoController : ControllerBase
    {
        private readonly TipoAlteracaoService _service;

        public TipoAlteracaoController(TipoAlteracaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult <List<ListarTipoAlteracaoDto>> Listar()
        {
            List<ListarTipoAlteracaoDto> tipoAlteracoes = _service.Listar();
            return Ok(tipoAlteracoes);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarTipoAlteracaoDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarTipoAlteracaoDto tipoAlteracao = _service.BuscarPorId(id);
                return Ok(tipoAlteracao);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarTipoAlteracaoDto criarTipoAlteracaoDto)
        {
            try
            {
                _service.Adicionar(criarTipoAlteracaoDto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(CriarTipoAlteracaoDto criarTipoAlteracaoDto, Guid id)
        {
            try
            {
                _service.Atualizar(criarTipoAlteracaoDto, id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
