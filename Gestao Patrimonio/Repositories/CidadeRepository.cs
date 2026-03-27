using Gestao_Patrimonio.Contexts;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public CidadeRepository(GestaoPatrimoniosContext context)
        {
            _context = context; 
        }

        public List<Cidade> Listar()
        {
            return _context.Cidade.OrderBy(c => c.NomeCidade).ToList();
        }

        public Cidade BuscarPorId(Guid cidadeId)
        {
            return _context.Cidade.Find(cidadeId);
        }

        public Cidade BuscarPorNomeEEstado(string nomeCidade, string nomeEstado)
        {
            return _context.Cidade.FirstOrDefault(c => c.NomeCidade == nomeCidade && c.Estado == nomeEstado);
        }

        public void Adicionar(Cidade cidade)
        {
            _context.Cidade.Add(cidade);
            _context.SaveChanges();
        }

        public void Atualizar(Cidade cidade)
        {
            if (cidade == null)
            {
                return;
            }

            Cidade cidadeBanco = _context.Cidade.Find(cidade.CidadeID);

            cidadeBanco.Estado = cidade.Estado;
            cidadeBanco.NomeCidade = cidade.NomeCidade;
            
            _context.SaveChanges();
        }
    }
}
