using Gestao_Patrimonio.Applications.Regras;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.TipoUsuario;
using Gestao_Patrimonio.Exceptions;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Applications.Services
{
    public class TipoUsuarioService
    {
        private readonly ITipoUsuarioRepository _repository;

        public TipoUsuarioService(ITipoUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoUsuarioDto> Listar()
        {
            List<TipoUsuario> tipoUsuarios = _repository.Listar();

            List<ListarTipoUsuarioDto> tipoUsuariosDtos = tipoUsuarios.Select(tipoUsuario => new ListarTipoUsuarioDto
            {
                TipoUsuarioID = tipoUsuario.TipoUsuarioID,
                NomeTipo = tipoUsuario.NomeTipo
            }).ToList();

            return tipoUsuariosDtos;
        }

        public ListarTipoUsuarioDto BuscarPorId(Guid id)
        {
            TipoUsuario tipoUsuario = _repository.BuscarPorId(id);

            if (tipoUsuario == null)
            {
                throw new DomainException("Tipo Usuario nao encontrado");
            }

            ListarTipoUsuarioDto tipoUsuarioDto = new ListarTipoUsuarioDto
            {
                TipoUsuarioID = tipoUsuario.TipoUsuarioID,
                NomeTipo = tipoUsuario.NomeTipo
            };

            return tipoUsuarioDto;
        }

        public void Adicionar(CriarTipoUsuarioDto criarDto)
        {
            Validar.ValidarNome(criarDto.NomeTipo);

            TipoUsuario tipoUsuarioExistente = _repository.BuscarPorNome(criarDto.NomeTipo);

            if (tipoUsuarioExistente != null)
            {
                throw new DomainException("Ja existe um Tipo Usuario com esse nome");
            }

            TipoUsuario tipoUsuario = new TipoUsuario
            {
                NomeTipo = criarDto.NomeTipo
            };

            _repository.Adicionar(tipoUsuario);
        }

        public void Atualizar(CriarTipoUsuarioDto criarDto, Guid id)
        {
            Validar.ValidarNome(criarDto.NomeTipo);

            TipoUsuario tipoUsuarioBanco = _repository.BuscarPorId(id);

            if (tipoUsuarioBanco == null)
            {
                throw new DomainException("Tipo Usuario nao encontrado");
            }

            TipoUsuario tipoUsuarioExistente = _repository.BuscarPorNome(criarDto.NomeTipo);

            if (tipoUsuarioExistente != null)
            {
                throw new DomainException("Ja existe um tipo usuario com esse nome cadastrado");
            }

            tipoUsuarioBanco.NomeTipo = criarDto.NomeTipo;

            _repository.Atualizar(tipoUsuarioBanco);
        }
    }
}
