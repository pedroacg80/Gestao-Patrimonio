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
    }
}
