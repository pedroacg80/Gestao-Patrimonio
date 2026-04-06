using Gestao_Patrimonio.Applications.Regras;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.PatrimonioDto;
using Gestao_Patrimonio.Exceptions;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Applications.Services
{
    public class PatrimonioService
    {
        private readonly IPatrimonioRepository _repository;

        public PatrimonioService(IPatrimonioRepository repository)
        {
            _repository = repository;
        }

        private static ListarPatrimonioDto LerDto(Patrimonio patrimonio)
        {
            ListarPatrimonioDto patrimonioDto = new ListarPatrimonioDto()
            {
                Denominacao = patrimonio.Denominacao,
                NumeroPatrimonio = patrimonio.NumeroPatrimonio,
                Valor = patrimonio.Valor,
                Imagem = patrimonio.Imagem,
                LocalizacaoID = patrimonio.LocalizacaoID,
                TipoPatrimonioID = patrimonio.TipoPatrimonioID,
                StatusPatrimonioID = patrimonio.StatusPatrimonioID,
            };

            return patrimonioDto;
        }

        public List<ListarPatrimonioDto> Listar()
        {
            List<Patrimonio> patrimonios = _repository.Listar();

            List<ListarPatrimonioDto> patrimoniosDto = patrimonios.Select(patrimonio => LerDto(patrimonio)).ToList();

            return patrimoniosDto;
        }

        public ListarPatrimonioDto BuscarPorId(Guid id)
        {
            Patrimonio patrimonio = _repository.BuscarPorId(id);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimonio nao encontrado");
            }

            return LerDto(patrimonio);
        }

        public void Adicionar(CriarPatrimonioDto criarPatrimonioDto)
        {
            Validar.ValidarNome(criarPatrimonioDto.Denominacao);
            Validar.ValidarNumeroPatrimonio(criarPatrimonioDto.NumeroPatrimonio);

            Patrimonio patrimonioExiste = _repository.BuscarPorNumeroPatrimonio(criarPatrimonioDto.NumeroPatrimonio);

            if (patrimonioExiste != null)
            {
                throw new DomainException("Ja existe um patrimonio cadastrado com esse numero");
            }


            Patrimonio patrimonio = new Patrimonio
            {
                Denominacao = criarPatrimonioDto.Denominacao,
                NumeroPatrimonio = criarPatrimonioDto.NumeroPatrimonio,
                Valor = criarPatrimonioDto.Valor,
                Imagem = criarPatrimonioDto.Imagem,
                LocalizacaoID = criarPatrimonioDto.LocalizacaoID,
                TipoPatrimonioID = criarPatrimonioDto.TipoPatrimonioID,
                StatusPatrimonioID = criarPatrimonioDto.StatusPatrimonioID
            };

            _repository.Adicionar(patrimonio);
        }
    }
}
