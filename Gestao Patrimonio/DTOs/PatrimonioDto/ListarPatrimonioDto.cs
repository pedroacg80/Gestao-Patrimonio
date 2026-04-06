namespace Gestao_Patrimonio.DTOs.PatrimonioDto
{
    public class ListarPatrimonioDto
    {
        public Guid PatrimonioID { get; set; }

        public string Denominacao { get; set; } = null!;

        public string? NumeroPatrimonio { get; set; }

        public decimal? Valor { get; set; }

        public string? Imagem { get; set; }

        public Guid LocalizacaoID { get; set; }

        public Guid TipoPatrimonioID { get; set; }

        public Guid StatusPatrimonioID { get; set; }
    }
}
