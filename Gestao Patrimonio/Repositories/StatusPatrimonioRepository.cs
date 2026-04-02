using Gestao_Patrimonio.Contexts;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Repositories
{
    public class StatusPatrimonioRepository : IStatusPatrimonioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public StatusPatrimonioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<StatusPatrimonio> Listar()
        {
            return _context.StatusPatrimonio.OrderBy(sp => sp.NomeStatus).ToList();
        }

        public StatusPatrimonio BuscarPorId(Guid id)
        {
            return _context.StatusPatrimonio.Find(id);
        }

        public StatusPatrimonio BuscarPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(sp => sp.NomeStatus == nomeStatus);
        }

        public void Adicionar(StatusPatrimonio statusPatrimonio)
        {
            _context.StatusPatrimonio.Add(statusPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(StatusPatrimonio statusPatrimonio)
        {
            if (statusPatrimonio == null)
            {
                return;
            }

            StatusPatrimonio statusBanco = _context.StatusPatrimonio.FirstOrDefault(sp => sp.StatusPatrimonioID == statusPatrimonio.StatusPatrimonioID);

            statusBanco.NomeStatus = statusPatrimonio.NomeStatus;

            _context.SaveChanges();
        }
    }
}
