using Gestao_Patrimonio.Applications.Regras;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.StatusTransferenciaDto;
using Gestao_Patrimonio.Exceptions;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Applications.Services
{
    public class StatusTransferenciaService
    {
        private readonly IStatusTransferenciaRepository _repository;

        public StatusTransferenciaService(IStatusTransferenciaRepository repository)
        {
            _repository = repository;
        }

        public List<ListarStatusTransferenciaDto> Listar()
        {
            List<StatusTransferencia> statusTransferencias = _repository.Listar();

            List<ListarStatusTransferenciaDto> statusTransferenciasDto = statusTransferencias.Select(statusTransferencia => new ListarStatusTransferenciaDto
            {
                StatusTransferenciaID = statusTransferencia.StatusTransferenciaID,
                NomeStatus = statusTransferencia.NomeStatus
            }).ToList();

            return statusTransferenciasDto;
        }

        public ListarStatusTransferenciaDto BuscarPorId(Guid id)
        {
            StatusTransferencia statusTransferencia = _repository.BuscarPorId(id);

            if (statusTransferencia == null)
            {
                throw new DomainException("Status Patrimonio nao encontrado");
            }

            ListarStatusTransferenciaDto statusTransferenciaDto = new ListarStatusTransferenciaDto
            {
                StatusTransferenciaID = statusTransferencia.StatusTransferenciaID,
                NomeStatus = statusTransferencia.NomeStatus,
            };

            return statusTransferenciaDto;
        }

        public void Adicionar(CriarStatusTransferenciaDto criarStatusTransferenciaDto)
        {
            Validar.ValidarNome(criarStatusTransferenciaDto.NomeStatus);

            StatusTransferencia statusExistente = _repository.BuscarPorNome(criarStatusTransferenciaDto.NomeStatus);

            if (statusExistente != null)
            {
                throw new DomainException("Ja existe um Status Patrimonio com esse nome");
            }

            StatusTransferencia statusTransferencia = new StatusTransferencia()
            {
                NomeStatus = criarStatusTransferenciaDto.NomeStatus
            };

            _repository.Adicionar(statusTransferencia);
        }

        public void Atualizar(CriarStatusTransferenciaDto criarStatusTransferenciaDto, Guid id)
        {
            Validar.ValidarNome(criarStatusTransferenciaDto.NomeStatus);

            StatusTransferencia statusExistente = _repository.BuscarPorNome(criarStatusTransferenciaDto.NomeStatus);

            if (statusExistente != null)
            {
                throw new DomainException("Ja existe um Status patrimonio com esse nome");
            }

            StatusTransferencia statusBanco = _repository.BuscarPorId(id);

            if (statusBanco == null)
            {
                throw new DomainException("Status patrimonio nao encontrado");
            }

            statusBanco.NomeStatus = criarStatusTransferenciaDto.NomeStatus;

            _repository.Atualizar(statusBanco);
        }

    }
}
