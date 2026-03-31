using Gestao_Patrimonio.Applications.Regras;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.CidadeDto;
using Gestao_Patrimonio.Exceptions;
using Gestao_Patrimonio.Interfaces;
using Gestao_Patrimonio.Repositories;

namespace Gestao_Patrimonio.Applications.Services
{
    public class CidadeService
    {
        private readonly ICidadeRepository _repository;

        public CidadeService(ICidadeRepository repository)
        {
            _repository = repository;   
        }

        public List<ListarCidadeDto> Listar()
        {
            List<Cidade> cidades = _repository.Listar(); 

            List<ListarCidadeDto> cidadesDto = cidades.Select(cidade => new ListarCidadeDto
            { 
                CidadeId = cidade.CidadeID,
                NomeCidade = cidade.NomeCidade,
                Estado = cidade.Estado
            }).ToList();

            return cidadesDto;
        }

        public ListarCidadeDto BuscarPorId(Guid cidadeId)
        {
            Cidade cidade = _repository.BuscarPorId(cidadeId);

            if (cidade == null)
            {
                throw new DomainException("Cidade nao encontrada");
            }

            ListarCidadeDto cidadeDto = new ListarCidadeDto
            {
                CidadeId = cidade.CidadeID,
                NomeCidade = cidade.NomeCidade,
                Estado = cidade.Estado
            };

            return cidadeDto;
        }

        public ListarCidadeDto BuscarPorNomeEEstado(string nomeCidade, string nomeEstado)
        {
            Cidade cidade = _repository.BuscarPorNomeEEstado(nomeCidade, nomeEstado);

            if (cidade == null)
            {
                throw new DomainException("Cidade nao encontrada");
            }

            ListarCidadeDto cidadeDto = new ListarCidadeDto
            {
                CidadeId = cidade.CidadeID,
                NomeCidade = cidade.NomeCidade,
                Estado = cidade.Estado
            };

            return cidadeDto;
        }

        public void Adicionar(CriarCidadeDto dto)
        {
            Validar.ValidarNome(dto.NomeCidade);
            Validar.ValidarEstado(dto.Estado);

            Cidade cidadeExiste = _repository.BuscarPorNomeEEstado(dto.NomeCidade, dto.Estado);

            if (cidadeExiste != null)
            {
                throw new DomainException("Ja existe uma cidade cadastrada com esse nome");
            }

            Cidade cidadeDto = new Cidade
            {
               NomeCidade = dto.NomeCidade,
               Estado = dto.Estado
            };

            _repository.Adicionar(cidadeDto);
        }

        public void Atualizar(Guid cidadeId, CriarCidadeDto dto)
        {
            Validar.ValidarNome(dto.NomeCidade);
            Validar.ValidarEstado (dto.Estado);

            Cidade cidadeBanco = _repository.BuscarPorId(cidadeId);

            if (cidadeBanco == null)
            {
                throw new DomainException("Cidade nao encontrada");
            }

            Cidade cidadeExistente = _repository.BuscarPorNomeEEstado(dto.NomeCidade, dto.Estado);

            if (cidadeExistente != null)
            {
                throw new DomainException("Ja existe uma cidade cadastrada com esse nome");
            }

            cidadeBanco.NomeCidade = dto.NomeCidade;    
            cidadeBanco.Estado = dto.Estado;

            _repository.Atualizar(cidadeBanco);
        }
    }
}
