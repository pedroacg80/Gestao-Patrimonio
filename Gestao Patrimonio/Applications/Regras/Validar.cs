using Gestao_Patrimonio.Exceptions;

namespace Gestao_Patrimonio.Applications.Regras
{
    public class Validar
    {
        public static void ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new DomainException("Nome eh obrigatorio");
            }
        }

        public static void ValidarEstado(string estado)
        {
            if (string.IsNullOrEmpty(estado))
            {
                throw new DomainException("Estado eh obrigatorio");
            }
        }
        public static void ValidarBairro(string bairro)
        {
            if (string.IsNullOrEmpty(bairro))
            {
                throw new DomainException("Bairro eh obrigatorio");
            }
        }
    }
}
