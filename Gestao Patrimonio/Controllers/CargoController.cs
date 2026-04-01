using Gestao_Patrimonio.Applications.Services;
using Gestao_Patrimonio.DTOs.CargoDto;
using Gestao_Patrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Patrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly CargoService _service;

        public CargoController(CargoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarCargoDto>> Listar()
        {
            List<ListarCargoDto> cargos = _service.Listar();
            return Ok(cargos);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarCargoDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarCargoDto cargo = _service.BuscarPorId(id);
                return Ok(cargo);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarCargoDto criarCargo)
        {
            try
            {
                _service.Adicionar(criarCargo);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(CriarCargoDto cargoDto, Guid id)
        {
            try
            {
                _service.Atualizar(cargoDto, id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
