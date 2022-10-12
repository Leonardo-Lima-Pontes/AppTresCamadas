using AppTresCamadas.Business.Models;

namespace AppTresCamadas.Business.Interfaces
{
    public interface IFornecedorRepository : IRepository<FornecedorModel>
    {
        Task<FornecedorModel> ObterFornecedorEndereco(Guid id);
        Task<FornecedorModel> ObterFornecedorProdutosEndereco(Guid id);
    }
}
