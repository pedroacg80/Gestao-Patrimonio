using Gestao_Patrimonio.Applications.Services;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.StatusPatrimonioDto;
using Gestao_Patrimonio.DTOs.StatusTransferenciaDto;
using Gestao_Patrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Patrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusTransferenciaController : ControllerBase
    {
        private readonly StatusTransferenciaService _service;

        public StatusTransferenciaController(StatusTransferenciaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarStatusTransferenciaDto>> Listar()
        {
            List<ListarStatusTransferenciaDto> statusTransferencias = _service.Listar();
            return Ok(statusTransferencias);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarStatusTransferenciaDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarStatusTransferenciaDto statusTransferencia = _service.BuscarPorId(id);
                return Ok(statusTransferencia);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarStatusTransferenciaDto criarStatusTransferenciaDto)
        {
            try
            {
                _service.Adicionar(criarStatusTransferenciaDto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(CriarStatusTransferenciaDto criarStatusTransferenciaDto, Guid id)
        {
            try
            {
                _service.Atualizar(criarStatusTransferenciaDto, id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
