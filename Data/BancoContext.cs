using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data
{
    public class BancoContext: DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options): base(options)
        {
            
        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        
                
    }
}