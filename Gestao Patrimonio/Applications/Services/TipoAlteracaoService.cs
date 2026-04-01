using Gestao_Patrimonio.Applications.Regras;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.TipoAlteracao;
using Gestao_Patrimonio.Exceptions;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Applications.Services
{
    public class TipoAlteracaoService
    {
        private readonly ITipoAlteracaoRepository _repository;

        public TipoAlteracaoService(ITipoAlteracaoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoAlteracaoDto> Listar()
        {
            List<TipoAlteracao> tipoAlteracoes = _repository.Listar();

            List<ListarTipoAlteracaoDto> tipoAlteracoesDto = tipoAlteracoes.Select(tipoAlteracao => new ListarTipoAlteracaoDto
            {
                TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                NomeTipo = tipoAlteracao.NomeTipo
            }).ToList();

            return tipoAlteracoesDto;
        }

        public ListarTipoAlteracaoDto BuscarPorId(Guid id)
        {
            TipoAlteracao tipoAlteracao = _repository.BuscarPorId(id);

            if (tipoAlteracao == null)
            {
                throw new DomainException("Tipo alteracao nao encontrado");
            }

            ListarTipoAlteracaoDto tipoAlteracaoDto = new ListarTipoAlteracaoDto
            {
                TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                NomeTipo = tipoAlteracao.NomeTipo
            };

            return tipoAlteracaoDto;
        }

        public void Adicionar(CriarTipoAlteracaoDto criarTipoAlteracaoDto)
        {
            Validar.ValidarNome(criarTipoAlteracaoDto.NomeTipo);

            TipoAlteracao tipoExistente = _repository.BuscarPorNome(criarTipoAlteracaoDto.NomeTipo);

            if (tipoExistente != null)
            {
                throw new DomainException("Ja existe um tipo existente com esse nome");
            }

            TipoAlteracao tipoAlteracao = new TipoAlteracao
            {
                NomeTipo = criarTipoAlteracaoDto.NomeTipo
            };

            _repository.Adicionar(tipoAlteracao);
        }

        public void Atualizar(CriarTipoAlteracaoDto criarTipoAlteracaoDto, Guid id)
        {
            Validar.ValidarNome(criarTipoAlteracaoDto.NomeTipo);

            TipoAlteracao tipoExistente = _repository.BuscarPorNome(criarTipoAlteracaoDto.NomeTipo);

            if (tipoExistente != null)
            {
                throw new DomainException("Ja existe um tipo Alteracao com esse nome cadastrado");
            }

            TipoAlteracao tipoBanco = _repository.BuscarPorId(id);

            if (tipoBanco == null)
            {
                throw new DomainException("Tipo alteracao nao encontrado");
            }

            tipoBanco.NomeTipo = criarTipoAlteracaoDto.NomeTipo;

            _repository.Atualizar(tipoBanco); 

        }

    }
}
