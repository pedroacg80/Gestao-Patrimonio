using Gestao_Patrimonio.Applications.Services;
using Gestao_Patrimonio.DTOs.EnderecoDto;
using Gestao_Patrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Patrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoService _service;

        public EnderecoController(EnderecoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarEnderecoDto>> Listar()
        {
            List<ListarEnderecoDto> enderecos = _service.Listar();

            return Ok(enderecos);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarEnderecoDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarEnderecoDto enderecoDto = _service.BuscarPorId(id);

                return Ok(enderecoDto);
             
            }
            catch (DomainException ex)
            {
                return BadRequest (ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarEnderecoDto enderecoDto)
        {
            try
            {
                _service.Adicionar(enderecoDto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest (ex.Message); 
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(CriarEnderecoDto enderecoDto, Guid id)
        {
            try
            {
                _service.Atualizar(enderecoDto, id);
                return Ok();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
