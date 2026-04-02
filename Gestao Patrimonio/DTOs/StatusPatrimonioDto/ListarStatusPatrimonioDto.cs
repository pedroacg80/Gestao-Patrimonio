namespace Gestao_Patrimonio.DTOs.StatusPatrimonioDto
{
    public class ListarStatusPatrimonioDto
    {
        public Guid StatusPatrimonioID { get; set; }

        public string NomeStatus { get; set; } = null!;
    }
}
