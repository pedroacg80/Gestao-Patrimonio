using Gestao_Patrimonio.Applications.Services;
using Gestao_Patrimonio.DTOs.BairroDto;
using Gestao_Patrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Patrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BairroController : ControllerBase
    {
        private readonly BairroService _service;

        public BairroController(BairroService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarBairroDto>> Listar()
        {
            List<ListarBairroDto> bairros = _service.Listar();

            return Ok(bairros);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarBairroDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarBairroDto bairro = _service.BuscarPorId(id);

                return Ok(bairro);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarBairroDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Ok(dto);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(CriarBairroDto dto, Guid id)
        {
            try
            {
                _service.Atualizar(dto, id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
