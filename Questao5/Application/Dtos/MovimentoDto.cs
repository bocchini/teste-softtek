using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questao5.Application.Dtos
{
    public class MovimentoDto
    {
        [Required(ErrorMessage = "Id da Conta Corrente é obrigatório")]
        public string IdContaCorrente { get; set; }

        [Required(ErrorMessage = "Id da TipoMovimento é obrigatório")]
        public string TipoMovimento { get; set; }

        [Required(ErrorMessage = "Valor da transação é obrigatório")]
        [Column(TypeName = "double(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2")]
        [DataType(DataType.Currency)]
        [Range(0.0, Double.MaxValue, ErrorMessage = "INVALID_VALUE")]
        public double Valor { get; set; }
    }
}
