namespace Gestao_Patrimonio.DTOs.CidadeDto
{
    public class LerCidadeDto
    {
        public Guid CidadeId { get; set; }
        public string NomeCidade { get; set; } = null!;
        public string Estado { get; set; } = null!;
    }
}
