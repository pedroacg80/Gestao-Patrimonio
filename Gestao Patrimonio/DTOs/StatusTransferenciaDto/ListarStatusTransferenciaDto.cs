namespace Gestao_Patrimonio.DTOs.StatusTransferenciaDto
{
    public class ListarStatusTransferenciaDto
    {
        public Guid StatusTransferenciaID { get; set; }

        public string NomeStatus { get; set; } = null!;
    }
}
