using Gestao_Patrimonio.Applications.Regras;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.CargoDto;
using Gestao_Patrimonio.Exceptions;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Applications.Services
{
    public class CargoService
    {
        private readonly ICargoRepository _repository;

        public CargoService (ICargoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarCargoDto> Listar()
        {
            List<Cargo> cargos = _repository.Listar();

            List<ListarCargoDto> cargosDto = cargos.Select(cargo => new ListarCargoDto
            {
                CargoID = cargo.CargoID, 
                NomeCargo = cargo.NomeCargo,
            }).ToList();

            return cargosDto;
        }

        public ListarCargoDto BuscarPorId(Guid id)
        {
            Cargo cargo = _repository.BuscarPorId(id);

            if (cargo == null)
            {
                throw new DomainException("Cargo nao encontrado");
            }

            ListarCargoDto cargoDto = new ListarCargoDto
            {
                CargoID = cargo.CargoID,
                NomeCargo= cargo.NomeCargo,
            };

            return cargoDto;
        }

        public void Adicionar(CriarCargoDto cargoDto)
        {
            Validar.ValidarNome(cargoDto.NomeCargo);

            Cargo cargoExistente = _repository.BuscarPorNome(cargoDto.NomeCargo);

            if (cargoExistente != null)
            {
                throw new DomainException("Ja existe um cargo cadastrado com esse nome");
            }

            Cargo cargo = new Cargo
            {
                NomeCargo = cargoDto.NomeCargo
            };

            _repository.Adicionar(cargo);
        }

        public void Atualizar(CriarCargoDto cargoDto, Guid id)
        {
            Validar.ValidarNome(cargoDto.NomeCargo);

            Cargo cargoExistente = _repository.BuscarPorNome(cargoDto.NomeCargo);

            if (cargoExistente != null)
            {
                throw new DomainException("Ja existe um cargo cadastrado com esse nome");
            }

            Cargo cargoBanco = _repository.BuscarPorId(id); 

            if (cargoBanco == null)
            {
                throw new DomainException("Cargo nao encontrado");
            }

            cargoBanco.NomeCargo = cargoDto.NomeCargo;

            _repository.Atualizar(cargoBanco);
        }
    }
}
