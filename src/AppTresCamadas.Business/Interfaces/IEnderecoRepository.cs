using AppTresCamadas.Business.Models;

namespace AppTresCamadas.Business.Interfaces
{
    public interface IEnderecoRepository : IRepository<EnderecoModel>
    {
        Task<EnderecoModel> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}
