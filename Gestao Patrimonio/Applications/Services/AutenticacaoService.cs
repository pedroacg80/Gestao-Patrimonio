using Gestao_Patrimonio.Applications.Autenticacao;
using Gestao_Patrimonio.Applications.Regras;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.AutenticacaoDto;
using Gestao_Patrimonio.Exceptions;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Applications.Services
{
    public class AutenticacaoService
    {
        private readonly IUsuarioRepository _repository;
        private readonly GeradorTokenJwt _tokenJwt;

        public AutenticacaoService(IUsuarioRepository repository, GeradorTokenJwt tokenJwt)
        {
            _repository = repository;
            _tokenJwt = tokenJwt;
        }

        private static bool VerificarSenha(string senhaDigitada, byte[] senhaHashBanco)
        {
            var hashDigitado = CriptografiaUsuario.CriptografarSenha(senhaDigitada);

            return hashDigitado.SequenceEqual(senhaHashBanco);
        }

        public TokenDto Login(LoginDto loginDto)
        {
            Usuario usuario = _repository.ObterPorNIFComTipoUsuario(loginDto.NIF);

            if (usuario == null)
            {
                throw new DomainException("NIF ou senha invalidos");
            }

            if (usuario.Ativo == false)
            {
                throw new DomainException("Usuario inativo");
            }

            if (!VerificarSenha(loginDto.Senha, usuario.Senha))
            {
                throw new DomainException("NIF ou senha invalidos");
            }

            string token = _tokenJwt.GerarToken(usuario);

           TokenDto novoToken = new TokenDto
           {
               Token = token,
               PrimeiroAcesso = usuario.PrimeiroAcesso,
               TipoUsuario = usuario.TipoUsuario.NomeTipo
           };
            return novoToken; 
        }

        public void TrocarPrimeiraSenha(Guid usuarioId, TrocarPrimeiraSenhaDto dto)
        {
            Validar.ValidarSenha(dto.SenhaAtual);
            Validar.ValidarSenha(dto.NovaSenha);

            Usuario usuario = _repository.BuscarPorId(usuarioId);

            if (usuario == null)
            {
                throw new DomainException("Usuario nao encontrado");
            }

            if (!VerificarSenha(dto.SenhaAtual, usuario.Senha))
            {
                throw new DomainException("Senha atual invalida");
            }

            if (dto.SenhaAtual == dto.NovaSenha)
            {
                throw new DomainException("A nova senha deve ser diferente da senha atual");
            }

            usuario.Senha = CriptografiaUsuario.CriptografarSenha(dto.NovaSenha);
            usuario.PrimeiroAcesso = false;

            _repository.AtualizarSenha(usuario);
            _repository.AtualizarPrimeiroAcesso(usuario);


        }
    }
}
