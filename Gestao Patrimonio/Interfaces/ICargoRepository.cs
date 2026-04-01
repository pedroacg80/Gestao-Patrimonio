using Gestao_Patrimonio.Domains;

namespace Gestao_Patrimonio.Interfaces
{
    public interface ICargoRepository
    {
        List<Cargo> Listar();
        Cargo BuscarPorId(Guid cargoId);
        Cargo BuscarPorNome(string nomeCargo);
        void Adicionar(Cargo cargo);
        void Atualizar(Cargo cargo);
    }
}
