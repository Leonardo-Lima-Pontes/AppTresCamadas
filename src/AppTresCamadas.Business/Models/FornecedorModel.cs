using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppTresCamadas.Business.Models.Enums;
using AppTresCamadas.Business.Models.Shared;

namespace AppTresCamadas.Business.Models
{
    public class FornecedorModel : EntityBase
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoDeFornecedor TipoDeFornecedor { get; set; }
        public EnderecoModel Endereco { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<ProdutoModel> Produtos { get; set; }
    }
}
