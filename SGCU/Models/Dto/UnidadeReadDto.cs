using System.ComponentModel.DataAnnotations;

namespace SGCU.Models.Dto
{
    public class UnidadeReadDto
    {
        public Guid UnidadeId { get; set; }
        public string CodigoUnidade { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
    }
}
