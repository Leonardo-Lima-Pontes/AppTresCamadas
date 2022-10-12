using AppTresCamadas.Business.Models;

namespace AppTresCamadas.Business.Interfaces
{
    public interface IProdutoRepository : IRepository<ProdutoModel>
    {
        Task<IEnumerable<ProdutoModel>> ObterProdutosPorFornecedor(Guid fornecedorId);
        Task<IEnumerable<ProdutoModel>> ObterProdutosFornecedores();
        Task<ProdutoModel> ObterProdutoFornecedor(Guid id);
    }
}
