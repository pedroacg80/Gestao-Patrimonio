using Gestao_Patrimonio.Contexts;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Repositories
{
    public class BairroRepository : IBairroRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public BairroRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Bairro> Listar()
        {
            return _context.Bairro.OrderBy(b => b.NomeBairro).ToList();
        }

        public Bairro BuscarPorId(Guid bairroId)
        {
            return _context.Bairro.Find(bairroId);
        }

        public Bairro BuscarPorNome(string nomeBairro, Guid cidadeId)
        {
            return _context.Bairro.FirstOrDefault(b => b.NomeBairro == nomeBairro && b.CidadeID == cidadeId);
        }

        public bool CidadeExiste(Guid cidadeId)
        {
            return _context.Cidade.Any(c => c.CidadeID == cidadeId);
        }

        public void Adicionar(Bairro bairro)
        {
            _context.Bairro.Add(bairro);
            _context.SaveChanges();
        }

        public void Atualizar(Bairro bairro)
        {
            if (bairro == null)
            {
                return;
            }

            Bairro bairroBanco = _context.Bairro.Find(bairro.BairroID);

            bairroBanco.NomeBairro = bairro.NomeBairro;
            bairroBanco.Endereco = bairro.Endereco;
            bairroBanco.Cidade = bairro.Cidade;

            _context.SaveChanges();

        }
    }
}