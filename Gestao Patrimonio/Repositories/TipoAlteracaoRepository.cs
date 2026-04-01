using Gestao_Patrimonio.Contexts;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Repositories
{
    public class TipoAlteracaoRepository : ITipoAlteracaoRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public TipoAlteracaoRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<TipoAlteracao> Listar()
        {
            return _context.TipoAlteracao.OrderBy(ta => ta.NomeTipo).ToList();
        }

        public TipoAlteracao BuscarPorId(Guid tipoAlteracaoId)
        {
            return _context.TipoAlteracao.Find(tipoAlteracaoId);
        }

        public TipoAlteracao BuscarPorNome(string nomeTipo)
        {
            return _context.TipoAlteracao.FirstOrDefault(ta => ta.NomeTipo == nomeTipo);
        }

        public void Adicionar(TipoAlteracao tipoAlteracao)
        {
            _context.TipoAlteracao.Add(tipoAlteracao);
            _context.SaveChanges();
        }

        public void Atualizar (TipoAlteracao tipoAlteracao)
        {
            if (tipoAlteracao == null)
            {
                return;
            }

            TipoAlteracao tipoAlteracaoBanco = _context.TipoAlteracao.FirstOrDefault(ta => ta.TipoAlteracaoID == tipoAlteracao.TipoAlteracaoID);

            tipoAlteracaoBanco.NomeTipo = tipoAlteracao.NomeTipo;

            _context.SaveChanges();
            
        }
    }
}
