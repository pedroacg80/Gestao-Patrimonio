using Gestao_Patrimonio.Contexts;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public TipoUsuarioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<TipoUsuario> Listar()
        {
            return _context.TipoUsuario.OrderBy(tu => tu.NomeTipo).ToList();
        }

        public TipoUsuario BuscarPorId(Guid id)
        {
            return _context.TipoUsuario.Find(id);
        }

        public TipoUsuario BuscarPorNome(string nomeTipo)
        {
            return _context.TipoUsuario.FirstOrDefault(tu => tu.NomeTipo == nomeTipo);
        }

        public void Adicionar(TipoUsuario tipoUsuario)
        {
            _context.TipoUsuario.Add(tipoUsuario);
            _context.SaveChanges();
        }

        public void Atualizar(TipoUsuario tipoUsuario)
        {
            if (tipoUsuario == null)
            {
                return;
            }

            TipoUsuario tipoUsuarioBanco = _context.TipoUsuario.FirstOrDefault(tu => tu.TipoUsuarioID == tipoUsuario.TipoUsuarioID);

            tipoUsuarioBanco.NomeTipo = tipoUsuario.NomeTipo;

            _context.SaveChanges();

        }

    }
}
