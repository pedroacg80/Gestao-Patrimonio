using Gestao_Patrimonio.Domains;

namespace Gestao_Patrimonio.Interfaces
{
    public interface ILocalizacaoRepository
    {
        List<Localizacao> Listar();
        Localizacao BuscarPorId(Guid localizacaoId);
        Localizacao BuscarPorNome(string nomeLocal, Guid araeId);
        bool AreaExiste(Guid areaId);
        void Adicionar(Localizacao localizacao);
        void Atualizar(Localizacao localizacao);
    }
}
