using Gestao_Patrimonio.Contexts;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Repositories
{
    public class TipoPatrimonioRepository : ITipoPatrimonioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public TipoPatrimonioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<TipoPatrimonio> Listar()
        {
            return _context.TipoPatrimonio.OrderBy(tp => tp.NomeTipo ).ToList();
        }

        public TipoPatrimonio BuscarPorId(Guid id)
        {
            return _context.TipoPatrimonio.Find(id);
        }

        public TipoPatrimonio BuscarPorNome(string nomeTipo)
        {
            return _context.TipoPatrimonio.FirstOrDefault(tp => tp.NomeTipo == nomeTipo);
        }

        public void Adicionar(TipoPatrimonio tipoPatrimonio)
        {
            _context.TipoPatrimonio.Add(tipoPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(TipoPatrimonio tipoPatrimonio)
        {
            if (tipoPatrimonio == null)
            {
                return;
            }

            TipoPatrimonio tipoPatrimonioBanco = _context.TipoPatrimonio.FirstOrDefault(tp => tp.TipoPatrimonioID == tipoPatrimonio.TipoPatrimonioID);

            tipoPatrimonioBanco.NomeTipo = tipoPatrimonio.NomeTipo;

            _context.SaveChanges();
        }

    }
}
