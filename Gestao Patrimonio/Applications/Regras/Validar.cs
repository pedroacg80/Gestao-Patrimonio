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
        
        public static void ValidarNumeroPatrimonio(string numeroPatrimonio)
        {
            if (string.IsNullOrEmpty(numeroPatrimonio))
            {
                throw new DomainException("Numero patrimonio eh obrigatorio");
            }
        }

        public static void ValidarNIF(string nif)
        {
            if (string.IsNullOrEmpty(nif))
            {
                throw new DomainException("NIF eh obriatorio");
            }
        }

        public static void ValidarCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                throw new DomainException("CPF eh obrigatorio");
            }
        }

        public static void ValidarEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new DomainException("Email eh obrigatorio");
            }
        }
        
    }
}
