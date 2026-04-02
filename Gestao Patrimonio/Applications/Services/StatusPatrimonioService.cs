using Gestao_Patrimonio.Applications.Regras;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.StatusPatrimonioDto;
using Gestao_Patrimonio.Exceptions;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Applications.Services
{
    public class StatusPatrimonioService
    {
        private readonly IStatusPatrimonioRepository _repository;

        public StatusPatrimonioService(IStatusPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarStatusPatrimonioDto> Listar()
        {
            List<StatusPatrimonio> statusPatrimonios = _repository.Listar();

            List<ListarStatusPatrimonioDto> statusPatrimoniosDto = statusPatrimonios.Select(statusPatrimonio => new ListarStatusPatrimonioDto
            {
                StatusPatrimonioID = statusPatrimonio.StatusPatrimonioID,
                NomeStatus = statusPatrimonio.NomeStatus
            }).ToList();

            return statusPatrimoniosDto;
        }

        public ListarStatusPatrimonioDto BuscarPorId(Guid id)
        {
            StatusPatrimonio statusPatrimonio = _repository.BuscarPorId(id);

            if (statusPatrimonio == null)
            {
                throw new DomainException("Status Patrimonio nao encontrado");
            }

            ListarStatusPatrimonioDto statusPatrimonioDto = new ListarStatusPatrimonioDto
            {
                StatusPatrimonioID = statusPatrimonio.StatusPatrimonioID,
                NomeStatus = statusPatrimonio.NomeStatus,
            };

            return statusPatrimonioDto; 
        }

        public void Adicionar(CriarStatusPatrimonioDto criarStatusPatrimonioDto)
        {
            Validar.ValidarNome(criarStatusPatrimonioDto.NomeStatus);

            StatusPatrimonio statusExistente = _repository.BuscarPorNome(criarStatusPatrimonioDto.NomeStatus);

            if (statusExistente != null)
            {
                throw new DomainException("Ja existe um Status Patrimonio com esse nome");
            }

            StatusPatrimonio statusPatrimonio = new StatusPatrimonio()
            {
                NomeStatus = criarStatusPatrimonioDto.NomeStatus
            };

            _repository.Adicionar(statusPatrimonio);
        }

        public void Atualizar(CriarStatusPatrimonioDto criarStatusPatrimonioDto, Guid id)
        {
            Validar.ValidarNome(criarStatusPatrimonioDto.NomeStatus);

            StatusPatrimonio statusExistente = _repository.BuscarPorNome(criarStatusPatrimonioDto.NomeStatus);

            if (statusExistente != null)
            {
                throw new DomainException("Ja existe um Status patrimonio com esse nome");
            }

            StatusPatrimonio statusBanco = _repository.BuscarPorId(id);

            if (statusBanco == null)
            {
                throw new DomainException("Status patrimonio nao encontrado");
            }

            statusBanco.NomeStatus = criarStatusPatrimonioDto.NomeStatus;

            _repository.Atualizar(statusBanco);
        }
    }
}
