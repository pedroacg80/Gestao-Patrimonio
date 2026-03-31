using Gestao_Patrimonio.Applications.Services;
using Gestao_Patrimonio.DTOs.CidadeDto;
using Gestao_Patrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Patrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly CidadeService _service;

        public CidadeController(CidadeService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult <List<ListarCidadeDto>> Listar()
        {
            List<ListarCidadeDto> cidades = _service.Listar();

            return Ok(cidades);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarCidadeDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarCidadeDto cidade = _service.BuscarPorId(id);

                return Ok(cidade);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarCidadeDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarCidadeDto dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
