using AppTresCamadas.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace AppTresCamadas.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    private DbSet<ProdutoModel> Produtos { get; set; }
    private DbSet<FornecedorModel> Fornecedores { get; set; }
    private DbSet<EnderecoModel> Enderecos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigurarTamanhoDeStringPadrao(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        ConfigurarCancelamentoDeDelecaoEmCascata(modelBuilder);
        
        base.OnModelCreating(modelBuilder);
    }

    private static void ConfigurarCancelamentoDeDelecaoEmCascata(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model
                     .GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
    }

    private static void ConfigurarTamanhoDeStringPadrao(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetProperties()
                         .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");
    }
}
 