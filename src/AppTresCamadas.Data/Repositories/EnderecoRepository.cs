using AppTresCamadas.Business.Interfaces;
using AppTresCamadas.Business.Models;
using AppTresCamadas.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AppTresCamadas.Data.Repositories
{
    public class EnderecoRepository : RepositoryBase<EnderecoModel>, IEnderecoRepository
    {
        public EnderecoRepository(AppDbContext database) : base(database) { }

        public async Task<EnderecoModel> ObterEnderecoPorFornecedor(Guid fornecedorId) =>
            await EntityDbSet.FirstOrDefaultAsync(e => e.FornecedorId == fornecedorId);
    }
}
