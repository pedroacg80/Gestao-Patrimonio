using Gestao_Patrimonio.Domains;

namespace Gestao_Patrimonio.Interfaces
{
    public interface IEnderecoRepository
    {
        List<Endereco> Listar();
        Endereco BuscarPorId(Guid enderecoId);
        Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId, Guid? enderecoId = null);
        bool BairroExiste(Guid bairroId);
        void Adicionar (Endereco endereco);
        void Atualizar(Endereco endereco);
       
    }
}
