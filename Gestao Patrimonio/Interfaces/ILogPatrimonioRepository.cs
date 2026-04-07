using Gestao_Patrimonio.Domains;

namespace Gestao_Patrimonio.Interfaces
{
    public interface ILogPatrimonioRepository
    {
        List<LogPatrimonio> Listar();
        List<LogPatrimonio> BuscarPorPatrimonio(Guid patrimonioId);
    }
}
