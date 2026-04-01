using Gestao_Patrimonio.Applications.Services;
using Gestao_Patrimonio.DTOs.TipoUsuario;
using Gestao_Patrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Patrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly TipoUsuarioService _service;

        public TipoUsuarioController(TipoUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarTipoUsuarioDto>> Listar()
        {
            List<ListarTipoUsuarioDto> tipoUsuarios = _service.Listar();

            return Ok(tipoUsuarios);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarTipoUsuarioDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarTipoUsuarioDto tipoUsuario = _service.BuscarPorId(id);
                return Ok(tipoUsuario);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarTipoUsuarioDto criarTipoUsuarioDto)
        {
            try
            {
                _service.Adicionar(criarTipoUsuarioDto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(CriarTipoUsuarioDto criarTipoUsuarioDto, Guid id)
        {
            try
            {
                _service.Atualizar(criarTipoUsuarioDto, id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
