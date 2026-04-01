using Gestao_Patrimonio.Contexts;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public EnderecoRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Endereco> Listar()
        {
            return _context.Endereco.OrderBy(e => e.Logradouro).ToList();
        }

        public Endereco BuscarPorId(Guid enderecoId)
        {
            return _context.Endereco.Find(enderecoId);
        }

        public Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId, Guid? enderecoId = null)
        {
            //return _context.Endereco.FirstOrDefault(e => e.Logradouro == logradouro && e.Numero == numero && e.BairroID == bairroId);
            var consulta = _context.Endereco.AsQueryable();

            if (enderecoId.HasValue)
            {
                consulta = consulta.Where(endereco => endereco.EnderecoID != enderecoId.Value);
            }

            return consulta.FirstOrDefault(endereco =>
                endereco.Logradouro.ToLower() == logradouro.ToLower() &&
                endereco.Numero == numero &&
                endereco.BairroID == bairroId
            );
        }

        public bool BairroExiste(Guid bairroId)
        {
            return _context.Bairro.Any(b => b.BairroID == bairroId);
        }

        public void Adicionar(Endereco endereco)
        {
            _context.Endereco.Add(endereco);
            _context.SaveChanges();
        }

        public void Atualizar(Endereco endereco)
        {
            if (endereco == null)
            {
                return;
            }

            Endereco enderecoBanco = _context.Endereco.FirstOrDefault(e => e.EnderecoID == endereco.EnderecoID);

            if (enderecoBanco == null)
            {
                return;
            }

            enderecoBanco.Logradouro = endereco.Logradouro;
            enderecoBanco.Numero = endereco.Numero;
            enderecoBanco.Complemento = endereco.Complemento;
            enderecoBanco.CEP = endereco.CEP;
            enderecoBanco.BairroID = endereco.BairroID;

            _context.Update(enderecoBanco);
            _context.SaveChanges();
        }

    }
}
