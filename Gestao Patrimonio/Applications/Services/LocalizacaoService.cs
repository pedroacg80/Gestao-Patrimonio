using Gestao_Patrimonio.Applications.Regras;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.LocalizacaoDto;
using Gestao_Patrimonio.Exceptions;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Applications.Services
{
    public class LocalizacaoService
    {
        private readonly ILocalizacaoRepository _repository;

        public LocalizacaoService(ILocalizacaoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarLocalizacaoDto> Listar()
        {
            List<Localizacao> localizacaos = _repository.Listar();

            List<ListarLocalizacaoDto> localizacoesDto = localizacaos.Select(localizacao => new ListarLocalizacaoDto
            {
                LocalizacaoID = localizacao.LocalizacaoID,
                NomeLocal = localizacao.NomeLocal,
                LocalSAP = localizacao.LocalSAP,
                DescricaoSAP = localizacao.DescricaoSAP,
                AreaID = localizacao.AreaID
            }).ToList();

            return localizacoesDto;
        }

        public ListarLocalizacaoDto BuscarPorId(Guid id)
        {
            Localizacao localizacao = _repository.BuscarPorId(id);

            if (localizacao == null)
            {
                throw new DomainException("Localizacao nao encontrada");
            }

            ListarLocalizacaoDto localizacaoDto = new ListarLocalizacaoDto
            {
                LocalizacaoID = localizacao.LocalizacaoID,
                NomeLocal = localizacao.NomeLocal,
                LocalSAP = localizacao.LocalSAP,
                DescricaoSAP = localizacao.DescricaoSAP,
                AreaID = localizacao.AreaID
            };

            return localizacaoDto;

        }

        public void Adicionar(CriarLocalizacaoDto dto)
        {
            Validar.ValidarNome(dto.NomeLocal);
            
            Localizacao localExistente = _repository.BuscarPorNome(dto.NomeLocal, dto.AreaID);

            if (localExistente != null)
            {
                throw new DomainException("Ja existe um local cadastrado com esse nome nessa area");
            }

            if (!_repository.AreaExiste(dto.AreaID))
            {
                throw new DomainException("Area informada nao existe");
            }



            Localizacao localizacao = new Localizacao
            {
                NomeLocal = dto.NomeLocal,
                LocalSAP = dto.LocalSAP, 
                DescricaoSAP = dto.DescicaoSAP,
                AreaID = dto.AreaID
            };

            _repository.Adicionar(localizacao);
        }

        public void Atualizar(Guid localizacaoId, CriarLocalizacaoDto dto)
        {
            Validar.ValidarNome(dto.NomeLocal);

            Localizacao localizacaoBanco = _repository.BuscarPorId(localizacaoId);


            if (localizacaoId == null)
            {
                throw new DomainException("Localizacao nao encontrada");
            }

            Localizacao localExistente = _repository.BuscarPorNome(dto.NomeLocal, dto.AreaID);

            if (localExistente != null)
            {
                throw new DomainException("Ja existe um local cadastrado com esse nome nessa area");
            }

            if (!_repository.AreaExiste(dto.AreaID))
            {
                throw new DomainException("Area informada nao existe");
            }

            localizacaoBanco.NomeLocal = dto.NomeLocal; 
            localizacaoBanco.LocalSAP = dto.LocalSAP;
            localizacaoBanco.DescricaoSAP = dto.DescicaoSAP;
            localizacaoBanco.AreaID = dto.AreaID;   

            _repository.Atualizar(localizacaoBanco);
        }
    }
}
