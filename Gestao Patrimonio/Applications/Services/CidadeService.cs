using Gestao_Patrimonio.DTOs.CidadeDto;
using Gestao_Patrimonio.Repositories;

namespace Gestao_Patrimonio.Applications.Services
{
    public class CidadeService
    {
        private readonly CidadeRepository _repository;

        public CidadeService(CidadeRepository repository)
        {
            _repository = repository;   
        }

        public List<LerCidadeDto> Listar()
        {

        }
    }
}
