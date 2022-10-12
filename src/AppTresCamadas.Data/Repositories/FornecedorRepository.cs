using AppTresCamadas.Business.Interfaces;
using AppTresCamadas.Business.Models;
using AppTresCamadas.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AppTresCamadas.Data.Repositories
{
    public class FornecedorRepository : RepositoryBase<FornecedorModel>, IFornecedorRepository
    {
        public FornecedorRepository(AppDbContext database) : base(database) { }

        public async Task<FornecedorModel> ObterFornecedorEndereco(Guid id) =>
            await EntityDbSet.Include(f => f.Endereco).FirstOrDefaultAsync(f => f.Id == id);

        public async Task<FornecedorModel> ObterFornecedorProdutosEndereco(Guid id) =>
            await EntityDbSet
            .Include(f => f.Produtos)
            .Include(f => f.Endereco)
            .FirstOrDefaultAsync(f => f.Id == id);
    }
}
