using AppTresCamadas.Business.Interfaces;
using AppTresCamadas.Business.Models;
using AppTresCamadas.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AppTresCamadas.Data.Repositories
{
    public class ProdutoRepository : RepositoryBase<ProdutoModel>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext database) : base(database) { }

        public async Task<ProdutoModel> ObterProdutoFornecedor(Guid id) =>
            await EntityDbSet.AsNoTracking().Include(p => p.Fornecedor).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<ProdutoModel>> ObterProdutosFornecedores() =>
            await EntityDbSet.AsNoTracking().Include(p => p.Fornecedor).ToListAsync();

        public async Task<IEnumerable<ProdutoModel>> ObterProdutosPorFornecedor(Guid fornecedorId) =>
            await EntityDbSet.AsNoTracking().Where(p => p.FornecedorId == fornecedorId).ToListAsync();

    }
}
