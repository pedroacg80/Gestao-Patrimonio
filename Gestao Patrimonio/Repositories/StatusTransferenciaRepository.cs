using Gestao_Patrimonio.Contexts;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Repositories
{
    public class StatusTransferenciaRepository : IStatusTransferenciaRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public StatusTransferenciaRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<StatusTransferencia> Listar()
        {
            return _context.StatusTransferencia.OrderBy(st => st.NomeStatus).ToList();
        }

        public StatusTransferencia BuscarPorId(Guid id)
        {
            return _context.StatusTransferencia.Find(id);
        }

        public StatusTransferencia BuscarPorNome(string nomeStatus)
        {
            return _context.StatusTransferencia.FirstOrDefault(st => st.NomeStatus == nomeStatus);
        }

        public void Adicionar(StatusTransferencia statusTransferencia)
        {
            _context.StatusTransferencia.Add(statusTransferencia);
            _context.SaveChanges();
        }

        public void Atualizar(StatusTransferencia statusTransferencia)
        {
            if (statusTransferencia == null)
            {
                return;
            }

            StatusTransferencia statusBanco = _context.StatusTransferencia.FirstOrDefault(st => st.StatusTransferenciaID == statusTransferencia.StatusTransferenciaID);

            statusBanco.NomeStatus = statusTransferencia.NomeStatus;

            _context.SaveChanges();
        }
    }
}
