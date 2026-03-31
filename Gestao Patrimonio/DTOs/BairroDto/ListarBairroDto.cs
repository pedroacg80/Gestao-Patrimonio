namespace Gestao_Patrimonio.DTOs.BairroDto
{
    public class ListarBairroDto
    {
        public Guid BairroId { get; set; }
        public string NomeBairro { get; set; } 
        public Guid CidadeId { get; set; }
    }
}
