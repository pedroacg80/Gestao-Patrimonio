using Gestao_Patrimonio.Applications.Regras;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.TipoPatrimonio;
using Gestao_Patrimonio.Exceptions;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Applications.Services
{
    public class TipoPatrimonioService
    {
        private readonly ITipoPatrimonioRepository _repository;

        public TipoPatrimonioService(ITipoPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoPatrimonioDto> Listar()
        {
            List<TipoPatrimonio> tipoPatrimonios = _repository.Listar();

            List<ListarTipoPatrimonioDto> tipoPatrimoniosDto = tipoPatrimonios.Select(tipoPatrimonio => new ListarTipoPatrimonioDto
            {
                TipoPatrimonioID = tipoPatrimonio.TipoPatrimonioID,
                NomeTipo = tipoPatrimonio.NomeTipo,
            }).ToList();

            return tipoPatrimoniosDto;
        }

        public ListarTipoPatrimonioDto BuscarPorId(Guid id)
        {
            TipoPatrimonio tipoPatrimonio = _repository.BuscarPorId(id);

            if (tipoPatrimonio == null)
            {
                throw new DomainException("Tipo patrimonio nao encontrado");
            }

            ListarTipoPatrimonioDto tipoPatrimonioDto = new ListarTipoPatrimonioDto
            {
                TipoPatrimonioID = tipoPatrimonio.TipoPatrimonioID,
                NomeTipo = tipoPatrimonio.NomeTipo
            }; 

            return tipoPatrimonioDto;
        }

        public void Adicionar(CriarTipoPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoPatrimonio tipoPatrimonioExistente = _repository.BuscarPorNome(dto.NomeTipo);

            if (tipoPatrimonioExistente != null)
            {
                throw new DomainException("Ja existe um tipo de patrimonio com esse nome");
            }

            TipoPatrimonio tipoPatrimonio = new TipoPatrimonio
            {
                NomeTipo = dto.NomeTipo
            };

            _repository.Adicionar(tipoPatrimonio);
        }

        public void Atualizar(CriarTipoPatrimonioDto dto, Guid id)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoPatrimonio tipoPatrimonioBanco = _repository.BuscarPorId(id);

            if (tipoPatrimonioBanco == null)
            {
                throw new DomainException("Tipo patrimonio nao encontrado");
            }

            TipoPatrimonio tipoPatrimonioExistente = _repository.BuscarPorNome(dto.NomeTipo);

            if (tipoPatrimonioExistente != null)
            {
                throw new DomainException("Ja existe um tipo de patrimonio com esse nome");
            }

            tipoPatrimonioBanco.NomeTipo = dto.NomeTipo;

            _repository.Atualizar(tipoPatrimonioBanco); 
        }
    }
}
