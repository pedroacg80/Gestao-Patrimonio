using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.SolicitacaoTransferenciaDto;
using Gestao_Patrimonio.Exceptions;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Applications.Services
{
    public class SolicitacaoTransferenciaService
    {
        private readonly ISolicitacaoTransferenciaRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;

        public SolicitacaoTransferenciaService(ISolicitacaoTransferenciaRepository repository, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        public List<ListarSolicitacaoTransferenciaDto> Listar()
        {
            List<SolicitacaoTransferencia> solicitacoes = _repository.Listar();

            List<ListarSolicitacaoTransferenciaDto> solicitacoesDto = solicitacoes.Select(solicitacao => new ListarSolicitacaoTransferenciaDto
            {
                TransferenciaID = solicitacao.TransferenciaID,
                DataCriacaoSolicitante = solicitacao.DataCriacaoSolicitante,
                DataResposta = solicitacao.DataResposta,
                Justificativa = solicitacao.Justificativa,
                StatusTransferenciaID = solicitacao.StatusTransferenciaID,
                UsuarioIDSolicitacao = solicitacao.UsuarioIDSolicitacao,
                UsuarioIDAprovacao = solicitacao.UsuarioIDAprovacao,
                PatrimonioID = solicitacao.PatrimonioID,
                LocalizacaoID = solicitacao.LocalizacaoID
            }).ToList();

            return solicitacoesDto;
        }

        public ListarSolicitacaoTransferenciaDto BuscarPorId(Guid transferenciaId)
        {
            SolicitacaoTransferencia solicitacao = _repository.BuscarPorId(transferenciaId);

            if (solicitacao == null)
            {
                throw new DomainException("Solicitacao nao encontrada");
            }

            ListarSolicitacaoTransferenciaDto solicitacaoDto = new ListarSolicitacaoTransferenciaDto
            {
                TransferenciaID = solicitacao.TransferenciaID,
                DataCriacaoSolicitante = solicitacao.DataCriacaoSolicitante,
                DataResposta = solicitacao.DataResposta,
                Justificativa = solicitacao.Justificativa,
                StatusTransferenciaID = solicitacao.StatusTransferenciaID,
                UsuarioIDSolicitacao = solicitacao.UsuarioIDSolicitacao,
                UsuarioIDAprovacao = solicitacao.UsuarioIDAprovacao,
                PatrimonioID = solicitacao.PatrimonioID,
                LocalizacaoID = solicitacao.LocalizacaoID
            };

            return solicitacaoDto;
        }
    }
}
