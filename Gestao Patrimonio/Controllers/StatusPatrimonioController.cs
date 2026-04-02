using Gestao_Patrimonio.Applications.Services;
using Gestao_Patrimonio.DTOs.StatusPatrimonioDto;
using Gestao_Patrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Patrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusPatrimonioController : ControllerBase
    {
        private readonly StatusPatrimonioService _service;

        public StatusPatrimonioController(StatusPatrimonioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarStatusPatrimonioDto>> Listar()
        {
            List<ListarStatusPatrimonioDto> statusPatrimonios = _service.Listar();
            return Ok(statusPatrimonios);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarStatusPatrimonioDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarStatusPatrimonioDto statusPatrimonio = _service.BuscarPorId(id);
                return Ok(statusPatrimonio);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarStatusPatrimonioDto criarStatusPatrimonioDto)
        {
            try
            {
                _service.Adicionar(criarStatusPatrimonioDto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(CriarStatusPatrimonioDto criarStatusPatrimonioDto, Guid id)
        {
            try
            {
                _service.Atualizar(criarStatusPatrimonioDto, id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
