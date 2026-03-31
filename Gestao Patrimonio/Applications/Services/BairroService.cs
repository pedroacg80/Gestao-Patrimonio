using Gestao_Patrimonio.Applications.Regras;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.BairroDto;
using Gestao_Patrimonio.Exceptions;
using Gestao_Patrimonio.Interfaces;
using Gestao_Patrimonio.Repositories;

namespace Gestao_Patrimonio.Applications.Services
{
    public class BairroService
    {
        private readonly IBairroRepository _repository;

        public BairroService(IBairroRepository repository)
        {
            _repository = repository;
        }

        public List<ListarBairroDto> Listar()
        {
            List<Bairro> bairros = _repository.Listar();

            List<ListarBairroDto> bairrosDto = bairros.Select(bairro => new ListarBairroDto
            {
                BairroId = bairro.BairroID,
                NomeBairro = bairro.NomeBairro, 
                CidadeId = bairro.CidadeID
            }).ToList();

            return bairrosDto;
        }

        public ListarBairroDto BuscarPorId(Guid bairroId)
        {
            Bairro bairro = _repository.BuscarPorId(bairroId);

            if (bairro == null)
            {
                throw new DomainException("Bairro nao encontrado");
            }

            ListarBairroDto bairroDto = new ListarBairroDto
            {
                BairroId = bairro.BairroID,
                NomeBairro = bairro.NomeBairro,
                CidadeId = bairro.CidadeID
            };

            return bairroDto;   
        }

        public void Adicionar(CriarBairroDto dto)
        {
            Validar.ValidarNome(dto.NomeBairro);

            Bairro bairroExistente = _repository.BuscarPorNome(dto.NomeBairro, dto.CidadeId);

            if (bairroExistente != null)
            {
                throw new DomainException("Ja existe um bairro cadastrado com esse nome");
            }

            Bairro bairro = new Bairro
            {
                NomeBairro = dto.NomeBairro,
                CidadeID = dto.CidadeId
            };

            _repository.Adicionar(bairro);
        }

        public void Atualizar(CriarBairroDto dto, Guid id)
        {
            Validar.ValidarNome(dto.NomeBairro);

            Bairro bairroBanco = _repository.BuscarPorId(id);

            if (bairroBanco == null)
            {
                throw new DomainException("Bairro nao encontrado");
            }

            Bairro bairroExistente = _repository.BuscarPorNome(dto.NomeBairro, dto.CidadeId);

            if (bairroExistente != null)
            {
                throw new DomainException("Ja existe um bairro cadastrado com esse nome");
            }

            bairroBanco.NomeBairro = dto.NomeBairro;
            bairroBanco.CidadeID = dto.CidadeId;

            _repository.Atualizar(bairroBanco);
        }
    }
}
