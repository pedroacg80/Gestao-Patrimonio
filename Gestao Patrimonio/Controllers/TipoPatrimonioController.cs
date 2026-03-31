using Gestao_Patrimonio.Applications.Services;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.TipoPatrimonio;
using Gestao_Patrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Patrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPatrimonioController : ControllerBase
    {
        private readonly TipoPatrimonioService _service;

        public TipoPatrimonioController(TipoPatrimonioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarTipoPatrimonioDto>> Listar()
        {
            List<ListarTipoPatrimonioDto> tipoPatrimonios = _service.Listar();

            return Ok(tipoPatrimonios);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarTipoPatrimonioDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarTipoPatrimonioDto tipoPatrimonio = _service.BuscarPorId(id);
                return Ok(tipoPatrimonio);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarTipoPatrimonioDto criarTipoPatrimonioDto)
        {
            try
            {
                _service.Adicionar(criarTipoPatrimonioDto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(CriarTipoPatrimonioDto criarTipoPatrimonioDto, Guid id)
        {
            try
            {
                _service.Atualizar(criarTipoPatrimonioDto, id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
