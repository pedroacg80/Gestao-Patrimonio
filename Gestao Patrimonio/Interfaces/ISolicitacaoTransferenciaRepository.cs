using Gestao_Patrimonio.Domains;

namespace Gestao_Patrimonio.Interfaces
{
    public interface ISolicitacaoTransferenciaRepository
    {
        List<SolicitacaoTransferencia> Listar();
        SolicitacaoTransferencia BuscarPorId(Guid transferenciaId);
        StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus);
        bool ExisteSolicitacaoPendente(Guid patrimonioId);
        bool UsuarioResponsavelDaLocalizacao(Guid usuarioId, Guid localizacaoId);
        void Adicionar(SolicitacaoTransferencia solicitacaoTransferencia);
        bool LocalizacaoExiste(Guid localizacaoId, Guid id);
        Patrimonio BuscarPatrimonioPorId(Guid patrimonioId);

    }
}
