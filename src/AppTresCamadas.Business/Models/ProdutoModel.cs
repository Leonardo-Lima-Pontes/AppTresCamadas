using System.ComponentModel.DataAnnotations;
using AppTresCamadas.Business.Models.Shared;

namespace AppTresCamadas.Business.Models
{
    public class ProdutoModel : EntityBase
    {
        public Guid FornecedorId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public bool Ativo { get; set; }
        public FornecedorModel Fornecedor { get; set; }
    }
}
